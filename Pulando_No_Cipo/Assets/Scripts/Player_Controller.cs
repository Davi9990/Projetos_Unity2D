using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Controller : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public float swingSpeed = 3f; // Velocidade do balanço na corda
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isSwinging;
    private Transform currentRope;
    private HingeJoint2D hingeJoint;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hingeJoint = gameObject.AddComponent<HingeJoint2D>(); 
        hingeJoint.enabled = false; // Começa desativado
    }

    void Update()
    {
        // Movimento Horizontal no Chão
        float moveInput = Input.GetAxis("Horizontal");
        if (!isSwinging)
        {
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        }
        
        // Agarrar e soltar a corda
        if (isSwinging)
        {
            // Se soltar o botão de pulo, solta da corda
            if (Input.GetButtonUp("Jump"))
            {
                ReleaseRope();
            }
        }
        else
        {
            // Se está no chão e aperta pulo, ele salta
            if (isGrounded && Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void ReleaseRope()
    {
        isSwinging = false;
        hingeJoint.enabled = false;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce * 0.7f); // Dá um leve impulso ao soltar
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Rope"))
        {
            currentRope = other.transform;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // Se está perto da corda e segura o botão de pulo, agarra na corda
        if (other.CompareTag("Rope") && Input.GetButton("Jump") && !isSwinging)
        {
            GrabRope(other.transform);
        }
    }

    private void GrabRope(Transform rope)
    {
        isSwinging = true;
        hingeJoint.enabled = true;
        hingeJoint.connectedBody = rope.GetComponent<Rigidbody2D>();
        hingeJoint.anchor = Vector2.zero; 
        hingeJoint.autoConfigureConnectedAnchor = false;
        hingeJoint.connectedAnchor = Vector2.zero;
        rb.velocity = Vector2.zero; // Para o movimento brusco
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if(collision.collider.CompareTag("Morte"))
        {
            SceneManager.LoadScene("Tela_De_Titulo");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
