using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecuperandoVida : MonoBehaviour
{
    public int vidasParaAdicionar = 1; // Quantidade de vidas para adicionar ao jogador

    // Método chamado quando o objeto colide com outro
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o objeto colidido é o jogador
        if (other.CompareTag("Player"))
        {
            // Obtém o componente HealthSystem do jogador
            SistemaDeVida healthSystem = other.GetComponent<SistemaDeVida>();

            if (healthSystem != null)
            {
                // Adiciona vidas ao jogador
                healthSystem.GanharVida(vidasParaAdicionar);
                healthSystem.AtualizarHudDeVida(); // Atualiza o texto de vidas na HUD

                // Destroi o objeto que concedeu a vida extra
                Destroy(gameObject);
            }
        }
    }
}
