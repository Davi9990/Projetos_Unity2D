using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [Header("Dash Settings")]
    [SerializeField] private float dashSpeed = 10f; // Velocidade do dash
    private bool isDashing = false; // Variável para controlar se o jogador está realizando um dash

    [Header("Animator")]
    private Animator animator;
    private bool facingRight = true; // Variável para controlar a direção que o personagem está virado

    [Header("Attack Settings")]
    [SerializeField] private float attackDuration = 0.5f;
    private bool isAttacking = false;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Obtém o componente Animator
    }

    void Update()
    {
        GetInputs();
        PerformDash();
    }

    void GetInputs()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing) // Verifica se o jogador pressionou o botão de dash e não está atualmente dashing
        {
            isDashing = true;
            Invoke(nameof(StopDash), 0.2f); // Define uma duração para o dash (0.2 segundos neste exemplo)
            animator.SetTrigger("Dash");
        }

        if (Input.GetKeyDown(KeyCode.F) && !isAttacking) // Verifica se o jogador pressionou o botão de ataque e não está atualmente atacando
        {
            isAttacking = true;
            Invoke(nameof(StopAttack), attackDuration); // Define uma duração para o ataque
            animator.SetTrigger("Attack");
        }
    }

    void PerformDash()
    {
        if (isDashing)
        {
            float dashDirection = facingRight ? 1 : -1;
            rb.velocity = new Vector2(dashSpeed * dashDirection, rb.velocity.y); // Aplica a velocidade do dash na direção atual
        }
    }

    void StopDash()
    {
        isDashing = false;
    }

    void StopAttack()
    {
        isAttacking = false;
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
