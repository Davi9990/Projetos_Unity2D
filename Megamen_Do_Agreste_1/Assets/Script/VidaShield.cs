using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaShield : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth;

    private EnemyShild enemyShild;


    void Start()
    {
        currentHealth = maxHealth;

        enemyShild = GetComponent<EnemyShild>();
    }

    // Update is called once per frame
    public void TakeDamege(int damage)
    {
        if (enemyShild != null && !enemyShild.isApproaching)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Debug.Log("Inimigo é imortal enquanto se aproxima do jogador!");
        }
    }
}
