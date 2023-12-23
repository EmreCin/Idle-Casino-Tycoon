using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionModel 
{
    public int Id;
    public string Name;
    public string Desc;
    public Currency Reward;

    public IMissionBehavior MissionBehavior;

    public MissionModel(MissionBaseSO data)
    {
        Id = data.Id;
        Name = data.Name;
        Desc = data.Desc;
        Reward = data.Reward;

        MissionBehavior = data.GetBehavior();
    }

    
}
