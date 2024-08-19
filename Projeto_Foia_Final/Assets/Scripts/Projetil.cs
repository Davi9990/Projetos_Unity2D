using System.Collections;
using UnityEngine;

public class Projetil : MonoBehaviour
{
    public float tempoDeVida = 25.0f;
    public float tempoParaAumentarGravidade = 10.0f;
    public float tempoParaPiscar = 20.0f;
    public float duracaoPiscar = 5.0f;
    public float escalaGravidade = 10.0f;
    public Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    public int vida = 1; // Vida do projétil

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(GerenciarTempoDeVida());
    }

    void OnCollisionEnter2D(Collision2D colisao)
    {
        if (colisao.gameObject.CompareTag("Jogador"))
        {
            colisao.gameObject.GetComponent<HealthSystem>().ReceberDano(1); // Aplica dano ao jogador
            ReceberDano(1); // Destroi o projétil ao colidir com o jogador
        }
    }

    public void ReceberDano(int dano)
    {
        vida -= dano;
        if (vida <= 0)
        {
            Destroy(gameObject); // Destroi o projétil se sua vida chegar a 0
        }
    }

    IEnumerator GerenciarTempoDeVida()
    {
        // Espera 10 segundos para aumentar a gravidade
        yield return new WaitForSeconds(tempoParaAumentarGravidade);
        rb.gravityScale = escalaGravidade;

        // Espera mais 10 segundos para iniciar a piscação
        yield return new WaitForSeconds(tempoParaPiscar - tempoParaAumentarGravidade);
        StartCoroutine(Piscar());

        // Espera mais 5 segundos para destruir o projétil
        yield return new WaitForSeconds(duracaoPiscar);
        Destroy(gameObject);
    }

    IEnumerator Piscar()
    {
        float intervaloPiscar = 0.1f;
        for (float t = 0; t < duracaoPiscar; t += intervaloPiscar)
        {
            spriteRenderer.color = spriteRenderer.color == Color.white ? Color.red : Color.white;
            yield return new WaitForSeconds(intervaloPiscar);
        }
    }
}
