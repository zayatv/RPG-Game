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

        Debug.Log(GetType().Name);
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

    public virtual void EnableWeapon()
    {
        if (stateMachine.Player.WeaponParentTransform.GetChild(0).TryGetComponent<CapsuleCollider>(out CapsuleCollider collider))
        {
            collider.enabled = true;
        }
    }

    public virtual void DisableWeapon()
    {
        if (stateMachine.Player.WeaponParentTransform.GetChild(0).TryGetComponent<CapsuleCollider>(out CapsuleCollider collider))
        {
            collider.enabled = false;
        }
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

    protected bool IsNextAttackConcurrent(int startAttack, int maxConcurrentAttacks, string attackType)
    {
        int currentAttack = stateMachine.Player.Animator.GetInteger(attackType);
        return currentAttack < maxConcurrentAttacks && currentAttack >= startAttack;
    }

    protected bool IsCurrentAttackLastAttack(int maxConcurrentAttacks, string attackType)
    {
        int currentAttack = stateMachine.Player.Animator.GetInteger(attackType);
        return currentAttack >= maxConcurrentAttacks;
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
        Transform weapon = stateMachine.Player.WeaponParentTransform.GetChild(0);
        initialRotation = weapon.rotation;

        Quaternion newRotation = Quaternion.Euler(newRotationVector);
        weapon.rotation = newRotation;
    }

    protected void EnableWeaponObject()
    {
        stateMachine.Player.WeaponParentTransform.gameObject.SetActive(true);
    }

    protected void DisableWeaponObject()
    {
        stateMachine.Player.WeaponParentTransform.gameObject.SetActive(false);
    }

    protected void StartAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetBool(animationHash, false);
    }

    protected void OnAttackStarted(InputAction.CallbackContext context)
    {
        if (UIManager.IsInMenu || !stateMachine.Player.CanAttack) return;

        switch (stateMachine.Player.CurrentEquippedWeapon.WeaponType)
        {
            case WeaponType.Sword:
                stateMachine.ChangeState(stateMachine.SwordAttackingState);
                break;

            case WeaponType.Spear:
                stateMachine.ChangeState(stateMachine.SpearAttackingState);
                break;

            case WeaponType.Wand:
                stateMachine.ChangeState(stateMachine.SwordAttackingState);
                break;

            case WeaponType.Hammer:
                stateMachine.ChangeState(stateMachine.SwordAttackingState);
                break;

            case WeaponType.Gauntlet:
                stateMachine.ChangeState(stateMachine.SwordAttackingState);
                break;

            case WeaponType.Dagger:
                stateMachine.ChangeState(stateMachine.SwordAttackingState);
                break;

            case WeaponType.Bow:
                stateMachine.ChangeState(stateMachine.BowAttackingState);
                break;

            default:
                break;
        }
    }
}
