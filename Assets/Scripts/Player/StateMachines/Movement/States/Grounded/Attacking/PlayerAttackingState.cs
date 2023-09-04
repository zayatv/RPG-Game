using UnityEngine;

public class PlayerAttackingState : PlayerGroundedState
{
    public PlayerAttackingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.ReusableData.MovementSpeedModifier = 0f;

        base.Enter();

        StartAnimation(stateMachine.Player.AnimationData.AttackParameterHash);

        stateMachine.ReusableData.CurrentJumpForce = Vector3.zero;

        ResetVelocity();
        EnableWeaponObject();

        stateMachine.Player.IsAttacking = true;
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.AnimationData.AttackParameterHash);

        DisableWeaponObject();

        stateMachine.Player.IsAttacking = false;
    }

    public override void EnableWeapon()
    {
        base.EnableWeapon();

        if(stateMachine.Player.WeaponParentTransform.GetChild(0).TryGetComponent<CapsuleCollider>(out CapsuleCollider collider))
        {
            collider.enabled = true;
        }
    }

    public override void DisableWeapon()
    {
        base.DisableWeapon();

        if(stateMachine.Player.WeaponParentTransform.GetChild(0).TryGetComponent<CapsuleCollider>(out CapsuleCollider collider))
        {
            collider.enabled = false;
        }
    }

    protected void SetAnimationInteger(string parameterName, int parameterValue)
    {
        stateMachine.Player.Animator.SetInteger(parameterName, parameterValue);
    }

    protected int GetAnimationIndex(string parameterName)
    {
        return stateMachine.Player.Animator.GetInteger(parameterName);
    }

    protected bool IsNextAttackConcurrent(int startAttack, int maxConcurrentAttacks, string attackType)
    {
        int currentAttack = stateMachine.Player.Animator.GetInteger(attackType);
        return currentAttack < maxConcurrentAttacks && currentAttack >= startAttack;
    }

    protected void ResetAnimationIndex(string attackType, int startAttackIndex = 0)
    {
        SetAnimationInteger(attackType, startAttackIndex);
    }

    protected void NextConcurrentAttack(string parameterName)
    {
        // if (!IsNextAttackConcurrent(startAttack, maxConcurrentAttacks, parameterName))
        // {
        //     StopAnimation(stateMachine.Player.AnimationData.AttackParameterHash);
        //     SetAnimationInteger(parameterName, startAttack);
        //     return;
        // }

        SetAnimationInteger(parameterName, GetAnimationIndex(parameterName) + 1);
    }

    protected void SetRotationForAttack(Vector3 newRotationVector, out Quaternion initialRotation)
    {
        var weapon = stateMachine.Player.WeaponParentTransform.GetChild(0);
        initialRotation = weapon.transform.rotation;

        Quaternion newRotation = Quaternion.Euler(newRotationVector.x, newRotationVector.y, newRotationVector.z);
        weapon.transform.rotation = newRotation;
    }

    private void EnableWeaponObject()
    {
        stateMachine.Player.WeaponParentTransform.gameObject.SetActive(true);
    }

    private void DisableWeaponObject()
    {
        stateMachine.Player.WeaponParentTransform.gameObject.SetActive(false);
    }
}
