using System;
using UnityEngine;

[Serializable]
public class PlayerSwordAttackData
{
    [field: SerializeField] public int StartingSwordAttackAnimationId { get; private set; } = 0;
    [field: SerializeField] public int LastConcurrentSwordAttackAnimationId { get; private set; } = 2;
}
