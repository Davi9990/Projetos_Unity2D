using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour
{
    public int scoreValue = 1;

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
