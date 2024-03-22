using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFallingState : PlayerAirborneState
{
    private PlayerFallData fallData;

    private Vector3 playerPositionOnEnter;

    private float startTime;

    public PlayerFallingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        fallData = airborneData.FallData;
    }

    public override void Enter()
    {
        base.Enter();

        StartAnimation(stateMachine.Player.AnimationData.FallParameterHash);

        playerPositionOnEnter = stateMachine.Player.transform.position;

        stateMachine.ReusableData.MovementSpeedModifier = 0f;

        ResetVerticalVelocity();

        startTime = Time.time;
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.AnimationData.FallParameterHash);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        LimitVerticalVelocity();
    }

    protected override void AddInputActionsCallbacks()
    {
        base.AddInputActionsCallbacks();

        stateMachine.Player.Input.PlayerActions.Jump.started += OnAirJump;
    }

    protected override void RemoveInputActionsCallbacks()
    {
        base.RemoveInputActionsCallbacks();

        stateMachine.Player.Input.PlayerActions.Jump.started -= OnAirJump;
    }

    protected override void ResetSprintState()
    {
    }

    protected override void OnContactWithGround(Collider collider)
    {
        float fallDistance = playerPositionOnEnter.y - stateMachine.Player.transform.position.y;

        if (fallDistance < fallData.MinimumDistanceToBeConsideredHardFall)
        {
            stateMachine.ChangeState(stateMachine.LightLandingState);
            return;
        }

        //Add Fall Damage here

        if (stateMachine.ReusableData.ShouldWalk && !stateMachine.ReusableData.ShouldSprint || stateMachine.ReusableData.MovementInput == Vector2.zero)
        {
            stateMachine.ChangeState(stateMachine.HardLandingState);
            return;
        }

        stateMachine.ChangeState(stateMachine.RollingState);
    }

    private void LimitVerticalVelocity()
    {
        Vector3 playerVerticalVelocity = GetPlayerVerticalVelocity();

        if (playerVerticalVelocity.y >= -fallData.FallSpeedLimit)
        {
            return;
        }

        Vector3 limitedVelocity = new Vector3(0f, -fallData.FallSpeedLimit - playerVerticalVelocity.y, 0f);

        stateMachine.Player.Rigidbody.AddForce(limitedVelocity, ForceMode.VelocityChange);
    }

    private void OnAirJump(InputAction.CallbackContext context)
    {
        if ((Time.time - airJumpData.TimeToAirJump) < startTime || !stateMachine.ReusableData.CanAirJump) return;
        stateMachine.ChangeState(stateMachine.AirJumpingState);
    }
}
