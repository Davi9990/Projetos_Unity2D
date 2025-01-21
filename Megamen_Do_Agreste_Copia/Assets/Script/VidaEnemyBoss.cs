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
            Player = FindObjectOfType<PlayerLogica>(); // Busca refer�ncia ao script do Player
        }
    }

    void Update()
    {
        // Se necess�rio, voc� pode adicionar l�gicas extras aqui, como atualiza��es visuais de vida
    }

    // M�todo que gerencia o dano que o inimigo recebe, com verifica��o de tags
    public void TakeDamage(int damage, GameObject damageSource)
    {
        // Verifica se a tag do objeto que causou o dano est� no array de tags permitidas
        foreach (string tag in allowedTags)
        {
            if (damageSource.CompareTag(tag))
            {
                // Aplica o dano se a tag for v�lida
                currentHealth -= damage;

                // Checa se a vida do inimigo chegou a zero
                if (currentHealth <= 0)
                {
                    // Quando a vida chega a zero, o inimigo morre
                    Destroy(gameObject);

                    // Atualiza o progresso global
                    GameManager.Instance.Boitata = true;

                    // Troca para a tela de sele��o de fases
                    SceneManager.LoadScene("SelecaoDeFase");
                }

                return; // Sai do m�todo ap�s aplicar o dano
            }
        }

        // Opcional: Mensagem no console caso a tag n�o seja v�lida
        Debug.Log($"Dano ignorado. Tag '{damageSource.tag}' n�o permitida.");
    }

    // Exemplo de colis�o com verifica��o de tags
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Chama o m�todo TakeDamage apenas se o objeto colidido tiver uma tag v�lida
        TakeDamage(1, collision.gameObject);
    }
}
