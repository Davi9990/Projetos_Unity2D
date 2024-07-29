using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect_Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Adicione lógica de coleta aqui, como aumentar a pontuação
            Destroy(gameObject);
        }
    }
}
