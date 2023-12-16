using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorFactory : IFactory<GeneratorController, GeneratorDataBaseSO, Transform>
{
    public void Create(GeneratorController controller, GeneratorDataBaseSO data, Transform parent)
    {
        GeneratorModel newModel = new GeneratorModel
        {
            Id = data.Id,
            Name = data.Name,
            Level = 1,
            MaxLevel = data.MaxLevel,
            IsUnlocked = data.IsUnlocked,
            UnlockPrice = data.UnlockPrice
        };
        GeneratorDetail detail = data.GetDetail(1);

        newModel.Interval = detail.Interval;
        newModel.GeneratingAmount = detail.GeneratingAmount;
        newModel.IdleCapacity = detail.IdleCapacity;

        var newController = GameObject.Instantiate<GeneratorController>(controller, parent);

        newController.gameObject.name = newModel.Id;

        newController.Init(newModel);
    }
}



