using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        // Obt�m o componente Animator do checkpoint
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o objeto que colidiu � o jogador
        if (other.CompareTag("Player"))
        {
            // Ativa a anima��o de checkpoint
            animator.SetBool("CheckPointing", true);

          
           
        }
    }
}
