using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorBehavior 
{
    GeneratorModel model;
    public GeneratorBehavior(GeneratorModel model)
    {
        this.model = model;
    }

    public void Generate(float addedTime)
    {

    }

    public float Collect()
    {
        return 0;
    }

    public void LevelUp()
    {

    }

    public bool CheckForLevelUp()
    {
        return model.UpgradeCurrency.Amount >= model.UpgradeCost;
    }

    public bool CheckForUnlock()
    {
        return model.UpgradeCurrency.Amount >= model.UnlockCost;
    }
}
