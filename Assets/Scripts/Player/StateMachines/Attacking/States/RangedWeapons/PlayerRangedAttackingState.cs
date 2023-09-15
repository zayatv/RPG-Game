public class PlayerRangedAttackingState : PlayerAttackingState
{
    protected bool isCharging;

    public PlayerRangedAttackingState(PlayerAttackingStateMachine playerAttackingStateMachine) : base(playerAttackingStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
}
