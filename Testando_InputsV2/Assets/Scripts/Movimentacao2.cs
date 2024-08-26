using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimentacao2 : MonoBehaviour
{
    public float velocidade;
    private Rigidbody2D corpo;
    public float xAxis;
    public float yAxis;
    public float jumpforce;

    void Start()
    {
        corpo = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        GetInputs();
        Jump();
    }

    void GetInputs()
    {
        xAxis = Input.GetAxisRaw("Vertical");
        yAxis = Input.GetAxisRaw("Horizontal");
    }

    private void Move()
    {
        /*
        Vector3 movement = new Vector3(Input.GetAxis("Vertical"), 0f, 0f);
        transform.position += movement * Time.deltaTime * velocidade;
        */

        
        corpo.velocity = new Vector2 (velocidade * xAxis,corpo.velocity.y);
        corpo.velocity = new Vector2(velocidade * yAxis, corpo.velocity.x);
        
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            corpo.velocity = Vector2.up * jumpforce;
        }
    }
}
