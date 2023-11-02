using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "Custom/Player/Stats")]
public class PlayerStatsSO : ScriptableObject
{
    [field: SerializeField] public Stat Health { get; private set; }
    [field: SerializeField] public Stat Defense { get; private set; }
    [field: SerializeField] public Stat Strength { get; private set; }
    [field: SerializeField] public Stat AttackSpeed { get; private set; }
    [field: SerializeField] public Stat CriticalHitChance { get; private set; }
    [field: SerializeField] public Stat CriticalHitDamage { get; private set; }
}
