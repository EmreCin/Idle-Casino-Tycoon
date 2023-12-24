using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MissionUnlockGenerator", menuName = "LevelData/Mission/MissionUnlockGenerator", order = 2)]
public class MissionUnlockGenerator : MissionBaseSO
{
    [Header("Mission Target")]
    public string GeneratorId;


    public override IMissionBehavior GetBehavior()
    {
        var behavior = new MissionUnlockGeneratorBehavior(Id, Reward, RewardCurrencyType, GeneratorId);
        return behavior;
    }
}
