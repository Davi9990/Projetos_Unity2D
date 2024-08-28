using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimentacao : MonoBehaviour
{
    public float velocidade;
    private Rigidbody2D corpo;
    public float xAxis;
    public float yAxis;
    private bool isFacingRight = true;

    void Start()
    {
        corpo = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
        Move();
    }

    void GetInputs()
    {
        xAxis = Input.GetAxisRaw("Vertical");
        yAxis = Input.GetAxisRaw("Horizontal");
    }

    private void Move()
    {
        corpo.velocity = new Vector2(velocidade * xAxis, corpo.velocity.y);
        corpo.velocity = new Vector2(velocidade * yAxis, corpo.velocity.x);

        //Checa a direção do movimento e flipa o personagem se necessario

        if(yAxis > 0 && !isFacingRight)
        {
            Flip();
        }
        else if(yAxis < 00 && isFacingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
