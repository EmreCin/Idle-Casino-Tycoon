using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "MissionContainerSO", menuName = "LevelData/Mission/MissionContainerSO", order =0)]
public class MissionContainerSO : ScriptableObject
{
    public List<MissionBaseSO> MissionList;
}