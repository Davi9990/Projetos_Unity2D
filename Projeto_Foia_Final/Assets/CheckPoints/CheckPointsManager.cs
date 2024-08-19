using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioSource;
    public AudioClip efeitoAtivacao;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Jogador"))
        {
            // Rodar a anima��o do checkpoint
            animator.SetTrigger("Activate");
            audioSource.PlayOneShot(efeitoAtivacao, 1.0f);

            // Salvar a posi��o do checkpoint no jogador
            other.GetComponent<HealthSystem>().SetCheckpoint(transform.position);
        }
    }
}
