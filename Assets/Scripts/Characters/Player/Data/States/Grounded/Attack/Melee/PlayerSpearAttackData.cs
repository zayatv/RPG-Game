using System;
using UnityEngine;


[Serializable]
public class PlayerSpearAttackData
{
    [field: SerializeField] public int StartingSpearAttackAnimationIndex { get; private set; } = 0;
    [field: SerializeField] public int LastConcurrentSpearAttackAnimationIndex { get; private set; } = 1;
}
