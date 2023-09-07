using System;
using UnityEngine.InputSystem;

public class PlayerBowAttackingState : PlayerRangedAttackingState
{
    public PlayerBowAttackingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    protected override void AddInputActionsCallbacks()
    {
        stateMachine.Player.Input.PlayerActions.Attack.canceled += OnNormalAttackStarted;

        stateMachine.Player.Input.PlayerActions.ChargedAttack.performed += OnChargedAttackStarted;
    }

    protected override void RemoveInputActionsCallbacks()
    {
        stateMachine.Player.Input.PlayerActions.Attack.canceled -= OnNormalAttackStarted;

        stateMachine.Player.Input.PlayerActions.ChargedAttack.performed -= OnChargedAttackStarted;
    }

    public override void OnAnimationExitEvent()
    {
        base.OnAnimationExitEvent();

        stateMachine.ChangeState(stateMachine.IdlingState);
    }

    protected void OnNormalAttackStarted(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.BowNormalAttackingState);
    }

    protected void OnChargedAttackStarted(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.BowChargedAttackingState);
    }
}
