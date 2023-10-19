using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Player player;
    private WeaponStats weaponStats;

    private void Awake()
    {
        player = transform.root.GetComponent<Player>();
        weaponStats = player.WeaponHandler.WeaponStats;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();

            float damage = weaponStats.Attack.TotalValue * player.Stats.Strength.TotalValue - 2 * (weaponStats.Attack.TotalValue + player.Stats.Strength.TotalValue);
            if (damage > 1)
            {
                enemy.Stats.Health.RemoveCurrentValue(damage);
            }
            else
            {
                enemy.Stats.Health.RemoveCurrentValue(1);
            }

            Debug.Log(damage);

            if (enemy.Stats.Health.CurrentValue <= 0)
            {
                Destroy(enemy.gameObject);
            }
        }
    }
}
