using System;
using UnityEngine;

[Serializable]
public class MeleeNormalAttackingData
{
    [field: SerializeField] public float TimeToStartRegisterConcurrentAttack { get; private set; } = 0.2f;

    public float TimeSinceLastAttack { get; set; } = 0f;
}
