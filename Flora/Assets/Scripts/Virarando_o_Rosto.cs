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
    public Transform PontodeAtaque; // Referência ao ponto de ataque

    private float velocidadeOriginal; // Para armazenar a velocidade original

    void Start()
    {
        objeto = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
        animator = GetComponent<Animator>(); // Obtém o componente Animator
        ataquePlayer = GetComponent<AtaquePlayer>(); // Obtém o componente AtaquePlayer
        PontodeAtaque = ataquePlayer.PontodeAtaque; // Obtém o ponto de ataque

        velocidadeOriginal = velocidade; // Armazena a velocidade original
    }

    private void FixedUpdate()
    {
        if (!ataquePlayer.atacando) // Verifica se não está atacando
        {
            Mover();
        }
    }

    void Mover()
    {
        moveX = Input.GetAxis("Horizontal");
        objeto.velocity = new Vector2(moveX * velocidade, objeto.velocity.y);

        // Ajusta o flip do sprite baseado na direção do movimento
        if (moveX > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveX < 0 && facingRight)
        {
            Flip();
        }

        // Ajusta as animações
        if (Mathf.Abs(moveX) > 0)
        {
            animator.SetBool("Walking", true);
            animator.SetBool("Idle", false);
        }
        else
        {
            animator.SetBool("Walking", false);
            animator.SetBool("Idle", true);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        spriteRenderer.flipX = !spriteRenderer.flipX;

        // Inverte a posição do ponto de ataque em relação ao personagem
        Vector3 PontodeAtaqueLocalPosition = PontodeAtaque.localPosition;
        PontodeAtaqueLocalPosition.x *= -1;
        PontodeAtaque.localPosition = PontodeAtaqueLocalPosition;
    }

    public void ReduzirVelocidade(float fator, float duracao)
    {
        StartCoroutine(ReduzirVelocidadeCoroutine(fator, duracao));
    }

    private IEnumerator ReduzirVelocidadeCoroutine(float fator, float duracao)
    {
        velocidade *= fator; // Reduz a velocidade pelo fator fornecido
        yield return new WaitForSeconds(duracao); // Espera pela duração especificada
        velocidade = velocidadeOriginal; // Restaura a velocidade original
    }
}
