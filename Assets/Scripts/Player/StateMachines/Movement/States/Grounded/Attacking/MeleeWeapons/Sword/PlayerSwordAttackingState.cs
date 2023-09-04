using UnityEngine;

public class PlayerSwordAttackingState : PlayerMeleeAttackingState
{
    private PlayerSwordAttackData swordAttackData;

    public PlayerSwordAttackingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        swordAttackData = movementData.SwordAttackData;
    }

    public override void OnAnimationExitEvent()
    {
        base.OnAnimationExitEvent();

        if (useNextConcurrentAttack)
        {
            return;
        }

        ResetAnimationIndex(stateMachine.Player.AnimationData.SwordAttackParameterName);
        stateMachine.ChangeState(stateMachine.IdlingState);
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
                stateMachine.Player.Input.PlayerActions.Attack.started -= OnAttackStarted;
            }
            return;
        }
    }
}
