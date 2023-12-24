using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MissionBaseSO : ScriptableObject
{
    public int Id;
    public string Name;
    public string Desc;
    public CurrencyType RewardCurrencyType;
    public float Reward;

    public abstract IMissionBehavior GetBehavior();
}

