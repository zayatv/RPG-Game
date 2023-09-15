using UnityEngine;

public class PlayerBowChargedAttackingState : PlayerRangedAttackingState
{
    public PlayerBowChargedAttackingState(PlayerAttackingStateMachine playerAttackingStateMachine) : base(playerAttackingStateMachine)
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
