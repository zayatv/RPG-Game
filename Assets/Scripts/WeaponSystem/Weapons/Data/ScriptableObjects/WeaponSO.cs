using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Custom/Weapons/Weak/Weapon", order = 1)]

public class WeaponSO : ScriptableObject
{
    [field: Header("General Information")]
    [field: SerializeField] public string WeaponName { get; private set; }
    [field: SerializeField] public string WeaponDescription { get; private set; }
    [field: SerializeField] public Rarity Rarity { get; private set; }
    [field: SerializeField] public WeaponType WeaponType { get; private set; }
    [field: SerializeField] public GameObject WeaponPrefab { get; private set; }

    [field: Header("Stats")]
    [field: SerializeField] public WeaponStats WeaponBaseStats { get; private set; }
    [field: SerializeField] public List<StatType> SubStats { get; private set; }

    [field: Header("Runestones")]
    [field: SerializeField] public List<string> EquippedRunestones { get; private set; }
}
