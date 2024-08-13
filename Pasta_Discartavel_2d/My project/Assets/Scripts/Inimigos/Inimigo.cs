using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour
{
    public int damage = 10;         // Dano que o inimigo causa ao jogador
    public float attackCooldown = 1f; // Tempo de recarga entre ataques
    private float lastAttackTime;    // Tempo do último ataque realizado

    private Rigidbody2D rb;         // Referência ao Rigidbody2D do inimigo

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Inicializa a referência ao Rigidbody2D do inimigo

        // Verifica se o Rigidbody2D foi encontrado e exibe uma mensagem de erro no console, se não for encontrado
        // if (rb == null)
        // {
        //     Debug.LogError("Rigidbody2D não encontrado no inimigo!");
        // }
    }

    void Update()
    {
        // Aqui você pode adicionar qualquer lógica que precise ser verificada a cada quadro
        // No entanto, este script não requer nada específico no Update
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        // Verifica se o objeto com o qual o inimigo está colidindo tem a tag "Player"
        // e se o tempo atual é maior que o último ataque mais o tempo de recarga
        if (collision.gameObject.CompareTag("Player") && Time.time > lastAttackTime + attackCooldown)
        {
            // Obtém a referência ao script Barra_de_Vida do jogador
            Barra_de_Vida Vida = collision.gameObject.GetComponent<Barra_de_Vida>();

            if (Vida != null) // Verifica se o jogador tem o componente Barra_de_Vida
            {
                Vida.TakeDamage(damage); // Aplica o dano ao jogador
                lastAttackTime = Time.time; // Atualiza o tempo do último ataque para o tempo atual
            }
        }
    }
}
