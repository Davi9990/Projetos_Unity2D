using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect_Coin : MonoBehaviour
{
    public int scoreValue;//Valor do  item

    //Creatina 1000
    //Batata Doce 2000
    //Suco 8000

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Player_Grande"))
        {
            if (ScoreManeger.Instance != null)
            {
                ScoreManeger.Instance.AddScore(scoreValue);
                //Movimentacao.pontuacao += scoreValue;
            }

            Destroy(gameObject);
        } 
    }
}
