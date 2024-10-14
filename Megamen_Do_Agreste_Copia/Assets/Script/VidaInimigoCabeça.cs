using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaInimigoCabeça : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth;
    public CabeçaVoadora voadora;

    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Respawn();
        }
    }

    void Respawn()
    {
        currentHealth = maxHealth; //Restaura a vida
        voadora.TeleportToStart(); //Teleporta o inimigo para o ponto de partidas
    }
}
