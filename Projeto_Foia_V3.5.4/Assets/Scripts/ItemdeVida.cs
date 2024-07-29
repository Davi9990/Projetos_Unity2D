using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemdeVida : MonoBehaviour
{
    public int vidaParaAdicionar = 1; // Quantidade de vida a ser adicionada

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HeathSystem healthSystem = collision.gameObject.GetComponent<HeathSystem>();

            if (healthSystem != null)
            {
                healthSystem.vida += vidaParaAdicionar;

                if (healthSystem.vida > healthSystem.vidaMaxima)
                {
                    healthSystem.vida = healthSystem.vidaMaxima;
                }

                Destroy(gameObject); // Destroi o item após a coleta
            }
        }
    }
}
