using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class MissionUnlockSpendableBehavior : IMissionBehavior
{
    private string targetGeneratorId;
    private int id;
    private float reward;
    private CurrencyType rewardCurrencyType;


    private CompositeDisposable disposables = new CompositeDisposable();

    bool isStarted;
    bool isReadyToComplete;


    public MissionUnlockSpendableBehavior(int id, float reward, CurrencyType rewardCurrencyType, string target)
    {
        this.id = id;
        this.reward = reward;
        this.rewardCurrencyType = rewardCurrencyType;
        targetGeneratorId = target;

        MessageBroker.Default.Receive<Mission_SpendableUpdateMessage>().Where(s => s.SpendableId == targetGeneratorId).Subscribe(((x) => { Unlock(); })).AddTo(disposables);
    }

    public void MissionStart()
    {
        isStarted = true;

        CheckMission();
    }

    private void Unlock()
    {
        isReadyToComplete = true;

        CheckMission();
    }

    public void CheckMission()
    {
            ChangeState();
    }
    public void ChangeState()
    {
        if (!isStarted || !isReadyToComplete) return;
        
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
