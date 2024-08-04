using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriarPoderCogumelo : MonoBehaviour
{
    public int healthAmount = 1; // Quantidade de saúde que o item aumenta

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Certifique-se de que o player tenha a tag "Player"
        {
            Barra_de_Vida healthSystem = collision.GetComponent<Barra_de_Vida>();
            if (healthSystem != null)
            {
                healthSystem.IncreaseHealth(healthAmount);
                Destroy(gameObject); // Destroi o item após pegar
            }
        }
    }
}
