using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAirJumpingState : PlayerAirborneState
{
    private bool canStartFalling;

    public PlayerAirJumpingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        StartAnimation(stateMachine.Player.AnimationData.AirJumpParameterHash);

        stateMachine.ReusableData.MovementSpeedModifier = 0f;

        stateMachine.ReusableData.CanAirJump = false;

        stateMachine.ReusableData.MovementDecelerationForce = airJumpData.DecelerationForce;

        Jump();
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.AnimationData.AirJumpParameterHash);

        SetBaseRotationData();

        canStartFalling = false;
    }

    public override void Update()
    {
        base.Update();

        if (!canStartFalling && IsMovingUp(0f))
        {
            canStartFalling = true;
        }

        if (!canStartFalling || GetPlayerVerticalVelocity().y > 0)
        {
            return;
        }

        stateMachine.ChangeState(stateMachine.FallingState);
    }

    public override void PhysicsUpdate()
    {
        if (IsMovingUp())
        {
            DecelerateVertically();
        }
    }

    protected override void ResetSprintState()
    {

    }

    private void Jump()
    {
        Vector3 jumpForce = stateMachine.ReusableData.CurrentJumpForce;

        Vector3 jumpDirection = stateMachine.Player.transform.forward;

        jumpForce.x *= jumpDirection.x;
        jumpForce.z *= jumpDirection.z;

        ResetVelocity();

        stateMachine.Player.Rigidbody.AddForce(jumpForce, ForceMode.VelocityChange);
    }

    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {
    }
}
