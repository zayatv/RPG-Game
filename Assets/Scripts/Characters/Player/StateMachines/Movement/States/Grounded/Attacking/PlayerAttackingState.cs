using System.Collections;
using UnityEngine;

public class PlayerAttackingState : PlayerGroundedState
{
    public PlayerAttackingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        EnableWeapon();

        StartAnimation(stateMachine.Player.AnimationData.AttackParameterHash);

        stateMachine.ReusableData.CurrentJumpForce = Vector3.zero;

        DisableCameraRecentering();
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.AnimationData.AttackParameterHash);

        stateMachine.Player.StartCoroutine(DisableWeaponRoutine());
    }

    private IEnumerator DisableWeaponRoutine()
    {
        yield return new WaitForSeconds(3);

        DisableWeapon();
    }
}
