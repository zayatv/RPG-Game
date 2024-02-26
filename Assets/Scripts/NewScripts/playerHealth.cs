using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth = 0;

    private void Start()
    {
        currentHealth = maxHealth;
    }
    //Damage Player Call From Enemy Script
   public void damage(int damage)
    {
        currentHealth -= damage;
        if(currentHealth<0)
        {
            //DAMAGE PLAYER
        }
    }
}
