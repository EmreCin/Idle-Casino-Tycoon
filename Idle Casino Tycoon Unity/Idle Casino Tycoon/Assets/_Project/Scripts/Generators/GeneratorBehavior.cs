using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

public class GeneratorBehavior 
{
    GeneratorModel model;

    float currentAmount;
    public float CurrentAmount => currentAmount;
    float currentTime;

    float speedMultiplier=1;
    float amountMultiplier=1;

    private CompositeDisposable disposables = new CompositeDisposable();

    public GeneratorBehavior(GeneratorModel model)
    {
        this.model = model;
        MessageBroker.Default.Receive<MultiplierMessage>().Subscribe(((x) => { CheckMultiplier(x); })).AddTo(disposables);
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

            if (currentTime >= (model.Interval * speedMultiplier))
            {
                currentAmount = Mathf.Clamp(currentAmount + (model.GeneratingAmount * amountMultiplier), 0, model.IdleCapacity);

                if (currentAmount != model.IdleCapacity) currentTime -= (model.Interval * speedMultiplier);
                else currentTime = (model.Interval * speedMultiplier);
            }
        }
        

        MessageBroker.Default.Publish(new Generator_GenerateMessage(model.Id, Time.deltaTime, currentTime / (model.Interval * speedMultiplier), currentAmount, currentAmount/model.IdleCapacity, amountMultiplier, speedMultiplier));

        //Debug.LogError(model.Id + "- Generate --->" + currentAmount + " : " + currentTime);
    }

    void CheckMultiplier(MultiplierMessage message)
    {
        var multipliers = message.MultiplierList.Where(s=> s.Id == model.Id).ToList();

        amountMultiplier = 1;
        speedMultiplier = 1;

        foreach (var m in multipliers)
        {
            if (m.MultiplierType == MultiplierType.Amount)
                amountMultiplier += m.Multiplier;
            else if (m.MultiplierType == MultiplierType.Speed)
                speedMultiplier -= m.Multiplier;
        }
    }

    public float Collect()
    {
        var newAmount = currentAmount;
        currentAmount = 0;
        return newAmount;
    }

    public bool CheckForLevelUp()
    {
        if (model.MaxLevel == model.Level) return false;

        return model.UpgradeCurrency.Amount >= model.UpgradeCost;
    }
    public bool CheckForUnlock()
    {
        if (model.IsUnlocked) return false;

        return model.UpgradeCurrency.Amount >= model.UnlockCost;
    }
}
