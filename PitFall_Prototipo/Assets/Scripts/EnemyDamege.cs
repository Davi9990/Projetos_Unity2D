using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamege : MonoBehaviour
{
    public SistemaDeVida vida;
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

    }
}
