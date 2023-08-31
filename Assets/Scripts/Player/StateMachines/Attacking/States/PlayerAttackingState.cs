using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingState : IState
{
    protected PlayerAttackingStateMachine stateMachine;

    public PlayerAttackingState(PlayerAttackingStateMachine playerAttackingStateMachine)
    {
        stateMachine = playerAttackingStateMachine;
    }

    public void DisableWeapon()
    {
        stateMachine.Player.WeaponParentTransform.GetChild(0).GetComponent<CapsuleCollider>().enabled = false;
        Debug.Log("Weapon Disabled");
    }

    public void EnableWeapon()
    {
        stateMachine.Player.WeaponParentTransform.GetChild(0).GetComponent<CapsuleCollider>().enabled = true;
        Debug.Log("Weapon Enabled");
    }

    public void Enter()
    {
        AddInputActionsCallbacks();
    }

    public void Exit()
    {
        RemoveInputActionsCallbacks();
    }

    public void HandleInput()
    {

    }

    public void OnAnimationEnterEvent()
    {

    }

    public void OnAnimationExitEvent()
    {

    }

    public void OnAnimationTransitionEvent()
    {

    }

    public void OnTriggerEnter(Collider collider)
    {

    }

    public void OnTriggerExit(Collider collider)
    {

    }

    public void PhysicsUpdate()
    {

    }

    public void Update()
    {

    }

    protected void StartAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetBool(animationHash, false);
    }

    protected virtual void AddInputActionsCallbacks()
    {
        
    }

    protected virtual void RemoveInputActionsCallbacks()
    {
        
    }
}
