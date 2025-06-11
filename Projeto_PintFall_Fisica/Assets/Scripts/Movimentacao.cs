using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimentacao : MonoBehaviour
{
    //Movimentação
    public float velocidade;
    private Rigidbody2D rb;
    private bool isFacingRight = true;
    public float swingSpeed = 3f;
    private bool isSwinging = false;
    private Transform currentRope;
    private HingeJoint2D hingeJoing;

    //Pulo
    public float JumpForce;
    public bool EstaNoChao;

    //Escadas
    private bool escalando;
    private float vertical;
    public bool escada;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hingeJoing = gameObject.GetComponent<HingeJoint2D>();
        hingeJoing.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Escalada();
        Move();

        if(isSwinging)
        {
            //Se soltar o botão de pulo, solta da corda
            if (Input.GetButtonUp("Jump"))
            {
                ReleaseRope();
            }
        }
        else
        {
            if(EstaNoChao && Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }
    }

    private void FixedUpdate()
    {
        if (escalando)
        {
            rb.gravityScale = 0;
            rb.velocity = new Vector2(rb.velocity.x, vertical * velocidade);
        }
        else
        {
            rb.gravityScale = 1;
        }
    }

    private void Escalada()
    {
        float verticalInput = Input.GetAxisRaw("Vertical");

        if(escada && Mathf.Abs(verticalInput) > 0.1f)
        {
            escalando = true;
            vertical = verticalInput;
        }
        else if(!escada || verticalInput == 0)
        {
            escalando = false;
            vertical = 0;
        }
    }

    private void Move()
    {
        float xAxis = Input.GetAxisRaw("Horizontal");

        if (!isSwinging)
        {
            rb.velocity = new Vector2(xAxis * velocidade, rb.velocity.y);
        }


        if(xAxis > 0 && !isFacingRight)
        {
            Flip();
        }else if(xAxis < 0 && isFacingRight)
        {
            Flip();
        }
    }

    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && EstaNoChao && !escalando)
        {
            rb.velocity = Vector2.up * JumpForce;
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    private void ReleaseRope()
    {
        isSwinging = false;
        hingeJoing.enabled = false;
        rb.velocity = new Vector2(rb.velocity.x, JumpForce * 0.7f); //Dá um leve impulso ao soltar do cipo
    }

    private void GrabRope(Transform rope)
    {
        isSwinging = true;
        hingeJoing.enabled = true;
        hingeJoing.connectedBody = rope.GetComponent<Rigidbody2D>();
        hingeJoing.anchor =  Vector2.zero;
        hingeJoing.autoConfigureConnectedAnchor = false;
        hingeJoing.connectedAnchor = Vector2.zero;
        rb.velocity = Vector2.zero; //Para o movimento brusco
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("chao"))
        {
            EstaNoChao = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("chao"))
        {
            EstaNoChao = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Rope") && Input.GetButton("Jump") && !isSwinging)
        {
            GrabRope(other.transform);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Escada"))
        {
            escada = true;
        }

        if (collision.CompareTag("Rope"))
        {
            currentRope = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Escada"))
        {
            escada = false;
            escalando = false;
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }
}
