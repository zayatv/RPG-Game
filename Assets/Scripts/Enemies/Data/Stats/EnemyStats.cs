using System;
using UnityEngine;

[Serializable]
public class EnemyStats
{
    [field: SerializeField] public Stat Health { get; private set; }

    public void Initialize()
    {
        Health.CalculateTotalStatValue();
    }

}
