using UnityEngine;

public class Enemy : MonoBehaviour
{
    [field: Header("Stats")]
    [field: SerializeField] public EnemyStats Stats { get; private set; }

    private void Awake()
    {
        Stats.Initialize();
    }
}
