using System;
using UnityEngine;

[Serializable]
public class PlayerStats
{
    [field: SerializeField] public Stat Health { get; private set; }
    [field: SerializeField] public Stat Mana { get; private set; }
    [field: SerializeField] public Stat Strength { get; private set; }
    [field: SerializeField] public Stat Defense { get; private set; }
    [field: SerializeField] public Stat CriticalHits { get; private set; }
    [field: SerializeField] public Stat Agility { get; private set; }
    [field: SerializeField] public Stat MovementSpeed { get; private set; }
    [field: SerializeField] public Stat Luck { get; private set; }
    [field: SerializeField] public Stat Stamina { get; private set; }

    public void Initialize()
    {
        Health.CalculateTotalStatValue();
        Mana.CalculateTotalStatValue();
        Strength.CalculateTotalStatValue();
        Defense.CalculateTotalStatValue();
        CriticalHits.CalculateTotalStatValue();
        Agility.CalculateTotalStatValue();
        MovementSpeed.CalculateTotalStatValue();
        Luck.CalculateTotalStatValue();
        Stamina.CalculateTotalStatValue();
    }
}
