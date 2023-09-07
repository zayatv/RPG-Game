public class PlayerRangedAttackingState : PlayerAttackingState
{
    protected bool isCharging;

    public PlayerRangedAttackingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        useNextConcurrentAttack = false;
    }
}
