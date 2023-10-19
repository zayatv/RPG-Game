using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackingState : IState
{
    protected PlayerAttackingStateMachine stateMachine;

    protected PlayerAttackData attackData;

    public PlayerAttackingState(PlayerAttackingStateMachine playerAttackingStateMachine)
    {
        stateMachine = playerAttackingStateMachine;

        attackData = stateMachine.Player.PlayerData.AttackData;
    }

    public virtual void Enter()
    {
        AddInputActionsCallbacks();
    }

    public virtual void Exit()
    {
        RemoveInputActionsCallbacks();
    }

    public virtual void HandleInput()
    {
    }

    public virtual void Update()
    {
    }

    public virtual void PhysicsUpdate()
    {
    }

    public virtual void OnAnimationEnterEvent()
    {
    }

    public virtual void OnAnimationExitEvent()
    {
    }

    public virtual void OnAnimationTransitionEvent()
    {
    }

    public virtual void EnableWeaponCollider()
    {
    }

    public virtual void DisableWeaponCollider()
    {
    }

    public virtual void OnTriggerEnter(Collider collider)
    {
    }

    public virtual void OnTriggerExit(Collider collider)
    {
    }

    protected virtual void AddInputActionsCallbacks()
    {
        
    }

    protected virtual void RemoveInputActionsCallbacks()
    {
        
    }

    protected void SetAnimationInteger(string parameterName, int parameterValue)
    {
        stateMachine.Player.Animator.SetInteger(parameterName, parameterValue);
    }

    protected int GetAnimationIndex(string parameterName)
    {
        return stateMachine.Player.Animator.GetInteger(parameterName);
    }

    protected void ResetAnimationIndex(string attackType, int startAttackIndex = 0)
    {
        SetAnimationInteger(attackType, startAttackIndex);
    }

    protected void SetWeaponRotationForAttack(Vector3 newRotationVector, out Quaternion initialRotation)
    {
        Transform weapon = stateMachine.Player.WeaponParentTransform.GetChild(0);
        initialRotation = weapon.rotation;

        Quaternion newRotation = Quaternion.Euler(newRotationVector);
        weapon.rotation = newRotation;
    }

    protected void StartAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetBool(animationHash, false);
    }

    protected void OnNormalAttackStarted(InputAction.CallbackContext context)
    {
        if (UIManager.IsInMenu || !stateMachine.Player.CanAttack) return;

        switch (stateMachine.Player.CurrentEquippedWeapon.WeaponType)
        {
            case WeaponType.Hammer:
                stateMachine.ChangeState(stateMachine.SwordNormalAttackingState);
                break;

            case WeaponType.Axe:
                stateMachine.ChangeState(stateMachine.SwordNormalAttackingState);
                break;

            case WeaponType.Gauntlet:
                stateMachine.ChangeState(stateMachine.SwordNormalAttackingState);
                break;

            case WeaponType.Sword:
                stateMachine.ChangeState(stateMachine.SwordNormalAttackingState);
                break;

            case WeaponType.Spear:
                stateMachine.ChangeState(stateMachine.SpearNormalAttackingState);
                break;

            case WeaponType.Dagger:
                stateMachine.ChangeState(stateMachine.SwordNormalAttackingState);
                break;

            case WeaponType.Wand:
                stateMachine.ChangeState(stateMachine.BowNormalAttackingState);
                break;

            case WeaponType.Bow:
                stateMachine.ChangeState(stateMachine.BowNormalAttackingState);
                break;

            default:
                break;
        }
    }

    protected void OnChargedAttackStarted()
    {
        if (UIManager.IsInMenu || !stateMachine.Player.CanAttack) return;

        switch (stateMachine.Player.CurrentEquippedWeapon.WeaponType)
        {
            case WeaponType.Hammer:
                stateMachine.ChangeState(stateMachine.SwordNormalAttackingState);
                break;

            case WeaponType.Axe:
                stateMachine.ChangeState(stateMachine.SwordNormalAttackingState);
                break;

            case WeaponType.Gauntlet:
                stateMachine.ChangeState(stateMachine.SwordNormalAttackingState);
                break;

            case WeaponType.Sword:
                stateMachine.ChangeState(stateMachine.SwordChargedAttackingState);
                break;

            case WeaponType.Spear:
                stateMachine.ChangeState(stateMachine.SpearNormalAttackingState);
                break;

            case WeaponType.Dagger:
                stateMachine.ChangeState(stateMachine.SwordNormalAttackingState);
                break;

            case WeaponType.Wand:
                stateMachine.ChangeState(stateMachine.BowChargedAttackingState);
                break;

            case WeaponType.Bow:
                stateMachine.ChangeState(stateMachine.BowChargedAttackingState);
                break;

            default:
                break;
        }
    }
}
