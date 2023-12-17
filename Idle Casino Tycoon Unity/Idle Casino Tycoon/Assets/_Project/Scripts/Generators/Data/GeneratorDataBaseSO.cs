using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GeneratorDataBaseSO : ScriptableObject
{
    public string Id;
    public string Name;

    public int MaxLevel;
    public int UnlockPrice;
    public bool IsUnlocked;
    public CurrencyType UpgradeCurrency;
    public CurrencyType GenerationCurrency;

    public abstract GeneratorDetail GetDetail(int level);

}




