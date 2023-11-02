using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStoppingState : PlayerGroundedState
{
    public PlayerStoppingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.ReusableData.MovementSpeedModifier = 0f;

        SetBaseCameraRecenteringData();
        
        base.Enter();

        StartAnimation(stateMachine.Player.AnimationData.StoppingParameterHash);

        stateMachine.Player.CanAttack = true;
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.AnimationData.StoppingParameterHash);

        stateMachine.Player.CanAttack = false;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        RotateTowardsTargetRotation();

        if (!IsMovingHorizontally())
        {
            return;
        }

        DecelerateHorizontally();
    }

    public override void OnAnimationTransitionEvent()
    {
        stateMachine.ChangeState(stateMachine.IdlingState);
    }

    protected override void AddInputActionsCallbacks()
    {
        base.AddInputActionsCallbacks();

        stateMachine.Player.Input.PlayerActions.Movement.started += OnMovementStarted;
        //stateMachine.Player.Input.PlayerActions.Attack.started += OnAttackStarted;
    }

    protected override void RemoveInputActionsCallbacks()
    {
        base.RemoveInputActionsCallbacks();

        stateMachine.Player.Input.PlayerActions.Movement.started -= OnMovementStarted;
        //stateMachine.Player.Input.PlayerActions.Attack.started -= OnAttackStarted;
    }

    private void OnMovementStarted(InputAction.CallbackContext context)
    {
        OnMove();
    }
}
