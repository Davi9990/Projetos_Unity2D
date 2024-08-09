using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [Header("Dash Settings")]
    [SerializeField] private float dashSpeed = 10f; // Velocidade do dash
    [SerializeField] private float dashDuration = 0.2f; // Duração do dash
    [SerializeField] private float dashCooldown = 3f; // Tempo de cooldown do dash
    private bool isDashing = false; // Variável para controlar se o jogador está realizando um dash
    private bool canDash = true; // Variável para controlar se o jogador pode realizar um dash

    [Header("Animator")]
    private Animator animator;
    private bool facingRight = true; // Variável para controlar a direção que o personagem está virado

    private Rigidbody2D rb;
    private Vector2 dashDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Obtém o componente Animator
    }

    void Update()
    {
        GetInputs();
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            PerformDash();
        }
    }

    void GetInputs()
    {
        if (Input.GetKey(KeyCode.LeftShift) && canDash && !isDashing) // Verifica se o jogador está pressionando o botão de dash e não está atualmente dashing
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                dashDirection = Vector2.left;
                if (facingRight)
                {
                    Flip();
                }
                StartCoroutine(DashCoroutine());
                Debug.Log("Dash para a esquerda");
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                dashDirection = Vector2.right;
                if (!facingRight)
                {
                    Flip();
                }
                StartCoroutine(DashCoroutine());
                Debug.Log("Dash para a direita");
            }
        }
    }

    IEnumerator DashCoroutine()
    {
        canDash = false; // Impede o jogador de realizar um novo dash enquanto está dashing
        isDashing = true;
        animator.SetTrigger("Dash");
        animator.SetBool("Running", true); // Ativa a animação de Running

        yield return new WaitForSeconds(dashDuration);

        isDashing = false;
        rb.velocity = Vector2.zero; // Para o movimento após o dash
        animator.SetBool("Running", false); // Desativa a animação de Running

        yield return new WaitForSeconds(dashCooldown); // Aguarda o tempo de cooldown

        canDash = true; // Permite que o jogador possa realizar um novo dash
    }

    void PerformDash()
    {
        rb.velocity = dashDirection * dashSpeed; // Aplica a velocidade do dash na direção atual
        Debug.Log("Performing Dash: " + dashDirection);
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
        Debug.Log("Flipped: " + localScale);
    }
}
