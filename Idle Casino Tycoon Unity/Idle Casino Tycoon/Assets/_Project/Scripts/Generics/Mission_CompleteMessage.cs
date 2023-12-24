using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Mission_CompleteMessage 
{
    public int Id;
    public bool IsClaimed;
    public Mission_CompleteMessage(int id, bool isClaimed)
    {
        Id = id;
        IsClaimed = isClaimed;
    }
}
