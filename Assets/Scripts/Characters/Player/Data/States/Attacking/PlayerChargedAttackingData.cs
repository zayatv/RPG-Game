using System;
using UnityEngine;

[Serializable]
public class PlayerChargedAttackingData
{
    [field: SerializeField][field: Range(0f, 1f)] public float SpeedModifier { get; private set; } = 0.255f;
}
