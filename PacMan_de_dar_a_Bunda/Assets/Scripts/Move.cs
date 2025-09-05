using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float velocidade = 6;
    private Rigidbody2D rb;
    private Vector2 direction;
    private bool isFacingRight = true;
    private bool isFacingUp = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movimentacao();

        if (direction.x > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (direction.x < 0 && isFacingRight)
        {
            Flip();
        }

        //Flip eixo Y
        if (direction.y > 0 && !isFacingUp)
        {
            FlipY();
        }
        else if (direction.y < 0 && isFacingUp)
        {
            FlipY();
        }
    }

    void Movimentacao()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;


        rb.velocity = direction * velocidade;
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }

    void FlipY()
    {
        isFacingUp = !isFacingUp;
        Vector3 escala = transform.localScale;
        escala.y *= -1;
        transform.localScale = escala;
    }
}
