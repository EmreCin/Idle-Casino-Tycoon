using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMissionBehavior
{
    public void MissionStart();
    public void CheckMission();
    public void ChangeState();
    public void CompleteMission();

}
