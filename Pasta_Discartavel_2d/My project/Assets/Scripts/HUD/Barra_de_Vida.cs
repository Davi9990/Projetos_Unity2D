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
    public int maxHealth = 300; // Valor m�ximo de vida que o jogador pode ter
    public int currentHealth; // Vida atual do jogador
    public int defense = 10; // Defesa do jogador, reduz o dano recebido
    public TextMeshProUGUI vidasText; // Refer�ncia ao componente TextMeshPro para exibir o n�mero de vidas na tela

    private criarPoder playerController; // Refer�ncia ao script que controla os poderes do jogador
    private bool isDead = false; // Verifica se o jogador est� morto

    // Refer�ncia ao cron�metro do jogo (atribuir pelo Inspector)
    public Cronometro countdownTimer;

    // Refer�ncia � c�mera (atribuir pelo Inspector ou encontrar manualmente)
    public Camera cameraController;

    void Start()
    {
        currentHealth = maxHealth; // Inicia o jogo com a vida m�xima
        playerController = GetComponent<criarPoder>(); // Obt�m a refer�ncia ao script criarPoder
        UpdateVidasText(); // Atualiza o texto que mostra o n�mero de vidas na tela

        // Adiciona a fun��o OnSceneLoaded para ser chamada sempre que uma nova cena for carregada
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        // Remove a fun��o OnSceneLoaded do evento sceneLoaded para evitar chamadas indesejadas
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Update()
    {
        // Se a vida do jogador chegar a zero e ele n�o estiver marcado como morto, chama o m�todo Die
        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    public void Die()
    {
        // Se o jogador j� estiver morto, n�o faz nada
        if (isDead) return;

        isDead = true; // Marca o jogador como morto
        GameManeger2.vidas--; // Diminui o n�mero de vidas no gerenciador de jogo
        UpdateVidasText(); // Atualiza o texto de vidas na tela

        // Se ainda houver vidas restantes, o jogador � reposicionado no �ltimo checkpoint
        if (GameManeger2.vidas > 0)
        {
            Respawn();
        }
        else
        {
            // Se n�o houver vidas restantes, carrega a cena de Game Over
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
        }
    }

    public void TakeDamage(int damage)
    {
        // Calcula o dano real considerando a defesa do jogador
        int realDamage = Mathf.Max(damage - defense, 0);
        currentHealth -= realDamage; // Reduz a vida atual do jogador pelo dano real

        // Se a vida do jogador cair a zero, chama o m�todo Die
        if (currentHealth <= 0)
        {
            currentHealth = 0; // Garante que a vida n�o seja negativa
            Die();
        }
        else
        {
            // Se o jogador ainda tiver vida, ele perde a capacidade de disparar proj�teis
            playerController.liberaPoder = false;
        }
    }

    public void IncreaseHealth(int amount)
    {
        // Aumenta a vida do jogador, garantindo que ela n�o ultrapasse o valor m�ximo
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth; // Garante que a vida n�o ultrapasse o m�ximo
        }
    }

    public void UpdateVidasText()
    {
        // Atualiza o texto do TextMeshPro com o n�mero de vidas restantes
        if (vidasText != null)
        {
            vidasText.text = GameManeger2.vidas.ToString(); // Converte o n�mero de vidas para string e exibe na tela
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o jogador colidiu com um checkpoint
        if (other.CompareTag("Checkpoint"))
        {
            // Atualiza a posi��o do �ltimo checkpoint no gerenciador de checkpoints
            CheckPointManager.Instance.RespawnPosition = other.transform.position;
        }
    }

    private void Respawn()
    {
        // Reposiciona o jogador na posi��o do �ltimo checkpoint salvo
        transform.position = CheckPointManager.Instance.RespawnPosition;
        currentHealth = maxHealth; // Restaura a vida do jogador ao m�ximo
        isDead = false; // Marca o jogador como vivo novamente

        // Atualiza a posi��o do jogador na c�mera
        if (cameraController != null)
        {
            cameraController.GetComponent<Camera>().UpdatePlayer(transform);
        }
        else
        {
            //Debug.LogError("Refer�ncia � CameraController n�o est� atribu�da.");
        }

        // Reseta o cron�metro se ele estiver dispon�vel
        if (countdownTimer != null)
        {
            countdownTimer.ResetTimer(countdownTimer.timeRemaining);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Atualiza o texto de vidas sempre que uma nova cena � carregada
        UpdateVidasText();

        // Restaura a posi��o do jogador no �ltimo checkpoint salvo
        if (CheckPointManager.Instance != null)
        {
            transform.position = CheckPointManager.Instance.RespawnPosition;
        }
    }
}