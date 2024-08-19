using System.Collections;
using UnityEngine;

public class ArbustoScript : MonoBehaviour
{
    private Animator animator;
    private Vector3 originalScale;
    private bool podeFliperar = true; // Controla se o arbusto ainda pode flipar
    private GameObject jogador; // Armazena o jogador

    void Start()
    {
        animator = GetComponent<Animator>();
        originalScale = transform.localScale; // Guarda o scale original do arbusto
        jogador = GameObject.FindGameObjectWithTag("Jogador"); // Procura o jogador pela tag
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Jogador") && podeFliperar)
        {
            StartCoroutine(AtivarArbusto());
        }
    }

    IEnumerator AtivarArbusto()
    {
        // Aumenta o scale para o tamanho da animação
        transform.localScale = new Vector3(6.9477f, 6.9477f, 6.9477f);

        // Inicia a animação
        animator.SetTrigger("Ativar");

        // Flip na direção do jogador
        if (jogador != null)
        {
            if (jogador.transform.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }

        // Espera 10 segundos
        yield return new WaitForSeconds(10f);

        // Volta o scale ao original e desativa o flip
        transform.localScale = originalScale;

        // Bloqueia o flip até o trigger ser ativado novamente
        podeFliperar = false;

        // Retorna à animação idle
        animator.SetTrigger("Idle");
    }
}
