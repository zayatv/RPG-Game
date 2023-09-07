using UnityEngine;

public class PlayerBowNormalAttackingState : PlayerBowAttackingState
{
    public PlayerBowNormalAttackingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("Normal Attack");
    }
}
