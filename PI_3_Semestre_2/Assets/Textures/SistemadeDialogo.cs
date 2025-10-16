using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class LinhaDialogo
{
    public string nomeNPC;
    [TextArea(2, 5)]
    public string texto;
}

public class SistemadeDialogo : MonoBehaviour
{
    [Header("Refer�ncias UI")]
    public GameObject painelDialogo;    
    public Text nomeNPCText;            
    public Text textoDialogoText;       
    public Button botaoAvancar;        

    [Header("Configura��o de Di�logo")]
    public List<LinhaDialogo> linhas;   // Lista de falas do NPC

    private int indiceAtual = 0;
    private bool dialogoAtivo = false;

    public Action aoFinalizarDialogo;    // Evento disparado quando o di�logo termina

    void Start()
    {
        painelDialogo.SetActive(false);
        if (botaoAvancar != null)
            botaoAvancar.onClick.AddListener(AvancarDialogo);
    }

    // Inicia o di�logo
    public void IniciarDialogo()
    {
        if (linhas == null || linhas.Count == 0) return;

        indiceAtual = 0;
        dialogoAtivo = true;
        painelDialogo.SetActive(true);
        MostrarLinhaAtual();
    }

    // Mostra a fala atual na tela
    void MostrarLinhaAtual()
    {
        if (indiceAtual < linhas.Count)
        {
            var linha = linhas[indiceAtual];
            nomeNPCText.text = linha.nomeNPC;
            textoDialogoText.text = linha.texto;
        }
    }

    // Avan�a para a pr�xima fala ou encerra o di�logo
    void AvancarDialogo()
    {
        if (!dialogoAtivo) return;

        indiceAtual++;

        if (indiceAtual < linhas.Count)
        {
            MostrarLinhaAtual();
        }
        else
        {
            EncerrarDialogo();
        }
    }

    // Encerra o di�logo
    void EncerrarDialogo()
    {
        painelDialogo.SetActive(false);
        dialogoAtivo = false;
        aoFinalizarDialogo?.Invoke(); // Chama o callback para o MissoesMobile
    }
}
