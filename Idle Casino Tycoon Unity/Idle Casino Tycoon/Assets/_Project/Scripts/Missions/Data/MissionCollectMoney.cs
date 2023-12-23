using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MissionCollectMoney", menuName = "LevelData/Mission/MissionCollectMoney", order = 1)]
public class MissionCollectMoney : MissionBaseSO
{
    public float AmountTarget;
    public CurrencyType Currency;


    public override IMissionBehavior GetBehavior()
    {
        var behavior = new MissionMoneyCollectBehavior(Id, Reward, Currency);
        behavior.SetTarget(AmountTarget);
        return behavior;
    }
}