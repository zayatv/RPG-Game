using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Custom/Weapons", order = 1)]

public class WeaponSO : ScriptableObject
{
    [field: Header("General Information")]
    [field: SerializeField] public string WeaponName { get; private set; }
    [field: SerializeField] public string WeaponDescription { get; private set; }
    [field: SerializeField] public Rarity Rarity { get; private set; }
    [field: SerializeField] public GameObject WeaponModel { get; private set; }

    [field: Header("Stats")]
    [field: SerializeField] public Stat BaseAttack { get; private set; }
    [field: SerializeField] public Stat PrimaryStat { get; private set; }
    [field: SerializeField] public Stat SecondaryStat { get; private set; }
}
