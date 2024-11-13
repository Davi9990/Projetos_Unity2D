using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    //Movimentação
    float dirX;
    float dirY;
    public float moveSpeed = 10f;

    //Pulos
    public float JumpForce;
    private bool EstaNoChao = false;
    private Rigidbody2D rb;
    public int Pulos = 2;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxis("Horizontal");
        dirY = Input.GetAxis("Vertical");

        transform.position = new Vector2(transform.position.x + dirX * moveSpeed * Time.deltaTime,
            transform.position.y + dirY * moveSpeed * Time.deltaTime);

        Pular();
    }

    void Pular()
    {
        if (Pulos > 0)
        {
            if (Input.GetButtonDown("Jump") && EstaNoChao == true)
            {
                rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
                Pulos --;
                //EstaNoChao = false;
            }
        }
        else
        {
            EstaNoChao = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Chao")
        {
            EstaNoChao = true;
            Pulos = 2;
        }
    }
}
