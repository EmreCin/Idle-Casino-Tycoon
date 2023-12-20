using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorFactory : IFactory<GeneratorController, GeneratorDataBaseSO, GeneratorModel>
{
    public GeneratorController Create(int level, GeneratorController controller, GeneratorDataBaseSO data, Wallet wallet,Transform parent)
    {
        GeneratorModel newModel = GetData(level, data, wallet);

        var newController = GameObject.Instantiate<GeneratorController>(controller,data.GeneratorPosition, Quaternion.identity, parent);
        newController.gameObject.name = newModel.Id;
        newController.Init(newModel);

        //Create other parts of the generator
        GameObject.Instantiate(data.Visual, newController.transform);

        GeneratorView view = GameObject.Instantiate(data.View, newController.transform);
        view.Init(newModel, newController);

        newController.AddButtonEvents(view.CollectButton, view.LevelUpButton, view.SelectButton);

        return newController;
    }

    public GeneratorModel GetData(int level, GeneratorDataBaseSO data, Wallet wallet)
    {
        GeneratorModel newModel = new GeneratorModel
        {
            Id = data.Id,
            Name = data.Name,
            Level = level,
            MaxLevel = data.MaxLevel,
            IsUnlocked = data.IsUnlocked,
            UnlockCost = data.UnlockPrice,
            UpgradeCurrency = wallet.GetCurrnecyById(data.UpgradeCurrency),
            GenerationCurrency = wallet.GetCurrnecyById(data.GenerationCurrency)
        };
        GeneratorDetail detail = data.GetDetail(level);

        newModel.Interval = detail.Interval;
        newModel.GeneratingAmount = detail.GeneratingAmount;
        newModel.IdleCapacity = detail.IdleCapacity;
        newModel.UpgradeCost = detail.UpgradeCost;

        return newModel;
    }
    
}



