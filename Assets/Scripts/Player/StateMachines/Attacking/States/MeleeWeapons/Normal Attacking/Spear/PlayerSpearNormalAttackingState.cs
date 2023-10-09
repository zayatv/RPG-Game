public class PlayerSpearNormalAttackingState : PlayerMeleeNormalAttackingState
{
    private PlayerSpearAttackData spearAttackData;

    public PlayerSpearNormalAttackingState(PlayerAttackingStateMachine playerAttackingStateMachine) : base(playerAttackingStateMachine)
    {
        spearAttackData = attackData.SpearAttackData;
    }

    public override void Enter()
    {
        base.Enter();

        StartAnimation(stateMachine.Player.AnimationData.SpearEquippedParameterHash);
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.AnimationData.SpearEquippedParameterHash);
    }

    public override void OnAnimationExitEvent()
    {
        base.OnAnimationExitEvent();

        if (useNextConcurrentAttack)
        {
            return;
        }

        ResetAnimationIndex(stateMachine.Player.AnimationData.SpearAttackParameterName);
        stateMachine.ChangeState(stateMachine.AttackingIdleState);
    }

    public override void OnAnimationTransitionEvent()
    {
        base.OnAnimationTransitionEvent();

        if (useNextConcurrentAttack)
        {
            useNextConcurrentAttack = false;
            NextConcurrentAttack(stateMachine.Player.AnimationData.SpearAttackParameterName);
            if (!IsNextAttackConcurrent(spearAttackData.StartingSpearAttackAnimationIndex, spearAttackData.LastConcurrentSpearAttackAnimationIndex, stateMachine.Player.AnimationData.SpearAttackParameterName))
            {
                stateMachine.Player.Input.PlayerActions.Attack.started -= OnMeleeAttackStarted;
                stateMachine.Player.Input.PlayerActions.Attack.started -= OnNormalAttackStarted;
            }
            return;
        }

        stateMachine.Player.Input.PlayerActions.Attack.started -= OnMeleeAttackStarted;
    }

}
