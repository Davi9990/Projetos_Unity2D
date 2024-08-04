using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virarando_o_Rosto : MonoBehaviour
{
    public float velocidade = 3.0f;
    public float jumpforce = 600f;
    public bool facingRight = true;
    public float moveX;
    private Rigidbody2D objeto;
    private Transform transform;
    private Animator animator;
    public SpriteRenderer spriteRenderer;
    private AtaquePlayer ataquePlayer; // Referência ao script de ataque

    void Start()
    {
        objeto = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
        animator = GetComponent<Animator>(); // Obtém o componente Animator
        ataquePlayer = GetComponent<AtaquePlayer>(); // Obtém o componente AtaquePlayer
    }

    private void FixedUpdate()
    {
        if (!ataquePlayer.atacando) // Verifica se não está atacando
        {
            Mover();
        }
        //else
        //{
        //    objeto.velocity = new Vector2(0, objeto.velocity.y); // Para o movimento quando atacando
        //}
    }

    void Mover()
    {
        moveX = Input.GetAxis("Horizontal");
        objeto.velocity = new Vector2(moveX * velocidade, objeto.velocity.y);

        // Ajusta o flip do sprite baseado na direção do movimento
        if (moveX > 0 && !facingRight)
        {
            Flip();
            animator.SetBool("Walking", true);
        }
        else if (moveX < 0 && facingRight)
        {
            Flip();
            animator.SetBool("Walking", true);
        }
        else if (moveX == 0)
        {
            animator.SetBool("Walking", false);
            animator.SetBool("Idle", true);
            animator.SetBool("Jumping", false);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }
}
