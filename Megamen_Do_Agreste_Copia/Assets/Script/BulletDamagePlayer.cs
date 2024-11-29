using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamagePlayer : MonoBehaviour
{
    //public int damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemySniper")
        {
            VidaInimigo inimigo = collision.gameObject.GetComponent<VidaInimigo>();
            //VidaInimigoCabe�a inimigo2 = collision.gameObject.GetComponent<VidaInimigoCabe�a>();
            VidaInimigoCabeça inimigo2 = collision.gameObject.GetComponent<VidaInimigoCabeça>();
            VidaShield inimigo3 = collision.gameObject.GetComponent<VidaShield>();

            // Aplica dano ao inimigo se o componente existir
            if (inimigo != null)
            {
                inimigo.TakeDamege(1); // Corrigido o nome do m�todo
            }

            // Aplica dano ao segundo tipo de inimigo, se o componente existir
            if (inimigo2 != null)
            {
                inimigo2.TakeDamage(1); // Corrigido o nome do m�todo
            }

            if(inimigo3 != null)
            {
                inimigo3.TakeDamege(1);
            }

            Destroy(gameObject); // Destroi a bala ap�s a colis�o
        }

        if (collision.gameObject.tag == "EnemysBullets")
        {
            Destroy(gameObject); // Destroi a bala se colidir com outra bala inimiga
        }
    }
}
