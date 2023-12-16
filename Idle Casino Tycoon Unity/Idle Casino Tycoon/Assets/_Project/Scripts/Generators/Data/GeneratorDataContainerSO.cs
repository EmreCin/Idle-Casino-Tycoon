using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GeneratorDataContainerSO", menuName = "LevelData/GeneratorDataContainerSO", order = 2)]
public class GeneratorDataContainerSO : ScriptableObject
{
    public List<GeneratorDataBaseSO> GeneratorList;
}
