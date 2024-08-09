using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [Header("Dash Settings")]
    [SerializeField] private float dashSpeed = 10f; // Velocidade do dash
    [SerializeField] private float dashDuration = 0.2f; // Dura��o do dash
    [SerializeField] private float dashCooldown = 3f; // Tempo de cooldown do dash
    private bool isDashing = false; // Vari�vel para controlar se o jogador est� realizando um dash
    private bool canDash = true; // Vari�vel para controlar se o jogador pode realizar um dash

    [Header("Animator")]
    private Animator animator;
    private bool facingRight = true; // Vari�vel para controlar a dire��o que o personagem est� virado

    private Rigidbody2D rb;
    private Vector2 dashDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Obt�m o componente Animator
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
        if (Input.GetKey(KeyCode.LeftShift) && canDash && !isDashing) // Verifica se o jogador est� pressionando o bot�o de dash e n�o est� atualmente dashing
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
        canDash = false; // Impede o jogador de realizar um novo dash enquanto est� dashing
        isDashing = true;
        animator.SetTrigger("Dash");
        animator.SetBool("Running", true); // Ativa a anima��o de Running

        yield return new WaitForSeconds(dashDuration);

        isDashing = false;
        rb.velocity = Vector2.zero; // Para o movimento ap�s o dash
        animator.SetBool("Running", false); // Desativa a anima��o de Running

        yield return new WaitForSeconds(dashCooldown); // Aguarda o tempo de cooldown

        canDash = true; // Permite que o jogador possa realizar um novo dash
    }

    void PerformDash()
    {
        rb.velocity = dashDirection * dashSpeed; // Aplica a velocidade do dash na dire��o atual
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
