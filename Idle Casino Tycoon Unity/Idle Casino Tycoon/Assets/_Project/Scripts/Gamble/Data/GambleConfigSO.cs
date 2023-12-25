using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GambleConfig", menuName = "Config/Gamble/GambleConfig", order = 1)]
public class GambleConfigSO : ScriptableObject
{
    public CurrencyType CostCurrency;
    public float Cost;
    public List<GambleOptionSO> OptionList;
}
