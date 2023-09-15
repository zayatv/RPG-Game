public class PlayerRangedChargedAttackingState : PlayerRangedAttackingState
{
    public PlayerRangedChargedAttackingState(PlayerAttackingStateMachine playerAttackingStateMachine) : base(playerAttackingStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        StartAnimation(stateMachine.Player.AnimationData.ChargeAttackParameterHash);

        EnableWeaponObject();

        stateMachine.Player.CameraUtility.DisableMainCamera();
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.AnimationData.ChargeAttackParameterHash);

        stateMachine.Player.CameraUtility.EnableMainCamera();
    }

    protected override void AddInputActionsCallbacks()
    {
        stateMachine.Player.Input.PlayerActions.Attack.canceled -= OnNormalAttackStarted;
    }

    public override void OnAnimationExitEvent()
    {
        base.OnAnimationExitEvent();

        stateMachine.ChangeState(stateMachine.AttackingIdleState);
    }
}
