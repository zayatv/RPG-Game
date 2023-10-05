using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    private Player player;
    [SerializeField] private GameObject currentWeaponGO;
    [SerializeField] private WeaponSO currentWeaponSO;

    [SerializeField] private WeaponGround weaponGround;

    [SerializeField] private WeaponAffinity weaponAffinity;

    private Stat BaseAttack, PrimaryStat, SecondaryStat;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        SetWeaponValues();
    }

    void SetWeaponValues() {
        currentWeaponGO = player.CurrentEquippedWeaponPrefab;
        currentWeaponSO = player.CurrentEquippedWeaponPrefab.GetComponent<WeaponHolder>().weaponSO;
        BaseAttack = currentWeaponSO.BaseAttack;
        PrimaryStat = currentWeaponSO.PrimaryStat;
        SecondaryStat = currentWeaponSO.SecondaryStat;
        weaponGround = currentWeaponGO.GetComponent<WeaponGround>();
        weaponAffinity = currentWeaponGO.GetComponent<WeaponAffinity>();
    }
 }
