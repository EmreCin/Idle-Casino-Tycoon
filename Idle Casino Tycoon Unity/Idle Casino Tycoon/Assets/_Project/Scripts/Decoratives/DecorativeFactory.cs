using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorativeFactory : MonoBehaviour
{
    public DecorativeItemView Create(DecorativeDataBaseSO data, DecorativeItemView viewPrefab, Transform parent)
    {
        DecorativeModel newModel = GetData(data);

        var view = GameObject.Instantiate<DecorativeItemView>(viewPrefab, data.DecorativePosition, Quaternion.identity, parent);
        view.gameObject.name += "_"+newModel.Id;
        view.Init(newModel);

        return view;
    }

    public DecorativeModel GetData(DecorativeDataBaseSO data)
    {
        DecorativeModel newModel= new DecorativeModel()
        {
            Id = data.Id,
            Name = data.Name,
            Desc = data.Desc,
            UnlockCost = data.UnlockCost,
            IsUnlocked = data.IsUnlocked,
            UnlockCurrency = data.UnlockCurrency,
            MultiplierInstanceList = new List<MultiplierInstance>(),
            SpriteList = data.VisualOptions,
            VisualPrefab = data.Visual,
            VisualPosition = data.DecorativePosition
        };

        foreach (var effect in data.MultiplierEffectList)
        {
            if(data.EffectedGeneratorList.Count == 0)
            {
                var newMultiplier = new MultiplierInstance("", effect.MultiplierType, effect.Multiplier, 0);

                newModel.MultiplierInstanceList.Add(newMultiplier);
            }
            else
            {
                foreach (var id in data.EffectedGeneratorList)
                {
                    var newMultiplier = new MultiplierInstance(id, effect.MultiplierType, effect.Multiplier, 0);

                    newModel.MultiplierInstanceList.Add(newMultiplier);
                }
            }
            
        }

        return newModel;
    }
}
