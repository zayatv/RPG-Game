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
        base.OnAnimationTransitionEvent();

        if (useNextConcurrentAttack)
        {
            useNextConcurrentAttack = false;
            NextConcurrentAttack(stateMachine.Player.AnimationData.SwordAttackParameterName);
           if (!IsNextAttackConcurrent(swordAttackData.StartingSwordAttackAnimationIndex, swordAttackData.LastConcurrentSwordAttackAnimationIndex, stateMachine.Player.AnimationData.SwordAttackParameterName))
           {
                stateMachine.Player.Input.PlayerActions.Attack.started -= OnAttackStarted;
           }
            return;
        }
    }

    public override void OnAnimationExitEvent()
    {
        base.OnAnimationExitEvent();

        if (useNextConcurrentAttack)
        {
            return;
        }
        
        ResetAnimationIndex(stateMachine.Player.AnimationData.SwordAttackParameterName);
        stateMachine.ChangeState(stateMachine.IdlingState);
    }

    protected override void AddInputActionsCallbacks()
    {
        stateMachine.Player.Input.PlayerActions.Attack.started += OnSwordAttackStarted;
    }

    protected override void RemoveInputActionsCallbacks()
    {
        stateMachine.Player.Input.PlayerActions.Attack.started -= OnSwordAttackStarted;
    }

    private void OnSwordAttackStarted(InputAction.CallbackContext context)
    {
        useNextConcurrentAttack = true;
    }
}
