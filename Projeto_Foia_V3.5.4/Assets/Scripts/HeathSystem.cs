using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class HeathSystem : MonoBehaviour
{public int vida;
    public int vidaMaxima;
    public int vidasRestantes; // Número de vidas restantes do jogador
    public TextMeshProUGUI vidasText; // Referência para o TextMeshPro que exibirá as vidas

    public Image[] coracao;
    public Sprite cheio;
    public Sprite vazio;

    private Vector3 checkpointPosition;

    void Start()
    {
        // Inicializa o checkpoint na posição inicial do jogador
        checkpointPosition = transform.position;
        UpdateVidasText();
    }

    void Update()
    {
        HealthLogic();
        DeadState();
    }

    void HealthLogic()
    {
        if (vida > vidaMaxima)
        {
            vida = vidaMaxima;
        }

        for (int i = 0; i < coracao.Length; i++)
        {
            if (i < vida)
            {
                coracao[i].sprite = cheio;
            }
            else
            {
                coracao[i].sprite = vazio;
            }

            if (i < vidaMaxima)
            {
                coracao[i].enabled = true;
            }
            else
            {
                coracao[i].enabled = false;
            }
        }
    }

    void DeadState()
    {
        if (vida <= 0)
        {
            vidasRestantes--;

            if (vidasRestantes > 0)
            {
                // Reseta a posição do jogador para o checkpoint e restaura a vida
                transform.position = checkpointPosition;
                vida = vidaMaxima;
            }
            else
            {
                // Chama a tela de Game Over
                GameOver();
            }

            UpdateVidasText();
        }
    }

    public void UpdateVidasText()
    {
        vidasText.text = ""+ vidasRestantes;
    }

    void GameOver()
    {
        // Carrega a cena de Game Over (substitua "GameOverScene" pelo nome da sua cena de Game Over)
        SceneManager.LoadScene("Game_Over");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            checkpointPosition = other.transform.position;
            // Trocar a cor do sprite do checkpoint
            SpriteRenderer spriteRenderer = other.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.color = Color.blue; // Troque para a cor desejada
            }

            // Desativar o colisor do checkpoint
            Collider2D collider = other.GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.enabled = false;
            }
        }
    }
}
