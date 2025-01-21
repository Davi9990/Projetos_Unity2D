using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaInimigo : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth;

    public GameObject[] SpawnItens;
    int random;

    // Array de tags permitidas para causar dano
    public string[] allowedTags;

    void Start()
    {
        currentHealth = maxHealth;
    }

    // Método para aplicar dano, com verificação da tag
    public void TakeDamage(int damage, GameObject damageSource)
    {
        // Verifica se a tag do objeto que causou o dano está no array de tags permitidas
        foreach (string tag in allowedTags)
        {
            if (damageSource.CompareTag(tag))
            {
                // Aplica o dano se a tag for válida
                currentHealth -= damage;

                if (currentHealth <= 0)
                {
                    SpawnRandom();
                    Destroy(gameObject);
                }

                return; // Sai do método após aplicar o dano
            }
        }

        // Opcional: Mensagem no console caso a tag não seja válida
        Debug.Log($"Dano ignorado. Tag '{damageSource.tag}' não permitida.");
    }

    void SpawnRandom()
    {
        // Sorteia um número entre 0 e o tamanho total do array + 1 (incluindo a chance de não spawnar nada)
        random = Random.Range(0, SpawnItens.Length + 1);

        // Se o valor sorteado for menor que o tamanho do array, spawna o item correspondente
        if (random < SpawnItens.Length)
        {
            Instantiate(SpawnItens[random], transform.position, transform.rotation);
        }
        // Caso contrário, não faz nada (chance de não spawnar nenhum item)
    }
}
