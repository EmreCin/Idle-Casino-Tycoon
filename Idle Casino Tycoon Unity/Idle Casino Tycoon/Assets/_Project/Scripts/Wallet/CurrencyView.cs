using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyView : MonoBehaviour
{
    [SerializeField] CurrencyType currencyType;
    [SerializeField] TMPro.TMP_Text amountText;
    [SerializeField] Sprite currencySprite;
    [SerializeField] Image currencyImage;

    Currency currency;

    private CompositeDisposable disposables = new CompositeDisposable();

    void Start()
    {
        currencyImage.sprite = currencySprite;
    }
    private void OnDestroy()
    {
        disposables.Clear();
    }

    public void Init(Wallet wallet)
    {
        currency = wallet.GetCurrnecyById(currencyType);
        amountText.text = currency.Amount.ToString();

        currency.ObserveEveryValueChanged(s => s.Amount).Subscribe(x => { UpdateAmount(); }).AddTo(disposables);
    }

    void UpdateAmount()
    {
        if(currency.Id == CurrencyType.Money)
            amountText.text = CurrencyHelper.ToMoney(currency.Amount,false);
        else
            amountText.text = currency.Amount.ToString();
    }

    
}
