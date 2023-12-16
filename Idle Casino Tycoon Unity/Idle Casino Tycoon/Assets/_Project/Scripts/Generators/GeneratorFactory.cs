using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorFactory : IFactory<GeneratorController, GeneratorDataBaseSO, GeneratorModel>
{
    public void Create(int level, GeneratorController controller, GeneratorDataBaseSO data, Transform parent)
    {
        GeneratorModel newModel = GetData(level, data);

        var newController = GameObject.Instantiate<GeneratorController>(controller, parent);

        newController.gameObject.name = newModel.Id;

        newController.Init(newModel);
    }

    public GeneratorModel GetData(int level, GeneratorDataBaseSO data)
    {
        GeneratorModel newModel = new GeneratorModel
        {
            Id = data.Id,
            Name = data.Name,
            Level = level,
            MaxLevel = data.MaxLevel,
            IsUnlocked = data.IsUnlocked,
            UnlockPrice = data.UnlockPrice
        };
        GeneratorDetail detail = data.GetDetail(1);

        newModel.Interval = detail.Interval;
        newModel.GeneratingAmount = detail.GeneratingAmount;
        newModel.IdleCapacity = detail.IdleCapacity;

        return newModel;
    }
    
}



