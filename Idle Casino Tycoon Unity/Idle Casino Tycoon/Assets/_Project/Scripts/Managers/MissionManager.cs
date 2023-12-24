using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    [SerializeField] MissionContainerSO missionContainer;
    [SerializeField] int activeMissionNumber = 3;
    [SerializeField] List<MissionItemView> missionItemViewList;

    private CompositeDisposable disposables = new CompositeDisposable();

    Queue<MissionModel> missionModelQueue = new Queue<MissionModel>();
    List<MissionModel> currentModel = new List<MissionModel>();

    private void Awake()
    {
        MessageBroker.Default.Receive<Mission_CompleteMessage>().Subscribe(((x) => { HandleCompleteMission(x); })).AddTo(disposables);

        foreach (var missionData in missionContainer.MissionList)
        {
            missionModelQueue.Enqueue(new MissionModel(missionData));
        }

        while (currentModel.Count < activeMissionNumber)
        {
            if (missionModelQueue.Count == 0) break;

            var model = missionModelQueue.Dequeue();
            model.MissionBehavior.MissionStart();

            missionItemViewList[currentModel.Count].Init(model);
            currentModel.Add(model);
        }
    }
    private void OnDisable()
    {
        disposables.Clear();
    }

    void HandleCompleteMission(Mission_CompleteMessage message)
    {
        if (message.IsClaimed)
        {
            missionItemViewList.FirstOrDefault(s => s.ItemViewId == message.Id).CompleteMission();

            currentModel.Remove(currentModel.FirstOrDefault(s => s.Id == message.Id));
            StartNewMission(message.Id);
        }
        else
        {
            missionItemViewList.FirstOrDefault(s => s.ItemViewId == message.Id).ActivateClaimButton();
        }

    }
    
    void StartNewMission(int id)
    {
        if (missionModelQueue.Count == 0)
        {
            StartCoroutine(DelayedClose(id));
            return;
        }

        var model = missionModelQueue.Dequeue();
        model.MissionBehavior.MissionStart();

        missionItemViewList.FirstOrDefault(s=> s.ItemViewId == id).Init(model);
        currentModel.Add(model);
    }

    IEnumerator DelayedClose(int id)
    {
        yield return new WaitForSeconds(1);
        missionItemViewList.FirstOrDefault(s => s.ItemViewId == id).gameObject.SetActive(false);
    }
}
