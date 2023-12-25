using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GambleOption_Generator", menuName = "Config/Gamble/GambleOption_Generator", order = 2)]
public class GambleOption_Generator : GambleOptionSO
{
    [Header("Generator Reward")]
    public string GeneratorId;
    public MultiplierType MultiplierType;
    public float Multiplier;
    public float Duration;

    public void ApplyReward()
    {

    }


}