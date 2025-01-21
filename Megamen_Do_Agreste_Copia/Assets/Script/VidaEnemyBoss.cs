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

    // Array de tags permitidas para causar dano
    public string[] allowedTags;

    void Start()
    {
        currentHealth = maxHealth;
        if (Player == null)
        {
            Player = FindObjectOfType<PlayerLogica>(); // Busca referência ao script do Player
        }
    }

    void Update()
    {
        // Se necessário, você pode adicionar lógicas extras aqui, como atualizações visuais de vida
    }

    // Método que gerencia o dano que o inimigo recebe, com verificação de tags
    public void TakeDamage(int damage, GameObject damageSource)
    {
        // Verifica se a tag do objeto que causou o dano está no array de tags permitidas
        foreach (string tag in allowedTags)
        {
            if (damageSource.CompareTag(tag))
            {
                // Aplica o dano se a tag for válida
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

                return; // Sai do método após aplicar o dano
            }
        }

        // Opcional: Mensagem no console caso a tag não seja válida
        Debug.Log($"Dano ignorado. Tag '{damageSource.tag}' não permitida.");
    }

    // Exemplo de colisão com verificação de tags
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Chama o método TakeDamage apenas se o objeto colidido tiver uma tag válida
        TakeDamage(1, collision.gameObject);
    }
}
