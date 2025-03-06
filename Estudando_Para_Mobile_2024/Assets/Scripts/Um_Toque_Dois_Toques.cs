using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Um_Toque_Dois_Toques : MonoBehaviour
{
    // Movimentação
    public float speed = 5f;
    private int direcao = 1;
    private bool isFacingRight = true;

    // Pulos
    public float jumpForce = 6;
    public bool estaNoChao = false;
    private Rigidbody2D rb;

    //Toque duplo
    private float tempoUltimoToque = 0f;
    private float intervaloToque = 0.3f;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                //Checa se o toque é um toque duplo
                if(Time.time - tempoUltimoToque < intervaloToque && estaNoChao)
                {
                    Jump();
                }

                tempoUltimoToque = Time.time;

                if (touch.position.x < Screen.width / 2)
                    direcao = -1; // Esquerda
                else
                    direcao = 1; // Direita
            }

            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                Mover();
            }
        }
    }

    void Mover()
    {
        transform.Translate(new Vector2(direcao * speed * Time.deltaTime, 0));
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        estaNoChao = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chao"))
        {
            estaNoChao = true;
        }
    }
}
