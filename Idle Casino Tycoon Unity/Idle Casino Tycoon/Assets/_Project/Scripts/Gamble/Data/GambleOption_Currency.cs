using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GambleOption_Currency", menuName = "Config/Gamble/GambleOption_Currency", order = 2)]
public class GambleOption_Currency : GambleOptionSO
{
    public CurrencyType RewardCurrency;
    public float Reward;

    public void ApplyReward()
    {

    }


}