public class PlayerMovementStateMachine : StateMachine
{
    public Player Player { get; }
    public PlayerStateReusableData ReusableData { get; }
    public PlayerIdlingState IdlingState { get; }
    public PlayerDashingState DashingState { get; }
    public PlayerWalkingState WalkingState { get; }
    public PlayerRunningState RunningState { get; }
    public PlayerSprintingState SprintingState { get; }

    public PlayerLightStoppingState LightStoppingState { get; }
    public PlayerMediumStoppingState MediumStoppingState { get; }
    public PlayerHardStoppingState HardStoppingState { get; }

    public PlayerMovementAttackingState AttackingState { get; }

    public PlayerLightLandingState LightLandingState { get; }
    
    public PlayerRollingState RollingState { get; }
    
    public PlayerHardLandingState HardLandingState { get; }

    public PlayerJumpingState JumpingState { get; }
    public PlayerAirJumpingState AirJumpingState { get; }
    public PlayerFallingState FallingState { get; }

    public PlayerMovementStateMachine(Player player)
    {
        Player = player;
        ReusableData = new PlayerStateReusableData();

        IdlingState = new PlayerIdlingState(this);
        DashingState = new PlayerDashingState(this);
        WalkingState = new PlayerWalkingState(this);
        RunningState = new PlayerRunningState(this);
        SprintingState = new PlayerSprintingState(this);

        LightStoppingState = new PlayerLightStoppingState(this);
        MediumStoppingState = new PlayerMediumStoppingState(this);
        HardStoppingState = new PlayerHardStoppingState(this);

        AttackingState = new PlayerMovementAttackingState(this);

        JumpingState = new PlayerJumpingState(this);
        AirJumpingState = new PlayerAirJumpingState(this);
        FallingState = new PlayerFallingState(this);

        LightLandingState = new PlayerLightLandingState(this);
        RollingState = new PlayerRollingState(this);
        HardLandingState = new PlayerHardLandingState(this);
    }
}
