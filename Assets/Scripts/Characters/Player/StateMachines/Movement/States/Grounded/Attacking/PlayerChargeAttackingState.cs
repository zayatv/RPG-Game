using UnityEngine;
using CombatSystem;
public class PlayerChargeAttackingState : PlayerGroundedState
{
    private PlayerChargedAttackingData chargeAttackData;

    private bool isCharging;

    public PlayerChargeAttackingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        chargeAttackData = stateMachine.Player.PlayerData.AttackData.ChargedAttackingData;
    }

    public override void Enter()
    {
        Debug.Log("Its actually thiss");
        //stateMachine.ReusableData.MovementSpeedModifier = chargeAttackData.SpeedModifier;

        //stateMachine.Player.Stats.sp
        stateMachine.Player.CameraUtility.aimCamera.Priority = 20;
        

        base.Enter();

       
        stateMachine.Player.Armory.leftHand.gameObject.SetActive(true);

        StartAnimation(stateMachine.Player.AnimationData.AttackParameterHash);

        stateMachine.ReusableData.CurrentJumpForce = Vector3.zero;

        isCharging = true;

        DisableCameraRecentering();
    }

    public override void Exit()
    {
        base.Exit();
       
       
        //stateMachine.Player.Armory.UnequipCurrentWeapon();
         
        StopAnimation(stateMachine.Player.AnimationData.AttackParameterHash);

        isCharging = false;
    }

    public override void PhysicsUpdate()
    {
        if (isCharging)
        {
         
            //Float();
            //UpdateTargetRotation(stateMachine.Player.CameraUtility.AimCamera.transform.forward, false);
            RotateTowardsTargetRotation();
            return;
        }

        base.PhysicsUpdate();
    }

    public override void OnAnimationTransitionEvent()
    {
        stateMachine.ReusableData.MovementSpeedModifier = 0f;
        
        //stateMachine.Player.CameraUtility.AimCamera.m_Priority = 0;

        base.OnAnimationTransitionEvent();

        ResetVelocity();
    }

    public override void OnAnimationExitEvent()
    {
        base.OnAnimationExitEvent();

        if (stateMachine.ReusableData.MovementInput == Vector2.zero)
        {
            stateMachine.ChangeState(stateMachine.IdlingState);
          
            return;
        }

        OnMove();
    }
}
