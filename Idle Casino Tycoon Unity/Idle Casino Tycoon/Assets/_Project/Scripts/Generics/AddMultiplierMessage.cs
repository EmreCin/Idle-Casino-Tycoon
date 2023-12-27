using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AddMultiplierMessage 
{
    public List<MultiplierInstance> MultiplierList;

    public AddMultiplierMessage(List<MultiplierInstance> mList)
    {
        MultiplierList = mList;
    }
}
