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

        stateMachine.ReusableData.CurrentJumpForce = Vector3.zero;

        StartAnimation(stateMachine.Player.AnimationData.AttackParameterHash);
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

    protected int GetAnimationInteger(string parameterName)
    {
        return stateMachine.Player.Animator.GetInteger(parameterName);
    }

    protected bool IsNextAttackConcurrent(int startAttack, int maxConcurrentAttacks, string attackType)
    {
        int currentAttack = stateMachine.Player.Animator.GetInteger(attackType);
        return currentAttack < maxConcurrentAttacks && currentAttack >= startAttack;
    }

    protected void NextConcurrentAttack(int startAttack, int maxConcurrentAttacks, string parameterName)
    {
        if (!IsNextAttackConcurrent(startAttack, maxConcurrentAttacks, parameterName))
        {
            Debug.Log("Attack non-concurrent");
            SetAnimationInteger(parameterName, startAttack);
            return;
        }

        Debug.Log("Attack concurrent");
        
        StopAnimation(stateMachine.Player.AnimationData.AttackParameterHash);
        Debug.Log("1");
        SetAnimationInteger(parameterName, GetAnimationInteger(parameterName) + 1);
        Debug.Log("2");
        StartAnimation(stateMachine.Player.AnimationData.AttackParameterHash);
        Debug.Log("3");
    }
}
