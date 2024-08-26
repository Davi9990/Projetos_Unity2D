using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimentacao : MonoBehaviour
{
    public float velocidade;
    private Rigidbody2D corpo;
    public float xAxis;
    public float yAxis;


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
    }
}
