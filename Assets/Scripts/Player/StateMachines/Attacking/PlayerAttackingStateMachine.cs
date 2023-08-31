public class PlayerAttackingStateMachine : StateMachine
{
    public Player Player { get; }
    public PlayerStateReusableData ReusableData { get; }

    public PlayerAttackingStateMachine(Player player)
    {
        Player = player;
        ReusableData = new PlayerStateReusableData();
    }
}
