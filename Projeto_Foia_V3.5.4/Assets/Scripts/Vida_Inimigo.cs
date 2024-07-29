using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public int maxHealth = 1000; // Vida máxima do inimigo
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth; // Inicializa a vida atual com a vida máxima
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Reduz a vida do inimigo

        // Verifica se a vida do inimigo chegou a zero
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //gameObject.SetActive(false);
         Destroy(gameObject);
    }
}
