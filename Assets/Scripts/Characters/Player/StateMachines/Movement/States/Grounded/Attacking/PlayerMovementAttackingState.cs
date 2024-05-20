using UnityEngine;

public class PlayerMovementAttackingState : PlayerAttackingState
{
    public PlayerMovementAttackingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    private bool canMove;

    public override void Enter()
    {
        canMove = false;
        stateMachine.ReusableData.MovementSpeedModifier = 0f;

        base.Enter();

        ResetVelocity();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (stateMachine.ReusableData.MovementInput == Vector2.zero || !canMove)
        {
            return;
        }

        OnMove();
    }

    public override void OnAnimationTransitionEvent()
    {
        base.OnAnimationTransitionEvent();

        canMove = true;
    }

    public override void OnAnimationExitEvent()
    {
        base.OnAnimationExitEvent();

        stateMachine.ChangeState(stateMachine.IdlingState);
    }
}
