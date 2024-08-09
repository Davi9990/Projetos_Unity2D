using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolaDeFogo : MonoBehaviour
{
    public int damage = 10; // Dano que a bola de fogo causa ao jogador

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Aplicar dano ao jogador
            Barra_de_Vida vida = collision.gameObject.GetComponent<Barra_de_Vida>();
            if (vida != null)
            {
                vida.TakeDamage(damage);
            }

            // Destruir a bola de fogo após colidir com o jogador
            Destroy(gameObject);
        }
    }
}
