using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DecorativeDataContainerSO", menuName = "LevelData/DecorativeDataContainerSO", order = 3)]
public class DecorativeDataContainerSO : ScriptableObject
{
    public List<DecorativeDataBaseSO> DecorativeList;
}
