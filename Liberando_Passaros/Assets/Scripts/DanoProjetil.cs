using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanoProjetil : MonoBehaviour
{
    public int damege = 1;
    public float attackCooldown;
    private float lastAttackTime;


    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Gaiola") && Time.time > lastAttackTime + attackCooldown)
        {
            VidaGaiola Vida = collision.gameObject.GetComponent<VidaGaiola>();

            if(Vida != null)
            {
                Vida.ReceberDano(damege); //Aplicar Dano
                lastAttackTime = Time.time;//Atualiza o tempo do ultimo atque para o tempo atual
                Destroy(gameObject);
            }
        }
    }
}
