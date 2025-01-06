using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion_Move_Set : MonoBehaviour
{
    public GameObject Player; // Refer�ncia ao Player
    public float distanciaParaAcao = 30f; // Dist�ncia m�xima para perseguir o Player
    public float velocidade = 5f; // Velocidade de movimento
    private bool olhandoParaDireita = true; // Para verificar a dire��o do Minion
    private Rigidbody2D rb;

    void Start()
    {
        // Encontrar o Player pela tag
        Player = GameObject.FindGameObjectWithTag("Player");

        if (Player == null)
        {
            Debug.LogError("Player n�o encontrado! Certifique-se de que ele tem a tag 'Player'.");
        }

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Player != null)
        {
            PerseguirJogador();
            AjustarVirada();
        }
    }

    private void PerseguirJogador()
    {
        // Calcula a dist�ncia at� o Player
        float distanciaParaJogador = Vector2.Distance(transform.position, Player.transform.position);

        // Se o Player estiver dentro do alcance, perseguir
        if (distanciaParaJogador <= distanciaParaAcao)
        {
            Vector2 direcao = (Player.transform.position - transform.position).normalized;

            // Ignorar o eixo Y (movimento apenas no eixo X)
            direcao.y = 0;

            // Movimenta o inimigo na dire��o do Player
            rb.MovePosition(rb.position + direcao * velocidade * Time.deltaTime);
        }
        else
        {
            // Para de se mover se o Player estiver fora do alcance
            rb.velocity = Vector2.zero;
        }
    }

    private void AjustarVirada()
    {
        // Verifica se precisa virar o Minion para olhar para o Player
        if ((Player.transform.position.x < transform.position.x && olhandoParaDireita) ||
            (Player.transform.position.x > transform.position.x && !olhandoParaDireita))
        {
            olhandoParaDireita = !olhandoParaDireita;

            // Inverte a escala no eixo X para "virar" o sprite
            Vector3 escala = transform.localScale;
            escala.x *= -1;
            transform.localScale = escala;
        }
    }
}
