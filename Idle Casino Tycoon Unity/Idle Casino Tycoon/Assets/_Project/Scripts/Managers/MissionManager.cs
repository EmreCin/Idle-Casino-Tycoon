using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    [SerializeField] MissionContainerSO missionContainer;
    [SerializeField] int activeMissionNumber = 3;

    Queue<MissionModel> missionModelQueue = new Queue<MissionModel>();
    List<MissionModel> currentModel = new List<MissionModel>();

    private void Awake()
    {
        foreach (var missionData in missionContainer.MissionList)
        {
            missionModelQueue.Enqueue(new MissionModel(missionData));
        }

        while(currentModel.Count < activeMissionNumber)
        {
            if (missionModelQueue.Count == 0) break;

            var model = missionModelQueue.Dequeue();
            model.MissionBehavior.MissionStart();
            currentModel.Add(model);
        }
    }
    
}
