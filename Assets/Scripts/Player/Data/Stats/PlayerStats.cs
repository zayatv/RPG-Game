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
        Health = new Stat();
        Mana = new Stat();
        Strength = new Stat();
        Defense = new Stat();
        CriticalHits = new Stat();
        Agility = new Stat();
        MovementSpeed = new Stat();
        Luck = new Stat();
        Stamina = new Stat();
    }
}
