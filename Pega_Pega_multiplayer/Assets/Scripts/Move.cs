using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;
using Unity.Netcode;


public class Move : NetworkBehaviour
{
    public float speed; // Velocidade de movimento
    private Rigidbody2D rb; // Componente Rigidbody2D
    private bool isFacingRight = true; // Controle para a dire��o do sprite

    public float jumpForce = 10; // For�a do pulo
    private bool isJumping = false; // Controle para verificar se o jogador pode pular
    private SpriteRenderer sprite; // Componente SpriteRenderer
    private static bool podeTrocar = true; // Controle global para evitar m�ltiplas trocas simult�neas

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Movimentacao();
        Pulo();
    }

    void Movimentacao()
    {
        float direction = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);

        // Verifica se o sprite precisa ser invertido
        if (direction > 0 && !isFacingRight)
        {
            InverterSprite();
        }
        else if (direction < 0 && isFacingRight)
        {
            InverterSprite();
        }
    }

    void Pulo()
    {
        if (Input.GetButtonDown("Jump") && isJumping)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            isJumping = false;
        }

        // Pulo vari�vel
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se o jogador est� tocando o ch�o
        if (collision.gameObject.tag == "Chao")
        {
            isJumping = true;
        }

        // Verifica colis�o com o outro jogador
        if (podeTrocar && (collision.gameObject.CompareTag("Player_Fugitivo") || collision.gameObject.CompareTag("Player_Pegador")))
        {
            // Apenas um jogador ser� respons�vel pela troca
            if (gameObject.CompareTag("Player_Pegador"))
            {
                TrocarPapeis(collision.gameObject);
            }
        }
    }

    private void TrocarPapeis(GameObject outroJogador)
    {
        podeTrocar = false;

        // Troca as tags
        Debug.Log($"Antes da troca: {gameObject.name} � {gameObject.tag}, {outroJogador.name} � {outroJogador.tag}");
        string minhaTag = gameObject.tag;
        gameObject.tag = outroJogador.tag;
        outroJogador.tag = minhaTag;
        Debug.Log($"Depois da troca: {gameObject.name} � {gameObject.tag}, {outroJogador.name} � {outroJogador.tag}");

        // Atualiza as cores
        if (gameObject.tag == "Player_Pegador")
        {
            sprite.color = Color.red;
            outroJogador.GetComponent<SpriteRenderer>().color = Color.blue;
        }
        else
        {
            sprite.color = Color.blue;
            outroJogador.GetComponent<SpriteRenderer>().color = Color.red;
        }

        Debug.Log($"Cor alterada: {gameObject.name} agora � {sprite.color}, {outroJogador.name} agora � {outroJogador.GetComponent<SpriteRenderer>().color}");

        // Aguarda 0.2 segundos para evitar m�ltiplas trocas simult�neas
        StartCoroutine(ResetarTroca());
    }

    private IEnumerator ResetarTroca()
    {
        yield return new WaitForSeconds(0.2f);
        podeTrocar = true;
    }

    private void InverterSprite()
    {
        isFacingRight = !isFacingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
