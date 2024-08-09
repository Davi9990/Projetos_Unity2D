using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulos : MonoBehaviour
{
    public Rigidbody2D Player;
    public bool chao1;
    public Transform detectarchao;
    public LayerMask chao0;
    public int pulos = 2;
    public Animator animator;
    public float jumpForce;

    void Start()
    {
        Player = GetComponent<Rigidbody2D>();
        Player.gravityScale = 2f;
    }

    void Update()
    {
        chao1 = Physics2D.OverlapCircle(detectarchao.position, 0.1f, chao0);
        float moveX = Input.GetAxis("Horizontal");

        if (chao1)
        {
            animator.SetBool("Jumping", false);
            animator.SetBool("Idle", moveX == 0);
            animator.SetBool("Falling", false);
            animator.SetBool("Walking", moveX != 0);
            pulos = 2; // Defina o número de pulos permitidos quando no chão
        }
        else
        {
            if (Player.velocity.y < 0)
            {
                animator.SetBool("Falling", true);
            }
            else
            {
                animator.SetBool("Falling", false);
                animator.SetBool("Jumping", true);
            }

            animator.SetBool("Idle", false);
            animator.SetBool("Walking", false);
        }

        if (Input.GetButtonDown("Jump") && pulos > 0)
        {
            Player.velocity = Vector2.up * jumpForce;
            animator.SetTrigger("Jump"); // Ativa a animação de pulo
            pulos--;
            if (pulos == 0)
            {
                animator.SetBool("Walking", false);
                animator.SetBool("Jumping", true);
            }
        }

        // Aplicar gravidade adicional quando caindo
        if (!chao1 && Player.velocity.y < 0)
        {
            Player.velocity += Vector2.up * Physics2D.gravity.y * (1.5f - 1) * Time.deltaTime;
        }
    }
}
