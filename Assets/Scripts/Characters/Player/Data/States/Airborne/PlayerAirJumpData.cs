using System;
using UnityEngine;

[Serializable]
public class PlayerAirJumpData
{
    [field: SerializeField] public float TimeToAirJump { get; private set; } = 0.1f;
    [field: SerializeField] public Vector3 JumpForce { get; private set; }
    [field: SerializeField][field: Range(0f, 10f)] public float DecelerationForce { get; private set; } = 1.5f;
}
