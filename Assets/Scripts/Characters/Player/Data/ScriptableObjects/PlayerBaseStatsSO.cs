using UnityEngine;

[CreateAssetMenu(fileName = "PlayerBaseStats", menuName = "Custom/Player/Stats/PlayerBaseStats", order = 1)]
public class PlayerBaseStatsSO : ScriptableObject
{
    [field: SerializeField] public int PlayerBaseHealth { get; set; }
    [field: SerializeField] public int PlayerBaseMana { get; set; }
    [field: SerializeField] public int PlayerBaseStrength { get; set; }
    [field: SerializeField] public int PlayerBaseDefense { get; set; }
    [field: SerializeField] public int PlayerBaseAttackSpeed { get; set; }
    [field: SerializeField] public int PlayerBaseCriticalHitChance { get; set; }
    [field: SerializeField] public int PlayerBaseCriticalHitDamage { get; set; }
}
