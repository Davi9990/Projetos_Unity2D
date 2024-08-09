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
            // Rodar a anima��o do checkpoint
            animator.SetTrigger("Activate");

            // Salvar a posi��o do checkpoint no jogador
            other.GetComponent<HealthSystem>().SetCheckpoint(transform.position);
        }
    }
}
