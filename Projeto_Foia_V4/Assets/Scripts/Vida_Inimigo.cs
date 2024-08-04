using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public int maxHealth = 1000; // Vida m�xima do inimigo
    private int currentHealth;
    private Animator animator;

    void Start()
    {
        currentHealth = maxHealth; // Inicializa a vida atual com a vida m�xima
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Reduz a vida do inimigo
        animator.SetBool("TakingHit", true);
        StartCoroutine(ResetTakingHit()); // Inicia o Coroutine para resetar a anima��o

        // Verifica se a vida do inimigo chegou a zero
        if (currentHealth <= 0)
        {
            Die();
            Debug.Log("Inimigo morreu");
        }
    }

    IEnumerator ResetTakingHit()
    {
        // Aguarda o final da anima��o de TakingHit antes de voltar ao estado Idle
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // Reseta o estado de TakingHit
        animator.SetBool("TakingHit", false);
    }

    void Die()
    {
        // Destr�i o game object do inimigo
        Destroy(gameObject);
    }
}
