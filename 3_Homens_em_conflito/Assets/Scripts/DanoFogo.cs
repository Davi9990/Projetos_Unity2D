using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanoFogo : MonoBehaviour
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
        if(collision.gameObject.CompareTag("Scorpion") && Time.time > lastAttackTime + attackCooldown) 
        {
            Vilao vilao = collision.gameObject.GetComponent<Vilao>();

            if(vilao != null)
            {
                vilao.ReceberDano(damage);
                lastAttackTime = Time.time;//Atualiza o tempo do ultimo ataque para o tempo atual
                Destroy(gameObject);
            }
        }
    }
}
