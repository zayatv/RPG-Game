using System;
using UnityEngine;


[Serializable]
public class Stat
{
    [field: SerializeField] public float BaseValue { get; private set; }
    [field: SerializeField] public float FlatModifier { get; set; }
    [field: SerializeField] public float PercentageModifier { get; set; }
}
