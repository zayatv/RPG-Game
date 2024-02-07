using UnityEngine;

public class PlayerMovementAttackingState : PlayerGroundedState
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

        stateMachine.Player.Armory.rightHand.gameObject.SetActive(true);

        StartAnimation(stateMachine.Player.AnimationData.AttackParameterHash);

        stateMachine.ReusableData.CurrentJumpForce = Vector3.zero;

        ResetVelocity();
        DisableCameraRecentering();
        EnableWeaponObject();
    }

    public override void Exit()
    {
        base.Exit();

        stateMachine.Player.Armory.rightHand.gameObject.SetActive(false);

        StopAnimation(stateMachine.Player.AnimationData.AttackParameterHash);
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
