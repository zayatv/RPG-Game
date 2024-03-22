using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdlingState : PlayerGroundedState
{
    private PlayerIdleData idleData;

    public PlayerIdlingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        idleData = movementData.IdleData;
    }

    public override void Enter()
    {
        stateMachine.ReusableData.MovementSpeedModifier = 0f;

        stateMachine.ReusableData.BackwardsCameraRecenteringData = idleData.BackwardsCameraRecenteringData;
        
        base.Enter();

        StartAnimation(stateMachine.Player.AnimationData.IdleParameterHash);

        stateMachine.ReusableData.CurrentJumpForce = airborneData.JumpData.StationaryForce;

        ResetVelocity();

        stateMachine.Player.CanAttack = true;
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.AnimationData.IdleParameterHash);

        stateMachine.Player.CanAttack = false;

        DisableWeaponObject();
    }

    public override void Update()
    {
        base.Update();

        if (stateMachine.ReusableData.MovementInput == Vector2.zero)
        {
            return;
        }

        OnMove();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (!IsMovingHorizontally())
        {
            return;
        }

        ResetVelocity();
    }

    /*protected override void AddInputActionsCallbacks()
    {
        base.AddInputActionsCallbacks();

        stateMachine.Player.Input.PlayerActions.Attack.started += OnAttackStarted;
    }

    protected override void RemoveInputActionsCallbacks()
    {
        base.RemoveInputActionsCallbacks();

        stateMachine.Player.Input.PlayerActions.Attack.started -= OnAttackStarted;
    }*/
}
