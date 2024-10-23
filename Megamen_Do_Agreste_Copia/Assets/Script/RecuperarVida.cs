using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecuperarVida : MonoBehaviour
{
    public int VidasParaAdicionar = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SistemaDeVida vida = collision.gameObject.GetComponent<SistemaDeVida>();

            if(vida != null)
            {
                vida.GanharVida(VidasParaAdicionar);

                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("Componente SistemaDeVida não encontrado no jogador");
            }
        }
    }
}
