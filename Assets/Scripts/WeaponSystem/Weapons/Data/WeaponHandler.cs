using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    private Player player;
    [SerializeField] private GameObject currentWeaponGO;
    [SerializeField] private WeaponSO currentWeaponSO;

    [SerializeField] private WeaponGround weaponGround;

    [SerializeField] private WeaponAffinity weaponAffinity;

    private Stat BaseAttack, PrimaryStat, SecondaryStat;

    void Start()
    {
        player = GetComponent<Player>();
        SetWeaponValues();
    }

    void SetWeaponValues() {
        currentWeaponGO = player.CurrentEquippedWeaponPrefab;
        currentWeaponSO = player.CurrentEquippedWeaponPrefab.GetComponent<WeaponHolder>().weaponSO;
        BaseAttack = currentWeaponSO.BaseAttack;
        SetWeaponSubstats();
        weaponGround = currentWeaponGO.GetComponent<WeaponGround>();
        weaponAffinity = currentWeaponGO.GetComponent<WeaponAffinity>();
    }

    private void SetWeaponSubstats()
    {
        switch (currentWeaponSO.SubStats.Count)
        {
            case 1:
                PrimaryStat = currentWeaponSO.SubStats[0];
                break;
            case 2:
                PrimaryStat = currentWeaponSO.SubStats[0];
                SecondaryStat = currentWeaponSO.SubStats[1];
                break;
            default:
                break;
        }
    }
 }
