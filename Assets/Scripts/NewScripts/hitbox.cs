using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitbox : MonoBehaviour
{
    public Enemy enemy;
    public int damageAmmount = 0;

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag=="weapon")
        {
            //Debug.Log("Damage");
            enemy.damage(damageAmmount);
        }
    }
}
