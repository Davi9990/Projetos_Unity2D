using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogoSimples : MonoBehaviour
{
    [Header("Componentes do di�logo")]
    public GameObject CaixaDeDialogo;
    public TextMeshProUGUI textoUI;

    [Header("Configura��o das falas")]
    [TextArea(2, 5)]
    public string[] falas; // Definido no Inspector
    private int indexFala = 0;

    [Header("Cena ao terminar o di�logo")]
    public string nomeCena;

    void Start()
    {
        // J� ativa a caixa e inicia o di�logo
        if (falas.Length > 0)
        {
            CaixaDeDialogo.SetActive(true);
            indexFala = 0;
            MostrarFalaAtual();
        }
    }

    // Chamado pelo bot�o
    public void ProximoDialogo()
    {
        indexFala++;

        if (indexFala < falas.Length)
        {
            MostrarFalaAtual();
        }
        else
        {
            // Quando acaba as falas -> troca de cena
            if (!string.IsNullOrEmpty(nomeCena))
            {
                SceneManager.LoadScene(nomeCena);
            }
            else
            {
                Debug.LogWarning("Nenhuma cena definida no Inspector!");
            }
        }
    }

    private void MostrarFalaAtual()
    {
        if (indexFala < falas.Length)
        {
            textoUI.text = falas[indexFala];
        }
    }
}
