using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        // Obtém o componente Animator do checkpoint
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o objeto que colidiu é o jogador
        if (other.CompareTag("Player"))
        {
            // Ativa a animação de checkpoint
            animator.SetBool("CheckPointing", true);

          
           
        }
    }
}
