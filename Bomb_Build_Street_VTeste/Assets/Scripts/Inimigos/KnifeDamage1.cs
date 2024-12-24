using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeDamage : MonoBehaviour
{
    public int damage = 1;
    AudioSource jogar;


    void Start()
    {
        jogar = GetComponent<AudioSource>();
        jogar.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Player_Grande")
            || collision.gameObject.CompareTag("Player_Giga"))
        {
            SistemaDeVida vida = collision.gameObject.GetComponent<SistemaDeVida>();

            if (vida != null)
            {
                vida.vida -= damage;
                vida.AtualizarHudDeVida();
            }

            Destroy(gameObject);
        }
    }
}
