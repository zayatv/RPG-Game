using System;
using UnityEngine;

[Serializable]
public class WeaponHandler
{
    [field: SerializeField] public WeaponStats WeaponStats { get; private set; }
    public WeaponGround WeaponGround { get; private set; }
    public WeaponAffinity WeaponAffinity { get; private set; }

    private Player player;

    public void Initialize(Player player)
    {
        this.player = player;

        SetWeaponValues();
        SetWeaponStats();
    }

    private void SetWeaponValues() {
        WeaponGround = player.CurrentEquippedWeapon.WeaponPrefab.GetComponent<WeaponGround>();
        WeaponAffinity = player.CurrentEquippedWeapon.WeaponPrefab.GetComponent<WeaponAffinity>();
    }

    private void SetWeaponStats()
    {
        WeaponStats = player.CurrentEquippedWeapon.WeaponBaseStats;
    }
 }
