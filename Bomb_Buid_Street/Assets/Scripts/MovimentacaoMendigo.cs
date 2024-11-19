using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentacaoMendigo : MonoBehaviour
{
    public Transform player;
    public Transform Player_Grande;
    public float followDistance = 10f; //Distancia em que o inimigo come�a a seguir o jogador
    public float moveSpeed = 2f; //Velocidade de movimento do inimigo
    public int damege = 10;
    public float attackCooldown = 1f;
    private float lastAttackTime; //Tempo de recarga entre ataques
    private Rigidbody2D rb;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        if(rb == null)
        {
            Debug.Log("RigidBody2d n�o encontrado no inimigo");
        }
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        float distanceToPlayer2 = Vector2.Distance(transform.position, Player_Grande.position);
       
        // Se a dist�ncia for menor ou igual ao followDistance, o inimigo segue o jogador
        if (distanceToPlayer <= followDistance) 
        {
            //Move o inimigo na dire��o do jogador
            anim.SetBool("Perseguindo", true);
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);

            
        }
        else if(distanceToPlayer2 <= followDistance)
        {
            anim.SetBool("Perseguindo", true);
            Vector2 direction2 = (Player_Grande.position - transform.position).normalized;
            transform.position += (Vector3)(direction2 * moveSpeed * Time.deltaTime);
        }
        else
        {
            anim.SetBool("Perseguindo", false);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && Time.time > lastAttackTime + attackCooldown) 
        {
            SistemaDeVida vida = collision.gameObject.GetComponent<SistemaDeVida>();

            if (vida != null) 
            {
                vida.vida -= damege;
                lastAttackTime = Time.time;
                vida.AtualizarHudDeVida();
            }
        }

        if(collision.gameObject.CompareTag("Player_Grande") && Time.time > lastAttackTime + attackCooldown)
        {
             SistemaDeVida vida = collision.gameObject.GetComponent<SistemaDeVida>();

            if (vida != null) 
            {
                vida.vida -= damege;
                lastAttackTime = Time.time;
                vida.AtualizarHudDeVida();
            }
        }
    }
}
