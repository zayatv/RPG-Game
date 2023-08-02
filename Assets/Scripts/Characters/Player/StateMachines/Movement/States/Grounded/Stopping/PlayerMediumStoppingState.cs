using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMediumStoppingState : PlayerStoppingState
{
    public PlayerMediumStoppingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateMachine.ReusableData.MovementDecelerationForce = movementData.StopData.MediumDecelerationForce;

        stateMachine.ReusableData.CurrentJumpForce = airborneData.JumpData.MediumForce;
    }
}
