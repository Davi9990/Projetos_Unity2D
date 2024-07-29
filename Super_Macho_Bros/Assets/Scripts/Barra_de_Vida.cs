using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Resolvers;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Barra_de_Vida : MonoBehaviour
{
    public int maxHealth = 300;
    public int currentHealth;
    public int defense = 10;

    private criarPoder playerController;

    void Start()
    {
        currentHealth = 1; // In�cio com 1 ponto de vida
        playerController = GetComponent<criarPoder>(); // Refer�ncia ao PlayerController
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Implementar l�gica de morte, por exemplo, recarregar a cena
        UnityEngine.SceneManagement.SceneManager.LoadScene("Fase1");
    }

    public void TakeDamage(int damage)
    {
        int realDamage = Mathf.Max(damage - defense, 0);
        currentHealth -= realDamage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        else
        {
            playerController.liberaPoder = false; // Perde a capacidade de disparar proj�teis
        }
    }

    public void IncreaseHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}