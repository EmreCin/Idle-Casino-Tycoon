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
    [SerializeField] SpinnerItemView resultItemView;

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

        resultItemView.PublishResult(result.IsSuccess, result.Selected.WinMessage);

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
        resultItemView.Reset();

    }
}
