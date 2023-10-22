using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackController : MonoBehaviour
{
    public InputActionReference attack;
    public InputActionReference chargeAttack;

    void Start()
    {
        attack.action.performed += AttackPerformed;
        chargeAttack.action.performed += ChargeAttackPerformed;
        chargeAttack.action.canceled += ChargeAttackCanceled;
    }

    private void OnDestroy()
    {
        attack.action.performed -= AttackPerformed;
        chargeAttack.action.performed -= ChargeAttackPerformed;
        chargeAttack.action.canceled -= ChargeAttackCanceled;
    }

    private void AttackPerformed(InputAction.CallbackContext obj)
    {
        Debug.Log("Attack");
    }

    private void ChargeAttackPerformed(InputAction.CallbackContext obj)
    {
        Debug.Log("Charge " + obj.phase);
    }

    private void ChargeAttackCanceled(InputAction.CallbackContext obj)
    {
        Debug.Log("Charge End");
    }
}
