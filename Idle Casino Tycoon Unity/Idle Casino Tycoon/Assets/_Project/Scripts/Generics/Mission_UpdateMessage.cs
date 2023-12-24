using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Mission_UpdateMessage 
{
    public int Id;
    public float FillAmount;

    public Mission_UpdateMessage(int id, float fill)
    {
        Id = id;
        FillAmount = fill;
    }
}
