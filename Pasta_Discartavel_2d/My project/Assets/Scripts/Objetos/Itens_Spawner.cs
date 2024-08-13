using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itens_Spawner : MonoBehaviour
{
    public GameObject[] items; // Vetor de itens que serão spawnados
    private int currentIndex = 0; // Índice atual do vetor de itens

    public Transform spawnPoint; // Ponto de spawn dos itens (acima do bloco)
    public GameObject block; // Referência ao bloco
    public GameObject newBlockPrefab; // Novo prefab para quando todos os itens forem retirados

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se a colisão foi feita pelo jogador
        if (collision.gameObject.CompareTag("Player"))
        {
            // Verifica se a colisão foi feita na parte de baixo do bloco
            ContactPoint2D contact = collision.contacts[0];
            if (contact.normal.y > 0)
            {
                SpawnItem();
            }
        }
    }

    private void SpawnItem()
    {
        // Verifica se ainda há itens no vetor
        if (currentIndex < items.Length)
        {
            // Spawna o próximo item do vetor
            Instantiate(items[currentIndex], spawnPoint.position, Quaternion.identity);
            currentIndex++; // Incrementa o índice para o próximo item

            // Verifica se foi o último item e já troca o bloco
            if (currentIndex >= items.Length)
            {
                ChangeBlockInstantly(newBlockPrefab);
            }
        }
    }

    private void ChangeBlockInstantly(GameObject newBlockPrefab)
    {
        // Substitui o bloco pelo novo prefab
        Vector3 blockPosition = block.transform.position;
        Quaternion blockRotation = block.transform.rotation;
        Destroy(block);
        Instantiate(newBlockPrefab, blockPosition, blockRotation);
    }
}
