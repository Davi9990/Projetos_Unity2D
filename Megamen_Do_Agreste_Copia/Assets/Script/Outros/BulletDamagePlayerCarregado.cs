using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamagePlayerCarregado : MonoBehaviour
{
    public int damage;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemySniper")
        {
            VidaInimigo inimigo = collision.gameObject.GetComponent<VidaInimigo>();
            //VidaInimigoCabe�a inimigo2 = collision.gameObject.GetComponent<VidaInimigoCabe�a>();
            VidaInimigoCabeça inimigo2 = collision.gameObject.GetComponent<VidaInimigoCabeça>();
            VidaShield inimigo3 = collision.gameObject.GetComponent<VidaShield>();
            VidaEnemyBoss inimigo4 = collision.gameObject.GetComponent<VidaEnemyBoss>();
            Vida_Enemy_Boss_Curupira inimigo5 = collision.gameObject.GetComponent<Vida_Enemy_Boss_Curupira>();
            Vida_Enemy_Boss_Iara inimigo6 = collision.gameObject.GetComponent<Vida_Enemy_Boss_Iara>();

            // Aplica dano ao inimigo se o componente existir
            if (inimigo != null)
            {
                inimigo.TakeDamege(damage); // Corrigido o nome do m�todo
            }

            // Aplica dano ao segundo tipo de inimigo, se o componente existir
            if (inimigo2 != null)
            {
                inimigo2.TakeDamage(damage); // Corrigido o nome do m�todo
            }

            if(inimigo3 != null)
            {
                inimigo3.TakeDamege(damage);
            }

            if(inimigo4 != null)
            {
                inimigo4.TakeDamage(damage);
            }

            if(inimigo5 != null)
            {
                inimigo5.TakeDamege(damage);
            }

            if(inimigo6 != null)
            {
                inimigo6.TakeDamege(damage);
            }

            Destroy(gameObject); // Destroi a bala ap�s a colis�o
        }

        if (collision.gameObject.tag == "EnemysBullets")
        {
            Destroy(gameObject); // Destroi a bala se colidir com outra bala inimiga
        }
    }
}
