using System;
using UnityEngine;

[Serializable]
public class WeaponStats
{
    [field: SerializeField] public Stat Attack { get; private set; }
    [field: SerializeField] public Stat Durability { get; private set; }
    [field: SerializeField] public Stat Arcane { get; private set; }
    [field: SerializeField] public Stat Potential { get; private set; }
    [field: SerializeField] public Stat AttackSpeed { get; private set; }
}
