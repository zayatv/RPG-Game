using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerNormalAttackingState : PlayerAttackingState
{
    protected MeleeNormalAttackingData normalAttackingData;

    protected bool useNextConcurrentAttack;

    public PlayerNormalAttackingState(PlayerAttackingStateMachine playerAttackingStateMachine) : base(playerAttackingStateMachine)
    {
        normalAttackingData = stateMachine.Player.PlayerData.AttackData.MeleeNormalAttackingData;
    }

    public override void Enter()
    {
        base.Enter();

        useNextConcurrentAttack = false;

        ResetTimeSinceLastAttackData();
    }

    public override void Exit()
    {
        base.Exit();

        ResetTimeSinceLastAttackData();
    }

    protected override void AddInputActionsCallbacks()
    {
        stateMachine.Player.Input.PlayerActions.Attack.canceled += OnConcurrentNormalAttackOnTransition;
    }

    protected override void RemoveInputActionsCallbacks()
    {
        stateMachine.Player.Input.PlayerActions.Attack.canceled -= OnConcurrentNormalAttackOnTransition;
    }

    protected void OnConcurrentNormalAttackOnTransition(InputAction.CallbackContext context)
    {
        float timeDifference = Time.time - normalAttackingData.TimeToStartRegisterConcurrentAttack;

        if (normalAttackingData.TimeSinceLastAttack <= timeDifference)
        {
            useNextConcurrentAttack = true;
        }
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
        ResetTimeSinceLastAttackData();
        SetAnimationInteger(parameterName, GetAnimationIndex(parameterName) + 1);
    }

    protected void ResetTimeSinceLastAttackData()
    {
        normalAttackingData.TimeSinceLastAttack = Time.time;
    }
}
