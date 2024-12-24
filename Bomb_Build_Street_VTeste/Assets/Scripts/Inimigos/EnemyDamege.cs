using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamege : MonoBehaviour
{
    public SistemaDeVida vida;
    public SistemaDeVida vida2;
    public SistemaDeVida vida3;
    public int damage = 1;
    //public MovimentacaoInimigo inimigo;



    private void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            vida.vidaatual = vida.vida;
            vida.vida -= damage;
            vida.AtualizarHudDeVida();
            //inimigo.TeleportToStart();
        }
        else if(collision.gameObject.tag == "Player_Grande")
        {
            vida2.vidaatual = vida.vida;
            vida2.vida -= damage;
            vida2.AtualizarHudDeVida();
        }
        else if(collision.gameObject.tag == "Player_Giga")
        {
            vida3.vidaatual = vida.vida;
            vida3.vida -= damage;
            vida3.AtualizarHudDeVida();
        }

    }
}
