using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "GeneratorDataByLevelSO", menuName = "LevelData/GeneratorDataByLevelSO", order = 3)]
public class GeneratorDataByLevelSO : GeneratorDataBaseSO
{
    public List<GeneratorLevelData> LevelDataList;

    public override GeneratorDetail GetDetail(int level)
    {
        GeneratorLevelData data = LevelDataList.Where(s => s.Level == level).FirstOrDefault();

        return new GeneratorDetail(data.Interval, data.GeneratingAmount, data.IdleCapacity);
    }
}

[System.Serializable]
public class GeneratorLevelData
{
    public int Level;
    public float IdleCapacity;
    public float Interval;
    public float GeneratingAmount;
}