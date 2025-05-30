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
            // Aumentar o n�mero de vidas do jogador
            GameManager.vidas += lifeAmount;

            // Atualizar o texto de vidas, se necess�rio
            Barra_de_Vida playerLife = other.GetComponent<Barra_de_Vida>();
            if (playerLife != null)
            {
                playerLife.UpdateVidasText();
            }

            // Destruir o item ap�s a coleta
            Destroy(gameObject);
        }
    }
}
