using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMeleeNormalAttackingState : PlayerNormalAttackingState
{
    public PlayerMeleeNormalAttackingState(PlayerAttackingStateMachine playerAttackingStateMachine) : base(playerAttackingStateMachine)
    {
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
