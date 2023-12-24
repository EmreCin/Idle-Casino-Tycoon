using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class MissionLevelUpGeneratorBehavior : IMissionBehavior
{
    private int id;
    private int amountTarget;
    private string generatorId;
    private float reward;
    private CurrencyType rewardCurrencyType;


    private CompositeDisposable disposables = new CompositeDisposable();

    int currentValue;
    bool isStarted;
    bool isReadyToComplete;


    public MissionLevelUpGeneratorBehavior(int id, float reward, CurrencyType rewardCurrencyType, string generatorId, int target)
    {
        this.id = id;
        this.reward = reward;
        this.rewardCurrencyType = rewardCurrencyType;
        amountTarget = target;
        this.generatorId = generatorId;

        currentValue = 1;

        
        MessageBroker.Default.Receive<Mission_SpendableUpdateMessage>().Where(s => s.SpendableId == generatorId).Subscribe(((x) => { LevelUp(x); })).AddTo(disposables);
    }

    private void LevelUp(Mission_SpendableUpdateMessage message)
    {
        currentValue += message.LevelUp;

        MessageBroker.Default.Publish(new Mission_UpdateMessage(id, (float)currentValue / amountTarget));

        CheckMission();
    }

    public void MissionStart()
    {
        isStarted = true;

        CheckMission();
    }

    public void CheckMission()
    {
        if (amountTarget <= currentValue)
        {
            isReadyToComplete = true;

            ChangeState();
        }
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
