using UnityEngine.InputSystem;

public class PlayerRangedAttackingState : PlayerAttackingState
{

    public PlayerRangedAttackingState(PlayerAttackingStateMachine playerAttackingStateMachine) : base(playerAttackingStateMachine)
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

    protected void OnNormalAttackStarted(InputAction.CallbackContext context)
    {
        switch (stateMachine.Player.CurrentEquippedWeapon.WeaponType)
        {
            case WeaponType.Bow:
                stateMachine.ChangeState(stateMachine.BowNormalAttackingState);
                break;
            case WeaponType.Wand:
                break;
            default:
                break;
        }
    }

    protected void OnChargedAttackStarted(InputAction.CallbackContext context)
    {
        switch (stateMachine.Player.CurrentEquippedWeapon.WeaponType)
        {
            case WeaponType.Bow:
                stateMachine.ChangeState(stateMachine.BowChargedAttackingState);
                break;
            case WeaponType.Wand:
                break;
            default:
                break;
        }
    }

    public override void OnAnimationExitEvent()
    {
        base.OnAnimationExitEvent();

        stateMachine.ChangeState(stateMachine.AttackingIdleState);
    }
}
