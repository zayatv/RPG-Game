using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Custom/Player/Player")]
public class PlayerSO : ScriptableObject
{
    [field: SerializeField] public PlayerGroundedData GroundedData { get; private set; }
    [field: SerializeField] public PlayerAirborneData AirborneData { get; private set; }

    [field: SerializeField] public PlayerAttackData AttackData { get; private set; }
}
