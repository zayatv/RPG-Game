using UnityEngine;

public class PlayerAirborneState : PlayerMovementState
{
    protected PlayerAirJumpData airJumpData;

    public PlayerAirborneState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        airJumpData = airborneData.AirJumpData;
    }

    public override void Enter()
    {
        base.Enter();

        StartAnimation(stateMachine.Player.AnimationData.AirborneParameterHash);

        ResetSprintState();
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.AnimationData.AirborneParameterHash);
    }

    protected override void OnContactWithGround(Collider collider)
    {
        stateMachine.ChangeState(stateMachine.LightLandingState);
    }

    protected virtual void ResetSprintState()
    {
        stateMachine.ReusableData.ShouldSprint = false;
    }
}
