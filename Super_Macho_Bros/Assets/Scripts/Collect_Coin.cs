using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect_Coin : MonoBehaviour
{
    public int scoreValue = 10; // Valor da moeda

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Adiciona a pontuação ao ScoreManager
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddScore(scoreValue);
            }

            // Destrói o objeto da moeda
            Destroy(gameObject);
        }
    }
}
