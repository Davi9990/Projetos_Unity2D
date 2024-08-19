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

                Destroy(gameObject); // Destr�i o item ap�s a coleta
            }
            else
            {
                Debug.LogError("Componente HealthSystem n�o encontrado no jogador.");
            }
        }
    }
}
