using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Resolvers;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Diagnostics;

public class Barra_de_Vida : MonoBehaviour
{
    public int maxHealth = 300;
    public int currentHealth;
    public int defense = 10;
    public TextMeshProUGUI vidasText; // Referência ao TextMeshPro para exibir vidas

    private criarPoder playerController;
    private bool isDead = false;

    // Referência ao cronômetro e atribua pelo Inspector
    public Cronometro countdownTimer;

    // Referência à câmera, atribua pelo Inspector ou encontre manualmente
    public Camera cameraController;

    void Start()
    {
        currentHealth = maxHealth; // Início com vida máxima
        playerController = GetComponent<criarPoder>(); // Referência ao PlayerController
        UpdateVidasText(); // Atualizar texto inicial de vidas

        // Adicionar a função que será chamada quando a cena for carregada
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        // Remover a função do evento para evitar chamadas inesperadas
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Update()
    {
        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    public void Die()
    {
        if (isDead) return; // Se já está morto, não fazer nada
        isDead = true; // Marca o jogador como morto
        GameManeger2.vidas--; // Reduzir o número de vidas
        UpdateVidasText(); // Atualizar o texto de vidas

        if (GameManeger2.vidas > 0)
        {
            Respawn(); // Reposicionar o jogador no último checkpoint
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
            playerController.liberaPoder = false; // Perde a capacidade de disparar projéteis
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
        // Atualizar o texto do TextMeshPro com o número de vidas restantes
        if (vidasText != null)
        {
            vidasText.text = GameManeger2.vidas.ToString();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Se o jogador colidir com um checkpoint
        if (other.CompareTag("Checkpoint"))
        {
            CheckPointManager.Instance.RespawnPosition = other.transform.position; // Atualizar posição do último checkpoint
        }
    }

    private void Respawn()
    {
        // Reposicionar o jogador na posição do checkpoint
        transform.position = CheckPointManager.Instance.RespawnPosition;
        currentHealth = maxHealth; // Restaurar vida máxima
        isDead = false; // Resetar estado de morte

        // Atualizar a posição do jogador na câmera
        if (cameraController != null)
        {
            cameraController.GetComponent<Camera>().UpdatePlayer(transform);
        }
        else
        {
            //Debug.LogError("Referência à CameraController não está atribuída.");
        }

        // Resetar o cronômetro
        if (countdownTimer != null)
        {
            countdownTimer.ResetTimer(countdownTimer.timeRemaining);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Atualizar o texto de vidas quando uma nova cena for carregada
        UpdateVidasText();

        // Restaurar a posição do jogador
        if (CheckPointManager.Instance != null)
        {
            transform.position = CheckPointManager.Instance.RespawnPosition;
        }
    }
}