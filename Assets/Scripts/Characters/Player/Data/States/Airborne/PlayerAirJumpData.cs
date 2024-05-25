using System;
using UnityEngine;

[Serializable]
public class PlayerAirJumpData
{
    [field: SerializeField] public float TimeToAirJump { get; private set; } = 0.1f;
    [field: SerializeField] public Vector3 StationaryJumpForce { get; private set; }
    [field: SerializeField] public Vector3 WeakJumpForce { get; private set; }
    [field: SerializeField] public Vector3 StrongJumpForce { get; private set; }
    [field: SerializeField][field: Range(0f, 10f)] public float DecelerationForce { get; private set; } = 1.5f;
}
