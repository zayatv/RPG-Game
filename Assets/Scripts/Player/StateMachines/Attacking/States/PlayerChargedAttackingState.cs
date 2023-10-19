using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerChargedAttackingState : PlayerAttackingState
{
    protected int chargedForSeconds;

    protected PlayerChargedAttackingData chargedAttackingData;

    public PlayerChargedAttackingState(PlayerAttackingStateMachine playerAttackingStateMachine) : base(playerAttackingStateMachine)
    {
        chargedAttackingData = stateMachine.Player.PlayerData.AttackData.ChargedAttackingData;
    }

    public override void Enter()
    {
        base.Enter();

        StartAnimation(stateMachine.Player.AnimationData.ChargeAttackParameterHash);

        stateMachine.Player.StartCoroutine(ChargeChargedAttack());
    }

    protected override void AddInputActionsCallbacks()
    {
        base.AddInputActionsCallbacks();

        stateMachine.Player.Input.PlayerActions.Attack.canceled += OnChargeCanceled;
    }

    protected override void RemoveInputActionsCallbacks()
    {
        base.RemoveInputActionsCallbacks();

        stateMachine.Player.Input.PlayerActions.Attack.canceled -= OnChargeCanceled;
    }

    private IEnumerator ChargeChargedAttack()
    {
        yield return new WaitForSeconds(1 - chargedAttackingData.TimeAfterEnteredChargedAttack);
        chargedForSeconds = 1;
        Debug.Log("Waited 1 Second!");

        yield return new WaitForSeconds(1);
        chargedForSeconds = 2;
        Debug.Log("Waited 2 Seconds!");

        yield return new WaitForSeconds(1);
        chargedForSeconds = 3;
        Debug.Log("Waited 3 Seconds!");

        //stateMachine.Player.Input.PlayerActions.Attack.canceled -= OnChargeCanceled;
        //StopAnimation(stateMachine.Player.AnimationData.ChargeAttackParameterHash);
    }

    private void OnChargeCanceled(InputAction.CallbackContext context)
    {
        stateMachine.Player.StopCoroutine(ChargeChargedAttack());
        StopAnimation(stateMachine.Player.AnimationData.ChargeAttackParameterHash);
    }
}
