using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class MissionMoneyCollectBehavior : IMissionBehavior
{
    private float amountTarget;
    private int id;
    private float reward;
    private CurrencyType rewardCurrencyType;

    private CurrencyType currencyType;

    private CompositeDisposable disposables = new CompositeDisposable();

    float currentValue;
    bool isStarted;
    bool isReadyToComplete;


    public MissionMoneyCollectBehavior(int id, float reward,CurrencyType rewardCurrencyType, CurrencyType currency)
    {
        this.id = id;
        this.reward = reward;
        this.rewardCurrencyType = rewardCurrencyType;
        currencyType = currency;
    }



    public void MissionStart()
    {
        isStarted = true;

        MessageBroker.Default.Receive<CurrencyMessage>().Where(s=> s.CurrencyType == currencyType).Subscribe(((x) => { Collect(x); })).AddTo(disposables);
    }

    private void Collect(CurrencyMessage currencyMessage)
    {
        currentValue += currencyMessage.Amount;
        
        CheckMission();
    }

    public void CheckMission()
    {
        if (currentValue >= amountTarget)
            ChangeState();

    }
    public void ChangeState()
    {
        if (isReadyToComplete) return;
        isReadyToComplete = true;
        Debug.LogError("ChangeState " + id.ToString());
        MessageBroker.Default.Publish(new Mission_CompleteMessage(id, false));
    }

    public void CompleteMission()
    {
        if (!isReadyToComplete) return;

        Debug.LogError("BEH CompleteMission " + id.ToString());
        MessageBroker.Default.Publish(new Mission_CompleteMessage(id,true));

        disposables.Clear();
    }

    public void SetTarget(float target)
    {
        amountTarget = target;
    }
}