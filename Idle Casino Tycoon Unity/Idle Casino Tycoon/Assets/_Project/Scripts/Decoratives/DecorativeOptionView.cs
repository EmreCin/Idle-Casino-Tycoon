using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class DecorativeOptionView : MonoBehaviour
{
    [SerializeField] List<DecorativeOptionItemView> optionItemList;
    [SerializeField] Transform panel;
    [SerializeField] Button buyButton;

    private CompositeDisposable disposables = new CompositeDisposable();

    int currentIndex;
    DecorativeModel model;
    Wallet wallet;

    private void Awake()
    {
        buyButton.onClick.AddListener(() => Buy());
        MessageBroker.Default.Receive<Wallet_Message>().Subscribe(((x) => { GetWallet(x.Wallet); })).AddTo(disposables);
        MessageBroker.Default.Receive<Decorative_SelectMessage>().Subscribe(((x) => { FillOptionIcons(x.Model); })).AddTo(disposables);
    }

    private void OnDisable()
    {
        disposables.Clear();
    }

    public void FillOptionIcons(DecorativeModel model)
    {
        currentIndex = 1;
        this.model = model;
        CheckIsUnlockable();
        ResetOptions();

        for (int i = 0; i < optionItemList.Count; i++)
        {
            if(i < model.SpriteList.Count)
            {
                optionItemList[i].gameObject.SetActive(true);
                optionItemList[i].InitOption(model.SpriteList[i], SelectOption, i);
                if (i == 0) optionItemList[i].OptionSelected(true);
            }
        }
        panel.gameObject.SetActive(true);
    }

    void SelectOption(int index)
    {
        currentIndex = index;

        for (int i = 0; i < optionItemList.Count; i++)
        {
            optionItemList[i].OptionSelected(i == index);
        }
    }

    private void ResetOptions()
    {
        foreach (var item in optionItemList)
        {
            item.gameObject.SetActive(false);
            item.OptionSelected(false);
        }
    }

    private void Buy()
    {
        model.SelectedVisual = currentIndex;

        MessageBroker.Default.Publish<Decorative_BuyMessage>(new Decorative_BuyMessage(model));
        panel.gameObject.SetActive(false);
    }
    private void GetWallet(Wallet wallet)
    {
        this.wallet = wallet;
    }

    void CheckIsUnlockable()
    {
        var currency = wallet.GetCurrnecyById(model.UnlockCurrency);

        if (model.UnlockCost > currency.Amount)
            buyButton.interactable = false;
        else
            buyButton.interactable = true;


    }
}
