using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidasExtras : MonoBehaviour
{
    public int vidasParaAdicionar = 1; // Quantidade de vidas para adicionar ao jogador

    // Método chamado quando o objeto colide com outro
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o objeto colidido é o jogador
        if (other.CompareTag("Player"))
        {
            // Obtém o componente HealthSystem do jogador
            HeathSystem healthSystem = other.GetComponent<HeathSystem>();

            if (healthSystem != null)
            {
                // Adiciona vidas ao jogador
                healthSystem.vidasRestantes += vidasParaAdicionar;
                healthSystem.UpdateVidasText(); // Atualiza o texto de vidas na HUD

                // Destroi o objeto que concedeu a vida extra
                Destroy(gameObject);
            }
        }
    }
}
