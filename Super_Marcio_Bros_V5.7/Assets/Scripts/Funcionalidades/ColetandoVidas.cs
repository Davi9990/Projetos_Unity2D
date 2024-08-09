using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColetandoVidas : MonoBehaviour
{
    public int lifeAmount = 1; // Quantidade de vidas que este item concede

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Aumentar o número de vidas do jogador
            GameManeger2.vidas += lifeAmount;

            // Atualizar o texto de vidas
            Barra_de_Vida playerLife = FindObjectOfType<Barra_de_Vida>();
            if (playerLife != null)
            {
                playerLife.UpdateVidasText();
            }

            // Destruir o item após a coleta
            Destroy(gameObject);
        }
    }
}
