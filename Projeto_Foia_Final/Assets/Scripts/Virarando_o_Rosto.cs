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
    private AtaquePlayer ataquePlayer; // Refer�ncia ao script de ataque
    public Transform PontodeAtaque; // Refer�ncia ao ponto de ataque

    private float velocidadeOriginal; // Para armazenar a velocidade original

    void Start()
    {
        objeto = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
        animator = GetComponent<Animator>(); // Obt�m o componente Animator
        ataquePlayer = GetComponent<AtaquePlayer>(); // Obt�m o componente AtaquePlayer
        PontodeAtaque = ataquePlayer.PontodeAtaque; // Obt�m o ponto de ataque

        velocidadeOriginal = velocidade; // Armazena a velocidade original
    }

    private void FixedUpdate()
    {
        if (!ataquePlayer.atacando) // Verifica se n�o est� atacando
        {
            Mover();
        }
    }

    void Mover()
    {
        moveX = Input.GetAxis("Horizontal");
        objeto.velocity = new Vector2(moveX * velocidade, objeto.velocity.y);

        // Ajusta o flip do sprite baseado na dire��o do movimento
        if (moveX > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveX < 0 && facingRight)
        {
            Flip();
        }

        // Ajusta as anima��es
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

        // Inverte a posi��o do ponto de ataque em rela��o ao personagem
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
        yield return new WaitForSeconds(duracao); // Espera pela dura��o especificada
        velocidade = velocidadeOriginal; // Restaura a velocidade original
    }
}
