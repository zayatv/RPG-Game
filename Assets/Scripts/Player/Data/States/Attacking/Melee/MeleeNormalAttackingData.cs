using System;
using UnityEngine;

[Serializable]
public class MeleeNormalAttackingData
{
    [field: SerializeField] public float TimeToStartRegisterConcurrentAttack { get; private set; } = 0.1f;
    [field: SerializeField] public float TimeToEndRegisterConcurrentAttack { get; private set; } = 1.5f;
    [field: SerializeField] public int LastAnimationIndex { get; private set; } = 0;
}
