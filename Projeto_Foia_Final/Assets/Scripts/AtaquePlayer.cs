using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaquePlayer : MonoBehaviour
{
    public bool atacando; // Tornar public para acessar no outro script
    public Animator animador;
    public Transform boss; // Torna a variável boss opcional

    public Transform PontodeAtaque;
    public float ataqueRanger = 3.2f;
    public LayerMask EnemyLayers;
    public float knockbackForce = 10f; // Força do knockback
    private AudioSource audioSource;
    public AudioClip audioClip;

    void Start()
    {
        // Atribui o componente Animator ao animador
        if (animador == null)
        {
            animador = GetComponent<Animator>();
        }
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Verifica se a tecla "Z" foi pressionada e se não está atacando
        if (Input.GetKeyDown(KeyCode.Z) && !atacando)
        {
            Ataque();
        }
    }

    void Ataque()
    {
        atacando = true; // Define atacando como true
        animador.SetTrigger("Attacking"); // Usa Trigger em vez de SetBool
        audioSource.PlayOneShot(audioClip, 1.0f);
        

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(PontodeAtaque.position, ataqueRanger, EnemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (boss != null && enemy.gameObject == boss.gameObject) // Verifica se boss não é null
            {
                enemy.GetComponent<BossHpManager>().ReceberDano(10);
            }
            else if (enemy.GetComponent<Enemy2>() != null) // Verifica se o inimigo tem o componente Enemy2
            {
                enemy.GetComponent<Enemy2>().ReceberDano(5); // Aplica dano aos inimigos atingidos
            }
            else if (enemy.GetComponent<Projetil>() != null) // Verifica se o inimigo tem o componente Projetil
            {
                enemy.GetComponent<Projetil>().ReceberDano(1); // Aplica dano ao projétil
            }
            ApplyKnockback(enemy); // Aplica knockback aos inimigos atingidos
        }

        StartCoroutine(ResetAtaque());
    }

    void ApplyKnockback(Collider2D enemy)
    {
        Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 knockbackDirection = enemy.transform.position - transform.position;
            knockbackDirection.Normalize();
            rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
        }
    }

    IEnumerator ResetAtaque()
    {
        // Aguarda o final da animação de ataque antes de voltar ao estado Idle
        yield return new WaitForSeconds(animador.GetCurrentAnimatorStateInfo(0).length + 0f);

        // Reseta o estado de ataque
        atacando = false; // Define atacando como false
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(PontodeAtaque.position, ataqueRanger);
    }
}
