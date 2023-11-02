using System.Collections.Generic;
using UnityEngine;

public class PlayerStateReusableData
{
    public Vector2 MovementInput { get; set; }

    public float MovementSpeedModifier { get; set; } = 1f;
    public float MovementOnSlopesSpeedModifier { get; set; } = 1f;
    public float MovementDecelerationForce { get; set; } = 1f;

    public float TimeToBeConcurrentSwordAttack { get; set; } = 1f;
    public int CurrentSwordAttack { get; set; }

    public List<PlayerCameraRecenteringData> BackwardsCameraRecenteringData { get; set; }
    public List<PlayerCameraRecenteringData> SidewaysCameraRecenteringData { get; set; }
    
    public bool ShouldWalk { get; set; }
    public bool ShouldSprint { get; set; }

    public bool CanAirJump { get; set; } = true;

    private Vector3 currentTargetRotation;
    private Vector3 timeToReachTargetRotation;
    private Vector3 dampedTargetRotationCurrentVelocity;
    private Vector3 dampedTargetRotationPassedTime;


    public ref Vector3 CurrentTargetRotation
    {
        get
        {
            return ref currentTargetRotation;
        }
    }

    public ref Vector3 TimeToReachTargetRotation
    {
        get
        {
            return ref timeToReachTargetRotation;
        }
    }

    public ref Vector3 DampedTargetRotationCurrentVelocity
    {
        get
        {
            return ref dampedTargetRotationCurrentVelocity;
        }
    }

    public ref Vector3 DampedTargetRotationPassedTime
    {
        get
        {
            return ref dampedTargetRotationPassedTime;
        }
    }

    public Vector3 CurrentJumpForce { get; set; }

    public PlayerRotationData RotationData { get; set; }
}
