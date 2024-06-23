using UnityEngine;

public class PlayerChargeAttackingState : PlayerAttackingState
{
    private PlayerChargedAttackingData chargeAttackData;

    private bool isCharging;

    private Vector2 normalizedVector;

    public PlayerChargeAttackingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        chargeAttackData = stateMachine.Player.PlayerData.AttackData.ChargedAttackingData;
    }

    public override void Enter()
    {
        stateMachine.ReusableData.MovementSpeedModifier = chargeAttackData.SpeedModifier;

        stateMachine.Player.CameraUtility.AimCamera.m_Priority = 100;

        base.Enter();

        isCharging = true;

        normalizedVector = new Vector2(1, 1).normalized;
    }

    public override void Exit()
    {
        stateMachine.Player.CameraUtility.AimCamera.m_Priority = 0;

        base.Exit();

        isCharging = false;
    }

    public override void PhysicsUpdate()
    {
        if (isCharging)
        {
            stateMachine.Player.Animator.SetFloat(stateMachine.Player.AnimationData.ChargeLoopXHash, stateMachine.ReusableData.MovementInput.x / normalizedVector.x);
            stateMachine.Player.Animator.SetFloat(stateMachine.Player.AnimationData.ChargeLoopYHash, stateMachine.ReusableData.MovementInput.y / normalizedVector.y);
            Float();
            Move();
            return;
        }

        base.PhysicsUpdate();
    }

    public override void OnAnimationEnterEvent()
    {
        stateMachine.ReusableData.MovementSpeedModifier = 0f;

        stateMachine.Player.CameraUtility.AimCamera.m_Priority = 0;

        base.OnAnimationEnterEvent();

        ResetVelocity();
    }

    public override void OnAnimationTransitionEvent()
    {
        base.OnAnimationTransitionEvent();

        if (stateMachine.ReusableData.MovementInput == Vector2.zero)
        {
            stateMachine.ChangeState(stateMachine.IdlingState);
            return;
        }

        OnMove();
    }

    private void Move()
    {
        UpdateTargetRotation(stateMachine.Player.CameraUtility.AimCamera.transform.forward, false);
        RotateTowardsTargetRotation(false);

        if (stateMachine.ReusableData.MovementInput == Vector2.zero || stateMachine.ReusableData.MovementSpeedModifier == 0f)
        {
            stateMachine.Player.Rigidbody.linearVelocity = new Vector3(0, stateMachine.Player.Rigidbody.linearVelocity.y, 0);
            return;
        }

        Vector3 movementDirection = GetMovementInputDirection();

        Vector3 moveDir = (stateMachine.Player.transform.right * movementDirection.x + stateMachine.Player.transform.forward * movementDirection.z).normalized;

        float movementSpeed = GetMovementSpeed();

        Vector3 currentPlayerHorizontalVelocity = GetPlayerHorizontalVelocity();

        stateMachine.Player.Rigidbody.AddForce(moveDir * movementSpeed - currentPlayerHorizontalVelocity, ForceMode.VelocityChange);
    }
}
