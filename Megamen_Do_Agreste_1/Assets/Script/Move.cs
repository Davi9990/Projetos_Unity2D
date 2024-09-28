using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    //Movimentação
    public float speed;
    private Rigidbody2D rg;

    //Pulos
    public float JP;
    private bool CnJmp;
    private float tempinho = 0.5f;

    //Atirar
    public Transform Hand;
    public GameObject Balas;
    public float speedBulllets;
    public float fireRate = 1f;
    public float nextFireTime = 0f;

    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        m();
        pular();

        if (tempinho <= 0)
        {
            CnJmp = false;
            Debug.Log("O pulo brochou");
        }
    }

    void m()
    {
        float n = Input.GetAxisRaw("Horizontal");
        rg.velocity = new Vector2(n * speed, rg.velocity.y);
    }

    void pular()
    {
        if (Input.GetButtonDown("Jump") && CnJmp == true)
        {
            rg.AddForce(new Vector2(0, JP), ForceMode2D.Impulse);
            CnJmp = false;
        }
    }

    private void Atirar()
    {
        if(Input.GetKeyDown(KeyCode.F) && Time.time >= nextFireTime)
        {
            GameObject newFire = Instantiate(Balas,Hand.position, Quaternion.identity);
            //Acessa o RigidBody2d do porjetil instanciado
            Rigidbody2D firerb = newFire.GetComponent<Rigidbody2D>();
            //Acessa uma força ao projetil na direção para a qual a mão está apontando
            firerb.velocity = Hand.right * speed;
            nextFireTime = Time.time + fireRate;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Chao")
        {
            CnJmp = true;
            //tempinho = 0.5f;
        }

    }

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "chao")
    //    {
    //        tempinho -= Time.deltaTime;

    //    }
    //}
}
