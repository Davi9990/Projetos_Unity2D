using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemdeVida : MonoBehaviour
{
    public int vidaParaAdicionar = 1; // Quantidade de vida a ser adicionada

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Jogador"))
        {
            HealthSystem healthSystem = collision.gameObject.GetComponent<HealthSystem>();

            if (healthSystem != null)
            {
                healthSystem.GanharVida(vidaParaAdicionar);

                Destroy(gameObject); // Destrói o item após a coleta
            }
            else
            {
                Debug.LogError("Componente HealthSystem não encontrado no jogador.");
            }
        }
    }
}
