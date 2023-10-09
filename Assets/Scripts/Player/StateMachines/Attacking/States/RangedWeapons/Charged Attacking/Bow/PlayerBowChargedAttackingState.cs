using UnityEngine.InputSystem;

public class PlayerBowChargedAttackingState : PlayerRangedChargedAttackingState
{
    public PlayerBowChargedAttackingState(PlayerAttackingStateMachine playerAttackingStateMachine) : base(playerAttackingStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //StartAnimation(stateMachine.Player.AnimationData.BowEquippedParameterHash);
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.AnimationData.BowEquippedParameterHash);
        StopAnimation(stateMachine.Player.AnimationData.BowShotParameterHash);
    }

    protected override void AddInputActionsCallbacks()
    {
        //stateMachine.Player.Input.PlayerActions.ChargedAttack.canceled += OnBowShoot;
    }

    protected override void RemoveInputActionsCallbacks()
    {
        //stateMachine.Player.Input.PlayerActions.ChargedAttack.canceled -= OnBowShoot;
    }

    private void OnBowShoot(InputAction.CallbackContext context)
    {
        StartAnimation(stateMachine.Player.AnimationData.BowShotParameterHash);
    }
}
