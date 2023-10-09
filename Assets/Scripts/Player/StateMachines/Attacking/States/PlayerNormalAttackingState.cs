using UnityEngine;

public class PlayerNormalAttackingState : PlayerAttackingState
{
    public PlayerNormalAttackingState(PlayerAttackingStateMachine playerAttackingStateMachine) : base(playerAttackingStateMachine)
    {
    }

    protected bool IsNextAttackConcurrent(int startAttack, int maxConcurrentAttacks, string attackType)
    {
        int currentAttack = stateMachine.Player.Animator.GetInteger(attackType);
        return currentAttack < maxConcurrentAttacks && currentAttack >= startAttack;
    }

    protected bool IsCurrentAttackLastAttack(int maxConcurrentAttacks, string attackType)
    {
        int currentAttack = stateMachine.Player.Animator.GetInteger(attackType);
        return currentAttack >= maxConcurrentAttacks;
    }

    protected void NextConcurrentAttack(string parameterName)
    {
        SetAnimationInteger(parameterName, GetAnimationIndex(parameterName) + 1);
    }
}
