using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Resolvers;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Barra_de_Vida : MonoBehaviour
{
    public int maxHealth = 300;
    public int currentHealth;
    public int defense = 10;
    public TextMeshProUGUI vidasText; // Refer�ncia ao TextMeshPro para exibir vidas

    private criarPoder playerController;

    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth; // In�cio com vida m�xima
        playerController = GetComponent<criarPoder>(); // Refer�ncia ao PlayerController
        UpdateVidasText(); // Atualizar texto inicial de vidas
    }

    void Update()
    {
        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead) return; // Se j� est� morto, n�o fazer nada
        isDead = true; // Marca o jogador como morto
        GameManager.vidas--; // Reduzir o n�mero de vidas
        UpdateVidasText(); // Atualizar o texto de vidas


        if (GameManager.vidas > 0)
        {
            // Recarregar a cena ou posicionar o jogador no in�cio
            UnityEngine.SceneManagement.SceneManager.LoadScene("Fase1");
        }
        else
        {
            // Redirecionar para a tela de Game Over
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
        }
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

    public void UpdateVidasText()
    {
        // Atualizar o texto do TextMeshPro com o n�mero de vidas restantes
        if (vidasText != null)
        {
            vidasText.text = "" + GameManager.vidas;
        }
    }
}