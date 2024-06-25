using Animancer;
using RPG.Gameplay;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG.Combat
{
    public class WeaponBehavior : MonoBehaviour
    {
        [SerializeField] protected InputActionReference attackInput;
        [SerializeField] protected InputActionReference chargeInput;

        protected Actor user;

        protected HybridAnimancerComponent Animator => user.CurrentAnimator;

        protected virtual void Start()
        {
            user = GetComponentInParent<Actor>();

            attackInput.action.performed += AttackInput_Performed;
            chargeInput.action.performed += ChargeInput_Start;
            chargeInput.action.canceled += ChargeInput_Release;
        }

        protected virtual void OnDestroy()
        {
            attackInput.action.performed -= AttackInput_Performed;
            chargeInput.action.performed -= ChargeInput_Start;
            chargeInput.action.canceled -= ChargeInput_Release;
        }

        protected virtual void Update()
        {

        }

        protected virtual void AttackInput_Performed(InputAction.CallbackContext context)
        {
        }

        protected virtual void ChargeInput_Start(InputAction.CallbackContext context)
        {
        }

        protected virtual void ChargeInput_Release(InputAction.CallbackContext context)
        {
        }
    }
}