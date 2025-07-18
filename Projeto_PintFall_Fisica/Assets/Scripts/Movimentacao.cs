using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimentacao : MonoBehaviour
{
    //Movimenta��o
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
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    //Escadas
    private bool escalando;
    private float vertical;
    public bool escada;

    //Animator
    private Animator anim;

    //Sons
    public AudioClip jumpSound;
    public AudioClip ropeGrabSound;
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hingeJoing = gameObject.GetComponent<HingeJoint2D>();
        hingeJoing.enabled = false;
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Escalada();
        Move();

        if(isSwinging)
        {
            //Se soltar o bot�o de pulo, solta da corda
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
            else if (!EstaNoChao)
            {
                anim.SetBool("Pulando", true);
            }
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius);
        EstaNoChao = false;
        foreach(Collider2D collider in colliders)
        {
            if (collider.CompareTag("chao"))
            {
                EstaNoChao = true;
                anim.SetBool("Pulando", false);
                break;
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
            anim.SetBool("Andando", true);
        }else if(xAxis < 0 && isFacingRight)
        {
            Flip();
            anim.SetBool("Andando", true);
        }
        else if(xAxis == 0)
        {
            anim.SetBool("Andando", false);
        }
    }

    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && EstaNoChao && !escalando)
        {
            rb.velocity = Vector2.up * JumpForce;

            if(jumpSound != null)
            {
                audioSource.PlayOneShot(jumpSound);
            }
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
        rb.velocity = new Vector2(rb.velocity.x, JumpForce * 0.7f); //D� um leve impulso ao soltar do cipo
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

        if(ropeGrabSound != null)
        {
            audioSource.PlayOneShot(ropeGrabSound);
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("chao"))
    //    {
    //        EstaNoChao = true;
    //    }
    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("chao"))
    //    {
    //        EstaNoChao = false;
    //    }
    //}

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Rope") && Input.GetButton("Jump") && !isSwinging)
        {
            GrabRope(other.transform);
            anim.SetBool("Cipando", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Escada"))
        {
            escada = true;
            anim.SetBool("Escalando", true);
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
            anim.SetBool("Escalando", false);
        }

        if (collision.CompareTag("Rope"))
        {
            anim.SetBool("Cipando", false);
        }
    }
}
