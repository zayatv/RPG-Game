using System;
using UnityEngine;

[Serializable]
public class PlayerSwordAttackData
{
    [field: SerializeField] public int StartingSwordAttackAnimationIndex { get; private set; } = 0;
    [field: SerializeField] public int LastConcurrentSwordAttackAnimationIndex { get; private set; } = 2;
}
