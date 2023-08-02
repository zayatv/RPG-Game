using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSprintingState : PlayerMovingState
{
    private PlayerSprintData sprintData;

    private float startTime;

    private bool keepSprinting;

    public PlayerSprintingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        sprintData = movementData.SprintData;
    }

    public override void Enter()
    {
        base.Enter();

        stateMachine.ReusableData.MovementSpeedModifier = sprintData.SpeedModifier;

        stateMachine.ReusableData.CurrentJumpForce = airborneData.JumpData.StrongForce;

        startTime = Time.time;
    }

    public override void Exit()
    {
        base.Exit();

        keepSprinting = false;
    }

    public override void Update()
    {
        base.Update();

        if (keepSprinting)
        {
            return;
        }

        if (Time.time < startTime + sprintData.SprintToRunTime)
        {
            return;
        }

        StopSprinting();
    }

    private void StopSprinting()
    {
        if (stateMachine.ReusableData.MovementInput == Vector2.zero)
        {
            stateMachine.ChangeState(stateMachine.IdlingState);

            return;
        }

        stateMachine.ChangeState(stateMachine.RunningState);
    }

    protected override void AddInputActionsCallbacks()
    {
        base.AddInputActionsCallbacks();

        stateMachine.Player.Input.PlayerActions.Sprint.performed += OnSprintPerformed;
    }

    protected override void RemoveInputActionsCallbacks()
    {
        base.RemoveInputActionsCallbacks();

        stateMachine.Player.Input.PlayerActions.Sprint.performed -= OnSprintPerformed;
    }

    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.HardStoppingState);
    }

    private void OnSprintPerformed(InputAction.CallbackContext context)
    {
        keepSprinting = true;
    }
}