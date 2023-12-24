using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MissionLevelUpGenerator", menuName = "LevelData/Mission/MissionLevelUpGenerator", order = 3)]
public class MissionLevelUpGenerator : MissionBaseSO
{
    [Header("Mission Target")]
    public int AmountTarget;
    public string GeneratorId;


    public override IMissionBehavior GetBehavior()
    {
        return new MissionLevelUpGeneratorBehavior(Id, Reward, RewardCurrencyType, GeneratorId, AmountTarget); ;
    }
}
