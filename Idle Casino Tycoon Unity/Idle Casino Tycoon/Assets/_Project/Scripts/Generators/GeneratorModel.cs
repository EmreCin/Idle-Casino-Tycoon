using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorModel
{
    public string Id;
    public string Name;

    public int Level;
    public int MaxLevel;
    public bool IsUnlocked;
    public float UnlockCost;

    public Currency UpgradeCurrency;
    public Currency GenerationCurrency;
    public float UpgradeCost;

    public float Interval;
    public float GeneratingAmount;
    public float IdleCapacity;
}

public struct GeneratorDetail
{
    public float Interval;
    public float GeneratingAmount;
    public float IdleCapacity;
    public float UpgradeCost;

    public GeneratorDetail(float interval, float generatingAmount, float idleCapacity, float upgradeCost)
    {
        Interval = interval;
        GeneratingAmount = generatingAmount;
        IdleCapacity = idleCapacity;
        UpgradeCost = upgradeCost;
    }
}