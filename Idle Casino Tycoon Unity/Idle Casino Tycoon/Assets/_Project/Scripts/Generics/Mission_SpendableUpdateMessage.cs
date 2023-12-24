using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Mission_SpendableUpdateMessage 
{
    public string SpendableId;
    public bool IsUnlocked;
    public int LevelUp;
    public Mission_SpendableUpdateMessage(string id, bool isUnlocked, int levelUp)
    {
        SpendableId = id;
        IsUnlocked = isUnlocked;
        LevelUp = levelUp;

    }
}
