using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    //public SistemasDeVidas vida;
    public int damage = 1;
    public AudioClip damageSound;
    
    void Start()
    {
       
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SistemasDeVidas vida = collision.gameObject.GetComponent<SistemasDeVidas>();

            if (vida != null)
            {
                vida.vidaatual = vida.vida;
                vida.vida -= damage;
                vida.AtualizarHudDeVida();
            }
            else
            {
                Debug.LogWarning("SistemasDeVidas não encontrado no objeto Player.");
            }

            if(damageSound != null)
            {
                AudioSource.PlayClipAtPoint(damageSound, Camera.main.transform.position,1.0f);
            }
        }
    }
}
