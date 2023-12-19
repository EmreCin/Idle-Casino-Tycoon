using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] LevelDataSO levelData;
    [SerializeField] GeneratorController generatorController;
    [SerializeField] Transform generatorContainer;

    private CompositeDisposable disposables = new CompositeDisposable();

    GeneratorFactory factory;
    Wallet wallet;
    List<GeneratorController> generatorList = new List<GeneratorController>();


    float timer;
    bool isWorldActive;

    private void Awake()
    {
        Init();
    }
    private void OnDestroy()
    {
        disposables.Clear();
    }

    private void Init()
    {
        Currency test = new Currency(CurrencyType.Money, 100);
        List<Currency> testList = new List<Currency>();
        testList.Add(test);

        wallet = new Wallet(testList);

        factory = new GeneratorFactory();
        foreach (var generator in levelData.LevelGenerator.GeneratorList)
        {
            var controller = factory.Create(1 ,generatorController, generator, wallet, generatorContainer);

            generatorList.Add(controller);

            controller.Collected.Subscribe(x => Collect(x)).AddTo(disposables);
            controller.Leveluped.Subscribe(x => LevelUpGenerator(x)).AddTo(disposables);
        }

        isWorldActive = true;
    }


    void Collect(CurrencyMessage currencyMessage)
    {
        wallet.Gain(currencyMessage.CurrencyType, currencyMessage.Amount);
    }
    
    void LevelUpGenerator(GeneratorModel model)
    {
        wallet.Spend(model.UpgradeCurrency.Id, model.UpgradeCost);
        

        var generatorData = levelData.LevelGenerator.GeneratorList.FirstOrDefault(s => s.Id == model.Id);
        var upgradedModel = factory.GetData(model.Level + 1, generatorData, wallet);

        generatorList.FirstOrDefault(s => s.Id == upgradedModel.Id)?.UpdateModel(upgradedModel);
    }

    private void Update()
    {
        //    timer += Time.deltaTime;

        //if(isWorldActive && timer >= 1)
        //{
        //    MessageBroker.Default.Publish(new GameManager_TimeMessage(timer));
        //    timer = 0;
        //}
        MessageBroker.Default.Publish(new GameManager_TimeMessage(Time.deltaTime));
    }

}
