using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DecorativeDataBaseSO", menuName = "LevelData/DecorativeDataBaseSO", order = 4)]
public class DecorativeDataBaseSO : ScriptableObject
{
    [Header("Decorative")]
    public string Id;
    public string Name;
    [Header("Other Components")]
    public Vector3 DecorativePosition;
    public Transform Visual;
    [Header("Details")]
    public int UnlockCost;
    public bool IsUnlocked;
    public CurrencyType UnlockCurrency;
    [Header("Multiplier")]
    public List<MultiplierEffect> MultiplierEffectList;
    public List<string> EffectedGeneratorList;
    public List<Sprite> VisualOptions;
}