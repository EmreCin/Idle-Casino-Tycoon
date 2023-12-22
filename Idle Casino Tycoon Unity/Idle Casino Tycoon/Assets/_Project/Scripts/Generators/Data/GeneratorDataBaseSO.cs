using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GeneratorDataBaseSO : ScriptableObject
{
    [Header("Generator")]
    public string Id;
    public string Name;
    [Header("Other Components")]
    public Vector3 GeneratorPosition;
    public Transform Visual;
    public GeneratorView View;
    [Header("Details")]
    public int MaxLevel;
    public int UnlockCost;
    public bool IsUnlocked;
    public CurrencyType UpgradeCurrency;
    public CurrencyType GenerationCurrency;
    

    public abstract GeneratorDetail GetDetail(int level);

}




