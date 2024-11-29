using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaInimigo : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth;

    public GameObject[] SpawnItens;
    int random;

    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    public void TakeDamege(int damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            SpawnRandom();
            Destroy(gameObject);
        }
    }

    void SpawnRandom()
    {
        // Sorteia um n�mero entre 0 e o tamanho total do array + 1 (incluindo a chance de n�o spawnar nada)
        random = Random.Range(0, SpawnItens.Length + 1);

        // Se o valor sorteado for menor que o tamanho do array, spawna o item correspondente
        if (random < SpawnItens.Length)
        {
            Instantiate(SpawnItens[random], transform.position, transform.rotation);
        }
        // Caso contr�rio, n�o faz nada (chance de n�o spawnar nenhum item)
    }
}
