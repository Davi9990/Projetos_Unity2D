using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour
{
    public int damage = 10;         // Dano que o inimigo causa ao jogador
    public float attackCooldown = 1f; // Tempo de recarga entre ataques
    private float lastAttackTime;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //if (rb == null)
        //{
        //    Debug.LogError("Rigidbody2D não encontrado no inimigo!");
        //}
    }

    void Update()
    {


    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && Time.time > lastAttackTime + attackCooldown)
        {
            Barra_de_Vida Vida = collision.gameObject.GetComponent<Barra_de_Vida>();
            if (Vida != null)
            {
                Vida.TakeDamage(damage);
                lastAttackTime = Time.time;
            }
        }
    }
}
