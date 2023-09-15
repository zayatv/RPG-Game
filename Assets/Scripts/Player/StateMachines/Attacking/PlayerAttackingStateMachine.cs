public class PlayerAttackingStateMachine : StateMachine
{
    public Player Player { get; }
    public PlayerStateReusableData ReusableData { get; }

    public PlayerAttackingIdleState AttackingIdleState { get; }

    public PlayerSwordAttackingState SwordAttackingState { get; }
    public PlayerSpearAttackingState SpearAttackingState { get; }

    public PlayerBowAttackingState BowAttackingState { get; }
    public PlayerBowChargedAttackingState BowChargedAttackingState { get; }
    public PlayerBowNormalAttackingState BowNormalAttackingState { get; }

    public PlayerAttackingStateMachine(Player player)
    {
        Player = player;
        ReusableData = new PlayerStateReusableData();

        AttackingIdleState = new PlayerAttackingIdleState(this);

        SwordAttackingState = new PlayerSwordAttackingState(this);
        SpearAttackingState = new PlayerSpearAttackingState(this);

        BowAttackingState = new PlayerBowAttackingState(this);
        BowChargedAttackingState = new PlayerBowChargedAttackingState(this);
        BowNormalAttackingState = new PlayerBowNormalAttackingState(this);
    }
}
