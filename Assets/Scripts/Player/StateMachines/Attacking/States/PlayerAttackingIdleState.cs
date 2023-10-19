using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackingIdleState : PlayerAttackingState
{
    public PlayerAttackingIdleState(PlayerAttackingStateMachine playerAttackingStateMachine) : base(playerAttackingStateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Player.IsAttacking = false;

        base.Enter();

        StopAnimation(stateMachine.Player.AnimationData.AttackParameterHash);

        stateMachine.Player.MovementStateMachine.ChangeState(stateMachine.Player.MovementStateMachine.IdlingState);
    }

    public override void Exit()
    {
        stateMachine.Player.IsAttacking = true;

        base.Exit();

        StartAnimation(stateMachine.Player.AnimationData.AttackParameterHash);

        stateMachine.Player.MovementStateMachine.ChangeState(stateMachine.Player.MovementStateMachine.AttackingState);
    }

    protected override void AddInputActionsCallbacks()
    {
        base.AddInputActionsCallbacks();

        //stateMachine.Player.Input.PlayerActions.Attack.started += AddCanceledCallback;
        stateMachine.Player.Input.PlayerActions.Attack.started += OnStartAttack;
    }

    protected override void RemoveInputActionsCallbacks()
    {
        base.RemoveInputActionsCallbacks();

        //stateMachine.Player.Input.PlayerActions.Attack.started -= AddCanceledCallback;
        stateMachine.Player.Input.PlayerActions.Attack.started += OnStartAttack;
        stateMachine.Player.Input.PlayerActions.Attack.canceled += OnNormalAttackStarted;
    }

    /*private void AddCanceledCallback(InputAction.CallbackContext context)
    {
        stateMachine.Player.Input.PlayerActions.Attack.canceled += OnNormalAttackStarted;
    }*/

    private void OnStartAttack(InputAction.CallbackContext context)
    {
        stateMachine.Player.StartCoroutine(WaitForChargeAttack());
    }

    private IEnumerator WaitForChargeAttack()
    {
        stateMachine.Player.Input.PlayerActions.Attack.canceled += OnNormalAttackStarted;

        yield return new WaitForSeconds(attackData.MeleeNormalAttackingData.TimeToStartRegisterConcurrentAttack);

        stateMachine.Player.Input.PlayerActions.Attack.canceled -= OnNormalAttackStarted;

        attackData.ChargedAttackingData.TimeAfterEnteredChargedAttack = attackData.MeleeNormalAttackingData.TimeToStartRegisterConcurrentAttack;
        OnChargedAttackStarted();
    }
}
