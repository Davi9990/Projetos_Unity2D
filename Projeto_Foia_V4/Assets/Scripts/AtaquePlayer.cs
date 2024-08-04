using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaquePlayer : MonoBehaviour
{
    public bool atacando; // Tornar public para acessar no outro script
    public Animator animator;

    public Transform PontodeAtaque;
    public float ataqueRanger = 0.5f;
    public LayerMask EnemyLayers;

    void Start()
    {
        // Inicialização, se necessário
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
        animator.SetBool("Attacking", true);

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(PontodeAtaque.position, ataqueRanger, EnemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy2>().TakeDamage(5); // Aplica dano aos inimigos atingidos
            enemy.GetComponent<Enemy>().ReduceSpeed(); // Reduz a velocidade dos inimigos atingidos
        }

        StartCoroutine(ResetAtaque());
    }

    IEnumerator ResetAtaque()
    {
        // Aguarda o final da animação de ataque antes de voltar ao estado Idle
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + 0.1f);

        // Reseta o estado de ataque
        atacando = false; // Define atacando como false
        animator.SetBool("Attacking", false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(PontodeAtaque.position, ataqueRanger);
    }
}
