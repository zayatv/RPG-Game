using UnityEngine.InputSystem;

public class PlayerRunningState : PlayerMovingState
{
    public PlayerRunningState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.ReusableData.MovementSpeedModifier = movementData.RunData.SpeedModifier;
        
        base.Enter();
    }

    protected override void OnWalkToggleStarted(InputAction.CallbackContext context)
    {
        base.OnWalkToggleStarted(context);

        stateMachine.ChangeState(stateMachine.WalkingState);
    }
}
