using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] LevelDataSO levelData;
    [SerializeField] GeneratorController generatorController;
    [SerializeField] DecorativeViewController decorativeController;
    [SerializeField] Transform generatorContainer;

    private CompositeDisposable disposables = new CompositeDisposable();

    GeneratorFactory generatorFactory;
    Wallet wallet;
    List<GeneratorController> generatorList = new List<GeneratorController>();


    float timer;
    bool isWorldActive;

    private void Awake()
    {
        Application.targetFrameRate = 30;
        Init();
    }
    private void Start()
    {
        MessageBroker.Default.Publish(new Wallet_Message(wallet));
        MessageBroker.Default.Receive<Decorative_BuyMessage>().Subscribe(((x) => { BuyedDecorative(x.Model); })).AddTo(disposables);
        MessageBroker.Default.Receive<Mission_ClaimMessage>().Subscribe(((x) => { MissionClaim(x); })).AddTo(disposables);
        MessageBroker.Default.Receive<Gamble_SpinMessage>().Subscribe(((x) => { Spin(); })).AddTo(disposables);
    }
    private void OnDestroy()
    {
        disposables.Clear();
    }

    private void Init()
    {
        Currency c1 = new Currency(CurrencyType.Money, 0);
        Currency c2 = new Currency(CurrencyType.Chip, 3);
        Currency c3 = new Currency(CurrencyType.Gem, 0);
        List<Currency> testList = new List<Currency>();
        testList.Add(c1);
        testList.Add(c2);
        testList.Add(c3);

        wallet = new Wallet(testList);


        InitGenerator();
        decorativeController.Init(levelData.LevelDecorative);

        isWorldActive = true;
    }

    void InitGenerator()
    {
        generatorFactory = new GeneratorFactory();
        foreach (var generator in levelData.LevelGenerator.GeneratorList)
        {
            var controller = generatorFactory.Create(1, generatorController, generator, wallet, generatorContainer);

            generatorList.Add(controller);

            controller.Collected.Subscribe(x => Collect(x)).AddTo(disposables);
            controller.Leveluped.Subscribe(x => LevelUpGenerator(x)).AddTo(disposables);
            controller.Unlocked.Subscribe(x => UnlockGenerator(x)).AddTo(disposables);
        }
    }
  

    void Collect(CurrencyMessage currencyMessage)
    {
        MessageBroker.Default.Publish(new CurrencyMessage(currencyMessage.CurrencyType, currencyMessage.Amount));
        
        wallet.Gain(currencyMessage.CurrencyType, currencyMessage.Amount);
    }
    
    void LevelUpGenerator(GeneratorModel model)
    {
        wallet.Spend(model.UpgradeCurrency.Id, model.UpgradeCost);
        

        var generatorData = levelData.LevelGenerator.GeneratorList.FirstOrDefault(s => s.Id == model.Id);
        var upgradedModel = generatorFactory.GetData(model.Level + 1, generatorData, wallet);

        generatorList.FirstOrDefault(s => s.Id == upgradedModel.Id)?.UpdateModel(upgradedModel);

        MessageBroker.Default.Publish(new Mission_SpendableUpdateMessage(model.Id, model.IsUnlocked, 1));

        FirebaseManager.Instance.LogGenericEventWithParameter("levelup",new Dictionary<string, string>() { { "id", model.Id }, { "level", model.Level.ToString() } });
    }
    void UnlockGenerator(GeneratorModel model)
    {
        MessageBroker.Default.Publish(new Mission_SpendableUpdateMessage(model.Id, model.IsUnlocked,0));
        wallet.Spend(model.UpgradeCurrency.Id, model.UnlockCost);

        FirebaseManager.Instance.LogGenericEventWithParameter("unlock_generator", new Dictionary<string, string>() { {"id", model.Id } });
    }

    void BuyedDecorative(DecorativeModel model)
    {
        MessageBroker.Default.Publish(new Mission_SpendableUpdateMessage(model.Id, model.IsUnlocked, 0));
        wallet.Spend(model.UnlockCurrency, model.UnlockCost);

        FirebaseManager.Instance.LogGenericEventWithParameter("buy_decorative", new Dictionary<string, string>() { { "id", model.Id } });
    }

    void MissionClaim(Mission_ClaimMessage message)
    {
        MessageBroker.Default.Publish(new CurrencyMessage(message.CurrencyType, message.Amount));

        wallet.Gain(message.CurrencyType, message.Amount);
    }

    void Spin()
    {
        wallet.Spend(CurrencyType.Chip,1);

        FirebaseManager.Instance.LogGenericEvent("spin");
    }

    void SpinWin(Mission_ClaimMessage message)
    {
        MessageBroker.Default.Publish(new CurrencyMessage(message.CurrencyType, message.Amount));

        wallet.Gain(message.CurrencyType, message.Amount);
    }

    private void Update()
    {
        if (isWorldActive)
            MessageBroker.Default.Publish(new GameManager_TimeMessage(Time.deltaTime));
    }

}
