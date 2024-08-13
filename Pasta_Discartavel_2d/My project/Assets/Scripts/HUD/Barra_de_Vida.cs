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
    public int maxHealth = 300; // Valor máximo de vida que o jogador pode ter
    public int currentHealth; // Vida atual do jogador
    public int defense = 10; // Defesa do jogador, reduz o dano recebido
    public TextMeshProUGUI vidasText; // Referência ao componente TextMeshPro para exibir o número de vidas na tela

    private criarPoder playerController; // Referência ao script que controla os poderes do jogador
    private bool isDead = false; // Verifica se o jogador está morto

    // Referência ao cronômetro do jogo (atribuir pelo Inspector)
    public Cronometro countdownTimer;

    // Referência à câmera (atribuir pelo Inspector ou encontrar manualmente)
    public Camera cameraController;

    void Start()
    {
        currentHealth = maxHealth; // Inicia o jogo com a vida máxima
        playerController = GetComponent<criarPoder>(); // Obtém a referência ao script criarPoder
        UpdateVidasText(); // Atualiza o texto que mostra o número de vidas na tela

        // Adiciona a função OnSceneLoaded para ser chamada sempre que uma nova cena for carregada
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        // Remove a função OnSceneLoaded do evento sceneLoaded para evitar chamadas indesejadas
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Update()
    {
        // Se a vida do jogador chegar a zero e ele não estiver marcado como morto, chama o método Die
        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    public void Die()
    {
        // Se o jogador já estiver morto, não faz nada
        if (isDead) return;

        isDead = true; // Marca o jogador como morto
        GameManeger2.vidas--; // Diminui o número de vidas no gerenciador de jogo
        UpdateVidasText(); // Atualiza o texto de vidas na tela

        // Se ainda houver vidas restantes, o jogador é reposicionado no último checkpoint
        if (GameManeger2.vidas > 0)
        {
            Respawn();
        }
        else
        {
            // Se não houver vidas restantes, carrega a cena de Game Over
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
        }
    }

    public void TakeDamage(int damage)
    {
        // Calcula o dano real considerando a defesa do jogador
        int realDamage = Mathf.Max(damage - defense, 0);
        currentHealth -= realDamage; // Reduz a vida atual do jogador pelo dano real

        // Se a vida do jogador cair a zero, chama o método Die
        if (currentHealth <= 0)
        {
            currentHealth = 0; // Garante que a vida não seja negativa
            Die();
        }
        else
        {
            // Se o jogador ainda tiver vida, ele perde a capacidade de disparar projéteis
            playerController.liberaPoder = false;
        }
    }

    public void IncreaseHealth(int amount)
    {
        // Aumenta a vida do jogador, garantindo que ela não ultrapasse o valor máximo
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth; // Garante que a vida não ultrapasse o máximo
        }
    }

    public void UpdateVidasText()
    {
        // Atualiza o texto do TextMeshPro com o número de vidas restantes
        if (vidasText != null)
        {
            vidasText.text = GameManeger2.vidas.ToString(); // Converte o número de vidas para string e exibe na tela
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o jogador colidiu com um checkpoint
        if (other.CompareTag("Checkpoint"))
        {
            // Atualiza a posição do último checkpoint no gerenciador de checkpoints
            CheckPointManager.Instance.RespawnPosition = other.transform.position;
        }
    }

    private void Respawn()
    {
        // Reposiciona o jogador na posição do último checkpoint salvo
        transform.position = CheckPointManager.Instance.RespawnPosition;
        currentHealth = maxHealth; // Restaura a vida do jogador ao máximo
        isDead = false; // Marca o jogador como vivo novamente

        // Atualiza a posição do jogador na câmera
        if (cameraController != null)
        {
            cameraController.GetComponent<Camera>().UpdatePlayer(transform);
        }
        else
        {
            //Debug.LogError("Referência à CameraController não está atribuída.");
        }

        // Reseta o cronômetro se ele estiver disponível
        if (countdownTimer != null)
        {
            countdownTimer.ResetTimer(countdownTimer.timeRemaining);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Atualiza o texto de vidas sempre que uma nova cena é carregada
        UpdateVidasText();

        // Restaura a posição do jogador no último checkpoint salvo
        if (CheckPointManager.Instance != null)
        {
            transform.position = CheckPointManager.Instance.RespawnPosition;
        }
    }
}