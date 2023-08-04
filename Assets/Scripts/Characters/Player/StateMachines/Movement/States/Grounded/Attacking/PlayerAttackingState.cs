using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingState : PlayerGroundedState
{
    public PlayerAttackingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.ReusableData.MovementSpeedModifier = 0f;

        base.Enter();

        StartAnimation(stateMachine.Player.AnimationData.AttackParameterHash);

        stateMachine.ReusableData.CurrentJumpForce = Vector3.zero;

        ResetVelocity();
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.AnimationData.AttackParameterHash);
    }

    protected void SetAnimationInteger(string parameterName, int parameterValue)
    {
        stateMachine.Player.Animator.SetInteger(parameterName, parameterValue);
    }

    protected int GetAnimationIndex(string parameterName)
    {
        return stateMachine.Player.Animator.GetInteger(parameterName);
    }

    protected bool IsNextAttackConcurrent(int startAttack, int maxConcurrentAttacks, string attackType)
    {
        int currentAttack = stateMachine.Player.Animator.GetInteger(attackType);
        return currentAttack < maxConcurrentAttacks && currentAttack >= startAttack;
    }

    protected void ResetAnimationIndex(int startAttackIndex, string attackType)
    {
        SetAnimationInteger(attackType, startAttackIndex);
    }

    protected void NextConcurrentAttack(string parameterName)
    {
        // if (!IsNextAttackConcurrent(startAttack, maxConcurrentAttacks, parameterName))
        // {
        //     StopAnimation(stateMachine.Player.AnimationData.AttackParameterHash);
        //     SetAnimationInteger(parameterName, startAttack);
        //     return;
        // }

        SetAnimationInteger(parameterName, GetAnimationIndex(parameterName) + 1);
    }
}
