public class PlayerRangedNormalAttackingState : PlayerRangedAttackingState
{
    public PlayerRangedNormalAttackingState(PlayerAttackingStateMachine playerAttackingStateMachine) : base(playerAttackingStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //EnableWeaponObject();
    }

    public override void OnAnimationExitEvent()
    {
        base.OnAnimationExitEvent();

        stateMachine.ChangeState(stateMachine.AttackingIdleState);
    }
}
