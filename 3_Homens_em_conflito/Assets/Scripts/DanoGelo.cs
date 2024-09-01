using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanoGelo : MonoBehaviour
{
    public int damage = 1;
    public float attackCooldown;
    private float lastAttackTime;

    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Otaku") && Time.time > lastAttackTime + attackCooldown)
        {
            Vilao Vida = collision.gameObject.GetComponent<Vilao>();

            if (Vida != null)
            {
                Vida.ReceberDano(damage);
                lastAttackTime = Time.time;//Atualiza o tempo do ultimo atque para o tempo atual
                Destroy(gameObject);
            }
        }
    }
}
