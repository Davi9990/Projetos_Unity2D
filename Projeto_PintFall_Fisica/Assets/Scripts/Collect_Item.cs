using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour
{
    public int scoreValue = 1;
    public AudioClip itemsound;
    //private AudioSource itemSouce;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (ScoreManeger.Instance != null)
            {
                ScoreManeger.Instance.AddScore(scoreValue);
            }

            if(itemsound != null)
            {
                AudioSource.PlayClipAtPoint(itemsound, Camera.main.transform.position, 1.0f);
            }

            Destroy(gameObject);
        }
    }
}
