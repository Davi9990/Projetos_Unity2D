using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carregador : MonoBehaviour
{
    [Header("Configuração de carregamento")]
    public Transform pontoAtras;          // ponto atrás do jogador
    public int colunas = 3;               // quantas colunas o grid terá
    public float espacamentoX = 0.4f;     // distância entre colunas
    public float espacamentoZ = 0.4f;     // distância entre fileiras

    public List<GameObject> canasCarregadas = new List<GameObject>();

    // adiciona uma cana ao jogador
    public void AdicionarCana(GameObject cana)
    {
        canasCarregadas.Add(cana);

        int indice = canasCarregadas.Count - 1;

        // calcula posição no grid
        int coluna = indice % colunas;          // 0,1,2 → reinicia quando atinge colunas
        int fileira = indice / colunas;         // aumenta a cada nova linha

        Vector3 posLocal = new Vector3(coluna * espacamentoX, 0, -fileira * espacamentoZ);

        // define o pai e posiciona
        cana.transform.SetParent(pontoAtras);
        cana.transform.localPosition = posLocal;
        cana.transform.localRotation = Quaternion.identity;
        cana.SetActive(true);
    }

    // entrega todas as canas no depósito
    public void EntregarCanas()
    {
        foreach(var c  in canasCarregadas)
        {
            Destroy(c);
        }
        canasCarregadas.Clear();

        Debug.Log("Cenas Entregues!");
    }
}
