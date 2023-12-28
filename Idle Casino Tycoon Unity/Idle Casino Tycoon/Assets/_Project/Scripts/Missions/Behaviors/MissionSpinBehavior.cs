using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class MissionSpinBehavior : IMissionBehavior
{
    private float amountTarget;
    private int id;
    private float reward;
    private CurrencyType rewardCurrencyType;


    private CompositeDisposable disposables = new CompositeDisposable();

    float currentValue;
    bool isStarted;
    bool isReadyToComplete;


    public MissionSpinBehavior(int id, float reward, CurrencyType rewardCurrencyType, float target)
    {
        this.id = id;
        this.reward = reward;
        this.rewardCurrencyType = rewardCurrencyType;
        amountTarget = target;
    }

    public void MissionStart()
    {
        isStarted = true;
        MessageBroker.Default.Receive<Gamble_SpinMessage>().Subscribe(((x) => { Spin(); })).AddTo(disposables);
    }

    private void Spin()
    {
        currentValue ++;

        MessageBroker.Default.Publish(new Mission_UpdateMessage(id, currentValue / amountTarget));

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
        MessageBroker.Default.Publish(new Mission_CompleteMessage(id, false));
    }

    public void CompleteMission()
    {
        if (!isReadyToComplete) return;

        MessageBroker.Default.Publish(new Mission_CompleteMessage(id, true));
        MessageBroker.Default.Publish(new Mission_ClaimMessage(rewardCurrencyType, reward));

        disposables.Clear();
    }
}
