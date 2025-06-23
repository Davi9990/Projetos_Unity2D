using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida_Velocidade : MonoBehaviour
{
    private Rigidbody2D rb;
    public float massa = 1f;

    public float velocidade;

    public int vida;
    public int vidaatual;
    public int vidaMaxima;
    private float energiaCinetica;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        velocidade = rb.velocity.magnitude;

        energiaCinetica = 0.5f * massa * Mathf.Pow(velocidade, 2);

        Move();
        VerificarMorte();
    }

    private void Move()
    {
        float xAxis = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(xAxis, rb.velocity.magnitude);
    }

    void VerificarMorte()
    {
        if(vida <= 0)
        {
            Destroy(gameObject);
            Debug.Log("MORREU !!!!!!!!!!!");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            vidaatual = vida;
            //vida -= velocidade;
        }
    }
}
