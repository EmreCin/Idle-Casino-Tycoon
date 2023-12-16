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
    public float UnlockPrice;

    public float Interval;
    public float GeneratingAmount;
    public float IdleCapacity;
}

public struct GeneratorDetail
{
    public float Interval;
    public float GeneratingAmount;
    public float IdleCapacity;

    public GeneratorDetail(float interval, float generatingAmount, float idleCapacity)
    {
        Interval = interval;
        GeneratingAmount = generatingAmount;
        IdleCapacity = idleCapacity;
    }
}