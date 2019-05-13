using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth { get; private set; }


    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage (int damage)
    {
        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage.");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die ()
    {
        // Die in some way
        // This method is ment to be overwritten
        Debug.Log(transform.name + " died.");
    }
}
