using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MissionUnlock", menuName = "LevelData/Mission/MissionUnlock", order = 2)]
public class MissionUnlock : MissionBaseSO
{
    [Header("Mission Target")]
    public string SpendableId;


    public override IMissionBehavior GetBehavior()
    {
        return new MissionUnlockSpendableBehavior(Id, Reward, RewardCurrencyType, SpendableId);
    }
}
