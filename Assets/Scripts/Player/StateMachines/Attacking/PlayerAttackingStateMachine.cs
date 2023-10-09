public class PlayerAttackingStateMachine : StateMachine
{
    public Player Player { get; }
    public PlayerStateReusableData ReusableData { get; }

    public PlayerRangedAttackingState RangedAttackingState { get; }

    public PlayerMeleeNormalAttackingState MeleeNormalAttackingState { get; }

    public PlayerAttackingIdleState AttackingIdleState { get; }

    public PlayerSwordNormalAttackingState SwordNormalAttackingState { get; }
    public PlayerSpearNormalAttackingState SpearNormalAttackingState { get; }

    public PlayerBowChargedAttackingState BowChargedAttackingState { get; }
    public PlayerBowNormalAttackingState BowNormalAttackingState { get; }

    public PlayerAttackingStateMachine(Player player)
    {
        Player = player;
        ReusableData = new PlayerStateReusableData();

        RangedAttackingState = new PlayerRangedAttackingState(this);

        MeleeNormalAttackingState = new PlayerMeleeNormalAttackingState(this);

        AttackingIdleState = new PlayerAttackingIdleState(this);

        SwordNormalAttackingState = new PlayerSwordNormalAttackingState(this);
        SpearNormalAttackingState = new PlayerSpearNormalAttackingState(this);

        BowChargedAttackingState = new PlayerBowChargedAttackingState(this);
        BowNormalAttackingState = new PlayerBowNormalAttackingState(this);
    }
}
