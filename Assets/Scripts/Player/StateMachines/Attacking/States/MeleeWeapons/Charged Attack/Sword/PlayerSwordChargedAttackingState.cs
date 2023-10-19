using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSwordChargedAttackingState : PlayerChargedAttackingState
{
    public PlayerSwordChargedAttackingState(PlayerAttackingStateMachine playerAttackingStateMachine) : base(playerAttackingStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("Charged Sword");
    }

    protected override void AddInputActionsCallbacks()
    {
        base.AddInputActionsCallbacks();

        stateMachine.Player.Input.PlayerActions.Attack.canceled += OnChargeCanceled;
    }

    protected override void RemoveInputActionsCallbacks()
    {
        base.RemoveInputActionsCallbacks();

        stateMachine.Player.Input.PlayerActions.Attack.canceled -= OnChargeCanceled;
    }

    public override void OnAnimationExitEvent()
    {
        base.OnAnimationEnterEvent();

        stateMachine.ChangeState(stateMachine.AttackingIdleState);
    }

    private void OnChargeCanceled(InputAction.CallbackContext context)
    {
        StopAnimation(stateMachine.Player.AnimationData.ChargeAttackParameterHash);
    }
}
