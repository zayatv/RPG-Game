using UnityEngine;

public class PlayerAirborneState : PlayerMovementState
{
    public PlayerAirborneState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    protected override void OnContactWithGround(Collider collider)
    {
        stateMachine.ChangeState(stateMachine.IdlingState);
    }
}
