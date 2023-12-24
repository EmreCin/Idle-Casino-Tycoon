using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Generator_UpdateMessage 
{
    public string GeneratorId;
    public bool IsUnlocked;
    public int LevelUp;
    public Generator_UpdateMessage(string id, bool isUnlocked, int levelUp)
    {
        GeneratorId = id;
        IsUnlocked = isUnlocked;
        LevelUp = levelUp;

    }
}
