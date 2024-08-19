using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public enum ItemType { Chave, Vida, Reviver }
    public ItemType tipoDoItem;

    public int quantidadeDeVida = 1; // Quantidade de vida que este item concede ao jogador
    public int quantidadeDeRevives = 1;
    private Animator animator; // Referência ao Animator
    private AudioSource audioSource;
    public AudioClip efeitoAtivacao;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // Encontra o Animator no GameObject com a tag "VidaExtra"
        GameObject vidaExtra = GameObject.FindGameObjectWithTag("VidaExtra");
        if (vidaExtra != null)
        {
            animator = vidaExtra.GetComponent<Animator>();
        }
        else
        {
            Debug.LogWarning("Nenhum objeto com a tag 'VidaExtra' foi encontrado!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Jogador"))
        {
            ColetarItem(other.gameObject);
            audioSource.PlayOneShot(efeitoAtivacao, 1f);
        }
    }

    private void ColetarItem(GameObject jogador)
    {
        switch (tipoDoItem)
        {
            case ItemType.Chave:
                jogador.GetComponent<JogadorInventario>().AdicionarChave();
                Destroy(gameObject); // Destrói o item imediatamente após a coleta
                audioSource.PlayOneShot(efeitoAtivacao, 1f);

                break;

            case ItemType.Vida:
                jogador.GetComponent<HealthSystem>().GanharVida(quantidadeDeVida);
                StartCoroutine(AnimarEDestruir()); // Inicia a animação antes de destruir
                audioSource.PlayOneShot(efeitoAtivacao, 1f);

                break;

            case ItemType.Reviver:
                jogador.GetComponent<HealthSystem>().GanharReviver(quantidadeDeRevives);
                StartCoroutine(AnimarEDestruir()); // Inicia a animação antes de destruir
                audioSource.PlayOneShot(efeitoAtivacao, 1f);

                break;
        }
    }

    private IEnumerator AnimarEDestruir()
    {
        if (animator != null)
        {
            animator.SetTrigger("Desaparecer"); // Dispara a animação de desaparecimento
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        }

        Destroy(gameObject); // Destrói o item após a animação
    }
}
