using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class GambleManager : MonoBehaviour
{
    [SerializeField] GambleConfigSO config;
    [SerializeField] List<SpinController> spinnerList;
    [SerializeField] Button spinButton;

    Currency chip;
    SpinResult result;
    private CompositeDisposable disposables = new CompositeDisposable();


    private void Awake()
    {
        spinButton.onClick.AddListener(() => {
            Spin();
            foreach (var spinner in spinnerList)
            {
                if(spinner.ResultSpinner) spinner.SpinFinished.Subscribe(x => HandleSpinResult()).AddTo(disposables);
                spinner.Spin();
            }
        });

        MessageBroker.Default.Receive<Wallet_Message>().Subscribe(((x) => { GetWallet(x.Wallet); })).AddTo(disposables);
    }

    private void OnDestroy()
    {
        disposables.Clear();
    }

    void GetWallet(Wallet wallet)
    {
        chip = wallet.GetCurrnecyById(CurrencyType.Chip);
        chip.ObserveEveryValueChanged(s => s.Amount).Subscribe(x => { CheckIsAvailable(); }).AddTo(disposables);
    }

    void CheckIsAvailable()
    {
        if (chip.Amount > 0)
            spinButton.interactable = true;
        else
            spinButton.interactable = false;
    }
   
    void HandleSpinResult()
    {
        if (result == null) return;

        if (!result.IsSuccess)
        {
            result = null;
            return;
        }


        if(result.Selected.GambleOption == GambleOption.Chip || result.Selected.GambleOption == GambleOption.Money)
        {
            var rewardCurrency = (GambleOption_Currency)result.Selected;
            MessageBroker.Default.Publish(new Mission_ClaimMessage(rewardCurrency.RewardCurrency, rewardCurrency.Reward));
        }
        else
        {
            var rewardMultiplier = (GambleOption_Generator)result.Selected;
            var mList = new List<MultiplierInstance>();
                mList.Add(new MultiplierInstance(rewardMultiplier.GeneratorId, rewardMultiplier.MultiplierType, rewardMultiplier.Multiplier, rewardMultiplier.Duration));
            MessageBroker.Default.Publish(new AddMultiplierMessage(mList));
        }
    }

    void Spin()
    {
        MessageBroker.Default.Publish(new Gamble_SpinMessage());

        result = new SimpleGamble().Spin(config.OptionList);
        int index = 1;
        foreach (var spinner in spinnerList)
        {
            if (result.IsSuccess) spinner.Init(new SpinnerFiller().FillSpinnerSuccess(config.OptionList, result.Selected.Icon));
            else spinner.Init(new SpinnerFiller().FillSpinnerFail(config.OptionList, result, index));
            index++;
        }
    }
}

public class SimpleGamble
{
    public SpinResult Spin(List<GambleOptionSO> optionSO)
    {
        SpinResult result = new SpinResult();
        result.IsSuccess = true;
        //slot1
        float randomNumber = Random.Range(100, 1001);
        Debug.LogError("randomNumber--> " + randomNumber);
        result.Selected = optionSO.Where(s => s.StartWeight <= randomNumber).OrderByDescending(o => o.StartWeight).FirstOrDefault();
        Debug.LogError("selected--> " + result.Selected.GambleOption.ToString());
        result.Slot1 = result.Selected.Icon;

        //slot2
        randomNumber = Random.Range(100, 1001);
        Debug.LogError("randomNumber--> " + randomNumber);
        if (result.Selected.StartWeight > randomNumber)
        {
            result.IsSuccess = false;
            result.Slot2 = optionSO.Where(s => s.StartWeight <= randomNumber && result.Selected != s).OrderByDescending(o => o.StartWeight).FirstOrDefault().Icon;
        }
        else result.Slot2 = result.Selected.Icon;
        //slot3
        randomNumber = Random.Range(100, 1001);
        Debug.LogError("randomNumber--> " + randomNumber);
        if (result.Selected.EndWeight > randomNumber)
        {
            result.IsSuccess = false;
            result.Slot3 = optionSO.Where(s => s.StartWeight <= randomNumber && result.Selected != s).OrderByDescending(o => o.StartWeight).FirstOrDefault().Icon;
        }
        else result.Slot3 = result.Selected.Icon;
        Debug.LogError("RESULT-------->" + result.IsSuccess);

        return result;
    }
}

public class SpinResult
{
    public bool IsSuccess;
    public Sprite Slot1;
    public Sprite Slot2;
    public Sprite Slot3;
    public GambleOptionSO Selected;
}

public class SpinnerFiller
{
    public List<Sprite> FillSpinnerSuccess(List<GambleOptionSO> optionSO, Sprite selected)
    {
        List<Sprite> spriteList = new List<Sprite>();
        
        while(spriteList.Count <5)
        {
            int randomInt = Random.Range(0, 6);
            if (optionSO[randomInt].Icon != selected)
                spriteList.Add(optionSO[randomInt].Icon);
        }
        spriteList.Add(selected);

        return spriteList;
    }

    public List<Sprite> FillSpinnerFail(List<GambleOptionSO> optionSO, SpinResult result, int index)
    {
        List<Sprite> spriteList = new List<Sprite>();

        while (spriteList.Count < 5)
        {
            int randomInt = Random.Range(0, 6);
            if (index == 1 &&  optionSO[randomInt].Icon != result.Slot1 
                || index == 2 && optionSO[randomInt].Icon != result.Slot2
                || index == 3 && optionSO[randomInt].Icon != result.Slot3)
                spriteList.Add(optionSO[randomInt].Icon);
        }
        if (index == 1)
            spriteList.Add(result.Slot1);
        else if (index == 2)
            spriteList.Add(result.Slot2);
        else if (index == 3)
            spriteList.Add(result.Slot3);


        return spriteList;
    }
}