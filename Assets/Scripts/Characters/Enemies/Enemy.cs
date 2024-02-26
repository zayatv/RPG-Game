using UnityEngine;

public class Enemy : MonoBehaviour
{
    [field: Header("Stats")]
    [field: SerializeField] public EnemyStats Stats { get; private set; }

    private void Awake()
    {
        Stats.Initialize();
    }

    public void damage(int dmgAmmount)
    {
        Stats.Health.CurrentValue -= dmgAmmount;

        if(Stats.Health.CurrentValue<=0)
        {
            //KILL Enemy;
            
        }
    }
}
