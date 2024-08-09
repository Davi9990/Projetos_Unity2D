using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Jogador"))
        {
            // Rodar a animação do checkpoint
            animator.SetTrigger("Activate");

            // Salvar a posição do checkpoint no jogador
            other.GetComponent<HealthSystem>().SetCheckpoint(transform.position);
        }
    }
}
