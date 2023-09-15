using UnityEngine.InputSystem;

public class PlayerMeleeAttackingState : PlayerAttackingState
{
    protected bool useNextConcurrentAttack;

    public PlayerMeleeAttackingState(PlayerAttackingStateMachine playerAttackingStateMachine) : base(playerAttackingStateMachine)
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
        stateMachine.Player.Input.PlayerActions.Attack.started += OnMeleeAttackStarted;
    }

    protected override void RemoveInputActionsCallbacks()
    {
        stateMachine.Player.Input.PlayerActions.Attack.started -= OnMeleeAttackStarted;
    }

    protected void OnMeleeAttackStarted(InputAction.CallbackContext context)
    {
        useNextConcurrentAttack = true;
    }
}