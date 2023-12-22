using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Generator_GenerateMessage
{
    public string Id;
    public float TimePassed;
    public float FillAmount;
    public float GeneratedAmount;
    public float CapacityFillAmount;
    public float AmountMultiplier;
    public float SpeedMultiplier;

    public Generator_GenerateMessage(string id, float timePassed, float fillAmount, float generatedAmount, float capacityFillAmount, float amountMultiplier, float speedMultiplier)
    {
        Id = id;
        TimePassed = timePassed;
        FillAmount = fillAmount;
        GeneratedAmount = generatedAmount;
        CapacityFillAmount = capacityFillAmount;
        AmountMultiplier = amountMultiplier;
        SpeedMultiplier = speedMultiplier;
    }
}