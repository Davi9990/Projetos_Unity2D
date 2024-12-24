using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queimar : MonoBehaviour
{
    public int damage = 1; // Dano que será aplicado ao jogador.
    public float attackCooldown = 1f; // Tempo de espera entre ataques.
    private bool podeAtacar = true; // Controle de cooldown.

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (podeAtacar && VerificarJogador(collision))
        {
            AplicarDano(collision);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (podeAtacar && VerificarJogador(collision))
        {
            AplicarDano(collision);
        }
    }

    private bool VerificarJogador(Collider2D collision)
    {
        // Verifica se o objeto colidido é o jogador.
        return collision.CompareTag("Player") ||
               collision.CompareTag("Player_Grande") ||
               collision.CompareTag("Player_Giga");
    }

    private void AplicarDano(Collider2D collision)
    {
        SistemaDeVida vida = collision.GetComponent<SistemaDeVida>();
        if (vida != null)
        {
            vida.vida -= damage; // Aplica dano.
            vida.AtualizarHudDeVida(); // Atualiza a HUD de vida.
            Debug.Log("Dano aplicado ao jogador.");
            StartCoroutine(RecarregarAtaque());
        }
        else
        {
            Debug.LogWarning("Componente SistemaDeVida não encontrado no jogador!");
        }
    }

    private IEnumerator RecarregarAtaque()
    {
        podeAtacar = false; // Bloqueia novos ataques.
        yield return new WaitForSeconds(attackCooldown); // Espera o cooldown.
        podeAtacar = true; // Libera novos ataques.
        Debug.Log("Cooldown finalizado. Pronto para atacar novamente.");
    }
}
