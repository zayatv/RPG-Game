using UnityEngine;

public class PlayerMovementAttackingState : PlayerGroundedState
{
    public PlayerMovementAttackingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.ReusableData.MovementSpeedModifier = 0f;

        base.Enter();

        stateMachine.ReusableData.CurrentJumpForce = Vector3.zero;

        ResetVelocity();
        DisableCameraRecentering();
        EnableWeaponObject();
    }
}
