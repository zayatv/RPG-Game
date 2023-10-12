using UnityEngine;

public class PlayerChargedAttackingState : PlayerAttackingState
{
    public PlayerChargedAttackingState(PlayerAttackingStateMachine playerAttackingStateMachine) : base(playerAttackingStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        StartAnimation(stateMachine.Player.AnimationData.ChargeAttackParameterHash);

        Debug.Log("Charged Attack");
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.AnimationData.ChargeAttackParameterHash);

        Debug.Log("Charged Attack Stopped");
    }
}
