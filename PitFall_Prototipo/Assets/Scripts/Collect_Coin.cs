using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect_Coin : MonoBehaviour
{
    public int scoreValue = 2000;//Valor do  item

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (ScoreManeger.Instance != null)
            {
                ScoreManeger.Instance.AddScore(scoreValue);
            }

            Destroy(gameObject);
        } 
    }
}
