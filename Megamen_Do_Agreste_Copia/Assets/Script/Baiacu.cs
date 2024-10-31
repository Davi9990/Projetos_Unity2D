using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baiacu : MonoBehaviour
{
    public int dano; // Dano causado ao jogador
    public float AreaDeDano; // Raio expandido da �rea de dano
    public float TempoDeRecarga; // Tempo em que o inimigo fica na forma normal
    public float TempoDeExpansao; // Tempo em que o inimigo fica na forma expandida
    private CircleCollider2D circleCollider; // Collider que ser� expandido
    private bool EstaExpandindo = false; // Flag para verificar se est� expandindo
    private float lastAttackTime; // Controla o �ltimo ataque

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        circleCollider = GetComponent<CircleCollider2D>(); // Pega o CircleCollider2D do inimigo
        StartCoroutine(ExpandindoAreaDeDano()); // Inicia o ciclo de expans�o e contra��o
    }

    private void OnTriggerStay2D(Collider2D collision) // Detecta o jogador dentro da �rea
    {
        if (EstaExpandindo && collision.gameObject.CompareTag("Player"))
        {
            SistemaDeVida vida = collision.GetComponent<SistemaDeVida>(); // Pega o sistema de vida do jogador

            if (vida != null && Time.time >= lastAttackTime + 1f) // Causa dano a cada 1 segundo
            {
                vida.vida -= dano;
                lastAttackTime = Time.time; // Atualiza o tempo do �ltimo ataque
            }
        }
    }

    private IEnumerator ExpandindoAreaDeDano()
    {
        while (true) // Loop infinito para alternar entre expans�o e contra��o
        {
            // Expande a �rea de colis�o
            EstaExpandindo = true;
            circleCollider.radius = AreaDeDano;
            anim.SetBool("Choque", true);

            yield return new WaitForSeconds(TempoDeExpansao); // Espera o tempo de expans�o

            // Retorna ao tamanho normal
            EstaExpandindo = false;
            circleCollider.radius = 0.07f;
            anim.SetBool("Choque", false);

            yield return new WaitForSeconds(TempoDeRecarga); // Espera o tempo de recarga
        }
    }
}
