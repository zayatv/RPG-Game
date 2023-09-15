public class PlayerAttackingStateMachine : StateMachine
{
    public Player Player { get; }
    public PlayerStateReusableData ReusableData { get; }

    public PlayerRangedAttackingState RangedAttackingState { get; }
    public PlayerMeleeAttackingState MeleeAttackingState { get; }

    public PlayerAttackingIdleState AttackingIdleState { get; }

    public PlayerSwordAttackingState SwordAttackingState { get; }
    public PlayerSpearAttackingState SpearAttackingState { get; }

    public PlayerBowChargedAttackingState BowChargedAttackingState { get; }
    public PlayerBowNormalAttackingState BowNormalAttackingState { get; }

    public PlayerAttackingStateMachine(Player player)
    {
        Player = player;
        ReusableData = new PlayerStateReusableData();

        RangedAttackingState = new PlayerRangedAttackingState(this);
        MeleeAttackingState = new PlayerMeleeAttackingState(this);

        AttackingIdleState = new PlayerAttackingIdleState(this);

        SwordAttackingState = new PlayerSwordAttackingState(this);
        SpearAttackingState = new PlayerSpearAttackingState(this);

        BowChargedAttackingState = new PlayerBowChargedAttackingState(this);
        BowNormalAttackingState = new PlayerBowNormalAttackingState(this);
    }
}
