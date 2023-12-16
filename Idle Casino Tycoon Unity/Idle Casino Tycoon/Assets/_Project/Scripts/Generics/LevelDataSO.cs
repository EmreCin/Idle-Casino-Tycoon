using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelDataSO", menuName = "LevelData/LevelDataSO", order = 1)]
public class LevelDataSO : ScriptableObject
{
    public GeneratorDataContainerSO LevelGenerator;
}
