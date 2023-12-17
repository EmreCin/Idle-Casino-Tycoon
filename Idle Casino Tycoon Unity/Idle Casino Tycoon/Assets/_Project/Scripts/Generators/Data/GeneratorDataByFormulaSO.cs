using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GeneratorDataByFormulaSO", menuName = "LevelData/GeneratorDataByFormulaSO", order = 3)]
public class GeneratorDataByFormulaSO : GeneratorDataBaseSO
{
    public float IdleCapacity_Base;
    public float IdleCapacity_LevelMultiplier;
    public float Interval_Base;
    public float Interval_LevelMultiplier;
    public float GeneratingAmount_Base;
    public float GeneratingAmount_LevelMultiplier;
    public float UpgradeCost_Base;
    public float UpgradeCost_LevelMultiplier;

    public override GeneratorDetail GetDetail(int level)
    {
        GeneratorDetail detail = new GeneratorDetail(Interval_Base, GeneratingAmount_Base, IdleCapacity_Base, UpgradeCost_Base);

        for (int i = 2; i <= level; i++)
        {
            detail.Interval -= (Interval_Base * Interval_LevelMultiplier) - Interval_Base;
            detail.IdleCapacity += (IdleCapacity_Base * IdleCapacity_LevelMultiplier) - IdleCapacity_Base;
            detail.GeneratingAmount += (GeneratingAmount_Base * GeneratingAmount_LevelMultiplier) - GeneratingAmount_Base;
            detail.UpgradeCost += (UpgradeCost_Base * UpgradeCost_LevelMultiplier) - UpgradeCost_Base;
        }

        return detail;
    }
}
