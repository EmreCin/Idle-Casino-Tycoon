using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class GeneratorBehavior 
{
    GeneratorModel model;

    float currentAmount;
    public float CurrentAmount => currentAmount;
    float currentTime;

    public GeneratorBehavior(GeneratorModel model)
    {
        this.model = model;
    }

    public void UpdateModel(GeneratorModel model)
    {
        this.model = model;
    }

    public void Generate(float addedTime)
    {
        if (currentAmount != model.IdleCapacity)
        {
            currentTime += addedTime;

            if (currentTime >= model.Interval)
            {
                currentAmount = Mathf.Clamp(currentAmount + model.GeneratingAmount, 0, model.IdleCapacity);

                if (currentAmount != model.IdleCapacity) currentTime -= model.Interval;
                else currentTime = model.Interval;
            }
        }
        

        MessageBroker.Default.Publish(new Generator_GenerateMessage(model.Id, Time.deltaTime, currentTime / model.Interval, currentAmount, currentAmount/model.IdleCapacity));

        //Debug.LogError(model.Id + "- Generate --->" + currentAmount + " : " + currentTime);
    }

    public float Collect()
    {
        var newAmount = currentAmount;
        currentAmount = 0;
        return newAmount;
    }

    public void LevelUp()
    {

    }

    public bool CheckForLevelUp()
    {
        if (model.MaxLevel == model.Level) return false;

        return model.UpgradeCurrency.Amount >= model.UpgradeCost;
    }

    public bool CheckForUnlock()
    {
        return model.UpgradeCurrency.Amount >= model.UnlockCost;
    }
}
