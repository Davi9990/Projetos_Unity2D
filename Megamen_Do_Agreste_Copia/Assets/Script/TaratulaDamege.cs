using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaratulaDamege : MonoBehaviour
{
    //public SistemaDeVida vida;
    public int damage = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            SistemaDeVida vida = collision.gameObject.GetComponent<SistemaDeVida>();

            if (vida != null)
            {
                vida.vida -= damage;
            }
        }
    }
}
