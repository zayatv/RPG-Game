using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMeleeNormalAttackingState : PlayerNormalAttackingState
{
    protected bool useNextConcurrentAttack;

    public PlayerMeleeNormalAttackingState(PlayerAttackingStateMachine playerAttackingStateMachine) : base(playerAttackingStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        useNextConcurrentAttack = false;

        EnableWeaponObject();
    }

    protected override void AddInputActionsCallbacks()
    {
        stateMachine.Player.Input.PlayerActions.Attack.canceled += OnMeleeNormalAttackStarted;
    }

    protected override void RemoveInputActionsCallbacks()
    {
        stateMachine.Player.Input.PlayerActions.Attack.canceled -= OnMeleeNormalAttackStarted;
    }

    protected void OnMeleeNormalAttackStarted(InputAction.CallbackContext context)
    {
        useNextConcurrentAttack = true;
    }

    public override void EnableWeaponCollider()
    {
        if (stateMachine.Player.WeaponParentTransform.GetChild(0).TryGetComponent<Collider>(out Collider collider))
        {
            collider.enabled = true;
        }
    }

    public override void DisableWeaponCollider()
    {
        if (stateMachine.Player.WeaponParentTransform.GetChild(0).TryGetComponent<Collider>(out Collider collider))
        {
            collider.enabled = false;
        }
    }
}
