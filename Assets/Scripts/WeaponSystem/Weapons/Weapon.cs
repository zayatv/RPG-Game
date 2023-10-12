using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        player = transform.root.GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("Hit: " + other.name);
        }
    }
}
