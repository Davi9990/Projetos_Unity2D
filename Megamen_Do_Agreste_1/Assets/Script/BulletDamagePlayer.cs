using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamagePlayer : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemySniper")
        {
            VidaInimigo inimigo = collision.gameObject.GetComponent<VidaInimigo>();

            if(inimigo != null)
            {
                inimigo.TakeDamege(1);
            }

            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "EnemysBullets")
        {
            Destroy(gameObject);
        }
    }
}
