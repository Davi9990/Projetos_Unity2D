using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    public int damage = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SistemaDeVida vida = collision.gameObject.GetComponent<SistemaDeVida>();

            if (vida != null)
            {
                vida.vida -= damage;
            }

            Destroy(gameObject);
        }

        if(collision.gameObject.tag == "BalasPlayer" || collision.gameObject.tag == "EnemySniper")
        {
            Destroy(gameObject);
        }
    }
}
