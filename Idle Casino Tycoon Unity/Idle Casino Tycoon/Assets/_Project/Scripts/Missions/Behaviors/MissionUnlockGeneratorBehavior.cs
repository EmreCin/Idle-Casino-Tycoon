using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class MissionUnlockGeneratorBehavior : IMissionBehavior
{
    private string targetGeneratorId;
    private int id;
    private float reward;
    private CurrencyType rewardCurrencyType;


    private CompositeDisposable disposables = new CompositeDisposable();

    bool isStarted;
    bool isReadyToComplete;


    public MissionUnlockGeneratorBehavior(int id, float reward, CurrencyType rewardCurrencyType, string target)
    {
        this.id = id;
        this.reward = reward;
        this.rewardCurrencyType = rewardCurrencyType;
        targetGeneratorId = target;

        MessageBroker.Default.Receive<Generator_UpdateMessage>().Where(s => s.GeneratorId == targetGeneratorId).Subscribe(((x) => { Unlock(); })).AddTo(disposables);
    }

    public void MissionStart()
    {
        isStarted = true;

        CheckMission();
    }

    private void Unlock()
    {
        isReadyToComplete = true;
        //MessageBroker.Default.Publish(new Mission_UpdateMessage(id, 1));

        CheckMission();
    }

    public void CheckMission()
    {
            ChangeState();
    }
    public void ChangeState()
    {
        if (!isStarted || !isReadyToComplete) return;
        
        Debug.LogError("ChangeState " + id.ToString());
        MessageBroker.Default.Publish(new Mission_CompleteMessage(id, false));
    }

    public void CompleteMission()
    {
        if (!isReadyToComplete) return;

        Debug.LogError("BEH CompleteMission " + id.ToString());
        MessageBroker.Default.Publish(new Mission_CompleteMessage(id, true));
        MessageBroker.Default.Publish(new Mission_ClaimMessage(rewardCurrencyType, reward));

        disposables.Clear();
    }

    

   
}
