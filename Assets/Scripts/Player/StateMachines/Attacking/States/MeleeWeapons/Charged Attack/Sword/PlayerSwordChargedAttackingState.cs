using UnityEngine;

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

    public override void OnAnimationExitEvent()
    {
        base.OnAnimationEnterEvent();

        stateMachine.ChangeState(stateMachine.AttackingIdleState);
    }
}
