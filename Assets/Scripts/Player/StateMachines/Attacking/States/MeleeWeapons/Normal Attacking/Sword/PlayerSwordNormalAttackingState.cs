public class PlayerSwordNormalAttackingState : PlayerMeleeNormalAttackingState
{
    private PlayerSwordAttackData swordAttackData;

    public PlayerSwordNormalAttackingState(PlayerAttackingStateMachine playerAttackingStateMachine) : base(playerAttackingStateMachine)
    {
        swordAttackData = attackData.SwordAttackData;
    }

    public override void OnAnimationExitEvent()
    {
        base.OnAnimationExitEvent();

        if (useNextConcurrentAttack)
        {
            return;
        }

        ResetAnimationIndex(stateMachine.Player.AnimationData.SwordAttackParameterName);
        stateMachine.ChangeState(stateMachine.AttackingIdleState);
    }

    public override void OnAnimationTransitionEvent()
    {
        base.OnAnimationTransitionEvent();

        if (useNextConcurrentAttack)
        {
            useNextConcurrentAttack = false;
            NextConcurrentAttack(stateMachine.Player.AnimationData.SwordAttackParameterName);
            if (!IsNextAttackConcurrent(swordAttackData.StartingSwordAttackAnimationIndex, swordAttackData.LastConcurrentSwordAttackAnimationIndex, stateMachine.Player.AnimationData.SwordAttackParameterName))
            {
                stateMachine.Player.Input.PlayerActions.Attack.canceled -= OnMeleeNormalAttackStarted;
                stateMachine.Player.Input.PlayerActions.Attack.canceled -= OnNormalAttackStarted;
            }
            return;
        }

        stateMachine.Player.Input.PlayerActions.Attack.canceled -= OnMeleeNormalAttackStarted;
    }
}
