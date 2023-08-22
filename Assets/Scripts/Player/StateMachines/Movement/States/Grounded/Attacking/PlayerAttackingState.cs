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
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.AnimationData.AttackParameterHash);

        DisableWeaponObject();
    }

    public override void EnableWeapon()
    {
        base.EnableWeapon();

        stateMachine.Player.CurrentEquippedWeapon.WeaponModel.GetComponent<CapsuleCollider>().enabled = true;
        Debug.Log("Weapon Enabled");
    }

    public override void DisableWeapon()
    {
        base.DisableWeapon();

        stateMachine.Player.CurrentEquippedWeapon.WeaponModel.GetComponent<CapsuleCollider>().enabled = false;
        Debug.Log("Weapon Disabled");
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

    protected void ResetAnimationIndex(int startAttackIndex, string attackType)
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

    private void EnableWeaponObject()
    {
        stateMachine.Player.WeaponParentTranform.gameObject.SetActive(true);
    }

    private void DisableWeaponObject()
    {
        stateMachine.Player.WeaponParentTranform.gameObject.SetActive(false);
    }
}
