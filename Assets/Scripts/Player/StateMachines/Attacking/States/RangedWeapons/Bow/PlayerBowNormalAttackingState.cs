using UnityEngine;

public class PlayerBowNormalAttackingState : PlayerRangedAttackingState
{
    public PlayerBowNormalAttackingState(PlayerAttackingStateMachine playerAttackingStateMachine) : base(playerAttackingStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("Normal Attack");
    }
}
