using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public enum ItemType { Chave, Vida }
    public ItemType tipoDoItem;

    public int quantidadeDeVida = 1; // Quantidade de vida que este item concede ao jogador
    public Animator animator; // Referência ao Animator

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Jogador"))
        {
            ColetarItem(other.gameObject);
        }
    }

    private void ColetarItem(GameObject jogador)
    {
        switch (tipoDoItem)
        {
            case ItemType.Chave:
                jogador.GetComponent<JogadorInventario>().AdicionarChave();
                Destroy(gameObject); // Destrói o item imediatamente após a coleta
                break;

            case ItemType.Vida:
                jogador.GetComponent<HealthSystem>().GanharVida(quantidadeDeVida);
                StartCoroutine(AnimarEDestruir()); // Inicia a animação antes de destruir
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
