using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSwordAttackingState : PlayerAttackingState
{
    private PlayerSwordAttackData swordAttackData;

    private bool useNextConcurrentAttack;

    public PlayerSwordAttackingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        swordAttackData = movementData.SwordAttackData;
    }

    public override void Enter()
    {
        base.Enter();

        useNextConcurrentAttack = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void OnAnimationTransitionEvent()
    {
        stateMachine.Player.Input.PlayerActions.Attack.started += OnAttackStarted;
    }

    private void OnAttackStarted(InputAction.CallbackContext context)
    {
        useNextConcurrentAttack = true;
    }

    public override void OnAnimationExitEvent()
    {
        base.OnAnimationExitEvent();

        stateMachine.Player.Input.PlayerActions.Attack.started -= OnAttackStarted;

        if (useNextConcurrentAttack)
        {
            useNextConcurrentAttack = false;
            NextConcurrentAttack(swordAttackData.StartingSwordAttackAnimationId, swordAttackData.LastConcurrentSwordAttackAnimationId, stateMachine.Player.AnimationData.SwordAttackParameterName);
            return;
        }

        stateMachine.ChangeState(stateMachine.IdlingState);
    }
}
