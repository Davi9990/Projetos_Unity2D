using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stackjump_pulo : MonoBehaviour
{
    public Rigidbody2D rb;
    public float jumpVelocity = 6.26f;
    private bool isGrounded = true;
    public gamemanager gameManager;

    void Update()
    {
#if UNITY_EDITOR
        // Para testes no PC (pressionando espaço)
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
#endif

        // Para toque na tela (mobile)
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && isGrounded)
        {
            Jump();
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
        isGrounded = false;
        gameManager.OnPlayerLanded(transform.position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("detector"))
        {
            gameManager.GameOver();
        }
        if (collision.gameObject.CompareTag("Caixa"))
        {
            gameManager.OnPlayerLanded(transform.position);
        }
    }
}
