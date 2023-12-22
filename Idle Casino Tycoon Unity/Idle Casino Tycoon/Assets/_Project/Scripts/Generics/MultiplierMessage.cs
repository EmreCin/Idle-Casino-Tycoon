using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MultiplierMessage
{
    public List<MultiplierInstance> MultiplierList;

    public MultiplierMessage(List<MultiplierInstance> multiplierlist)
    {
        MultiplierList = multiplierlist;
    }
}
