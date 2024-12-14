using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VidaEnemyBoss : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth;
    public bool BoitataON;

    public PlayerLogica Player;

    void Start()
    {
        currentHealth = maxHealth;
        if (Player == null)
        {
            Player = FindObjectOfType<PlayerLogica>(); //Busca referência ao script do Player
        }
    }

    void Update()
    {
        // Se necessário, você pode adicionar lógicas extras aqui, como atualizações visuais de vida
    }

    // Método que gerencia o dano que o inimigo recebe
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Checa se a vida do inimigo chegou a zero
        if (currentHealth <= 0)
        {
            // Quando a vida chega a zero, o inimigo morre
            Destroy(gameObject);

            // Atualiza o progresso global
            GameManager.Instance.Boitata = true;

            // Troca para a tela de seleção de fases
            SceneManager.LoadScene("SelecaoDeFase");
        }
    }

    //// Método que lida com as colisões do inimigo
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    // Verifica se a colisão é com o ataque do jogador
    //    if (collision.gameObject.CompareTag("BalasPlayer"))
    //    {
    //        // Aplica dano quando o ataque do jogador colide com o inimigo
    //        TakeDamage(1);  // 1 é o dano aplicado, pode ser alterado conforme necessário
    //    }
    //}
}
