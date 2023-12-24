using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MissionCollectMoney", menuName = "LevelData/Mission/MissionCollectMoney", order = 1)]
public class MissionCollectMoney : MissionBaseSO
{
    [Header("Mission Target")]
    public float AmountTarget;
    public CurrencyType Currency;


    public override IMissionBehavior GetBehavior()
    {
        return new MissionMoneyCollectBehavior(Id, Reward, RewardCurrencyType, Currency, AmountTarget); ;
    }
}