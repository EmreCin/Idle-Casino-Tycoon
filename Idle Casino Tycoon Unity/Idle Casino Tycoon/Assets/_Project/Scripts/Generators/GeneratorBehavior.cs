using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class GeneratorBehavior 
{
    GeneratorModel model;

    float currentAmount;
    float currentTime;

    public GeneratorBehavior(GeneratorModel model)
    {
        this.model = model;
    }

    public void Generate(float addedTime)
    {
        if (currentAmount == model.IdleCapacity) return;

        currentTime += addedTime;

        if(currentTime >= model.Interval)
        {
            currentAmount = Mathf.Clamp(currentAmount + model.GeneratingAmount, 0, model.IdleCapacity);

            if (currentAmount != model.IdleCapacity) currentTime -= model.Interval;
            else currentTime = model.Interval;
        }

        MessageBroker.Default.Publish(new Generator_GenerateMessage(model.Id, Time.deltaTime, currentTime / model.Interval, currentAmount, currentAmount/model.IdleCapacity));

        Debug.LogError(model.Id + "- Generate --->" + currentAmount + " : " + currentTime);
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
