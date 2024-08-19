using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjetilDano : MonoBehaviour
{
    private Vector2 direcao;
    private LayerMask inimigoLayers;
    private float ataqueRange;
    private float tempoDeVida = 3f; // Tempo de vida do proj�til

    public void Inicializar(Vector2 direcao, LayerMask inimigoLayers, float ataqueRange)
    {
        this.direcao = direcao;
        this.inimigoLayers = inimigoLayers;
        this.ataqueRange = ataqueRange;

        // Inicia a contagem regressiva para destruir o proj�til ap�s 3 segundos
        Destroy(gameObject, tempoDeVida);
    }

    void Update()
    {
        // Detecta inimigos no alcance do proj�til
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, ataqueRange, inimigoLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.GetComponent<BossHpManager>() != null)
            {
                enemy.GetComponent<BossHpManager>().ReceberDano(10); // Aplica dano ao boss
                DestruirProjetil(); // Destr�i o proj�til ao atingir um inimigo
            }
            else if (enemy.GetComponent<Enemy2>() != null)
            {
                enemy.GetComponent<Enemy2>().ReceberDano(5); // Aplica dano aos inimigos comuns
                DestruirProjetil(); // Destr�i o proj�til ao atingir um inimigo
            }
            else if (enemy.GetComponent<Projetil>() != null)
            {
                enemy.GetComponent<Projetil>().ReceberDano(1); // Aplica dano ao proj�til inimigo
                DestruirProjetil(); // Destr�i o proj�til ao atingir outro proj�til
            }

            // Aplica knockback ou outro efeito
            ApplyKnockback(enemy);
        }
    }

    void ApplyKnockback(Collider2D enemy)
    {
        Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 knockbackDirection = (enemy.transform.position - transform.position).normalized;
            rb.AddForce(knockbackDirection * 10f, ForceMode2D.Impulse); // Aplica for�a de knockback
        }
    }

    void DestruirProjetil()
    {
        // Destroi o proj�til ao atingir um inimigo
        Destroy(gameObject);
    }

    // Desenhar o raio para visualiza��o
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, ataqueRange);
    }
}
