using UnityEngine;

public class PlayerBowChargedAttackingState : PlayerBowAttackingState
{
    public PlayerBowChargedAttackingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("Charged Attack");
    }

    protected override void AddInputActionsCallbacks()
    {
        stateMachine.Player.Input.PlayerActions.Attack.canceled -= OnNormalAttackStarted;
    }
}
