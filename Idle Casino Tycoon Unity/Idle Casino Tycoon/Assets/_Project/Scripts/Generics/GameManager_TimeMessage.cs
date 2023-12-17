using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_TimeMessage
{
    public float TimePassed;

    public GameManager_TimeMessage(float timePassed)
    {
        TimePassed = timePassed;
    }
}

public struct Generator_GenerateMessage
{
    public string Id;
    public float TimePassed;
    public float FillAmount;
    public float GeneratedAmount;
    public float CapacityFillAmount;

    public Generator_GenerateMessage(string id, float timePassed, float fillAmount, float generatedAmount, float capacityFillAmount)
    {
        Id = id;
        TimePassed = timePassed;
        FillAmount = fillAmount;
        GeneratedAmount = generatedAmount;
        CapacityFillAmount = capacityFillAmount;
    }
}