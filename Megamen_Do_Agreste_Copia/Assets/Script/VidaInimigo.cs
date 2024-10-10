using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaInimigo : MonoBehaviour
{
    public int maxHealth = 5;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    public void TakeDamege(int damage)
    {
        currentHealth -= damage;

        if(currentHealth < 0)
        {
            Destroy(gameObject);
        }
    }
}
