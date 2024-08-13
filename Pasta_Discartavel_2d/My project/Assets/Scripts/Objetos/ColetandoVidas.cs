using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColetandoVidas : MonoBehaviour
{
    public int lifeAmount = 1; // Quantidade de vidas que este item concede
    private bool isCollected = false; // Flag para verificar se o item j� foi coletado

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isCollected)
        {
            isCollected = true; // Marca o item como coletado

            // Aumentar o n�mero de vidas do jogador
            GameManeger2.vidas += lifeAmount;

            // Atualizar o texto de vidas
            Barra_de_Vida playerLife = FindObjectOfType<Barra_de_Vida>();
            if (playerLife != null)
            {
                playerLife.UpdateVidasText();
            }

            // Destruir o item ap�s a coleta
            Destroy(gameObject);
        }
    }
}
