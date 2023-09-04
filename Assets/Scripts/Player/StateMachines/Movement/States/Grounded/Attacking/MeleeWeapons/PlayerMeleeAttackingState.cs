using UnityEngine.InputSystem;

public class PlayerMeleeAttackingState : PlayerAttackingState
{
    protected bool useNextConcurrentAttack;

    public PlayerMeleeAttackingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        useNextConcurrentAttack = false;
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
