using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JogadorInventario : MonoBehaviour
{
    public int numeroDeChaves = 0; // Número de chaves que o jogador possui

    public void AdicionarChave()
    {
        numeroDeChaves++;
        Debug.Log("Chave coletada! Total de chaves: " + numeroDeChaves);
    }

    public bool TemChave()
    {
        return numeroDeChaves > 0;
    }

    public void UsarChave()
    {
        if (numeroDeChaves > 0)
        {
            numeroDeChaves--;
            Debug.Log("Chave usada! Total de chaves restantes: " + numeroDeChaves);
        }
        else
        {
            Debug.Log("Nenhuma chave para usar!");
        }
    }
}
