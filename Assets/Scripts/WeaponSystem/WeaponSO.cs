using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Custom/Weapons", order = 1)]

public class WeaponSO : ScriptableObject
{
    [field: SerializeField] public string WeaponName { get; private set; }
    [field: SerializeField] public string WeaponDescription { get; private set; }
    [field: SerializeField] public GameObject WeaponModel { get; private set; }
}
