using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MissionSpin", menuName = "LevelData/Mission/MissionSpin", order = 5)]

public class MissionSpin : MissionBaseSO
{
    [Header("Mission Target")]
    public float AmountTarget;


    public override IMissionBehavior GetBehavior()
    {
        return new MissionSpinBehavior(Id, Reward, RewardCurrencyType, AmountTarget); ;
    }
}


