using UnityEngine.InputSystem;

public class PlayerMeleeNormalAttackingState : PlayerNormalAttackingState
{
    protected bool useNextConcurrentAttack;

    public PlayerMeleeNormalAttackingState(PlayerAttackingStateMachine playerAttackingStateMachine) : base(playerAttackingStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        useNextConcurrentAttack = false;

        EnableWeaponObject();
    }

    protected override void AddInputActionsCallbacks()
    {
        stateMachine.Player.Input.PlayerActions.Attack.canceled += OnMeleeAttackStarted;
    }

    protected override void RemoveInputActionsCallbacks()
    {
        stateMachine.Player.Input.PlayerActions.Attack.canceled -= OnMeleeAttackStarted;
    }

    protected void OnMeleeAttackStarted(InputAction.CallbackContext context)
    {
        useNextConcurrentAttack = true;
    }
}
