using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorativeModel 
{
    public string Id;
    public string Name;
    public int UnlockCost;
    public bool IsUnlocked;
    public CurrencyType UnlockCurrency;
    public List<MultiplierInstance> MultiplierInstanceList;
    public List<Sprite> SpriteList;
}
