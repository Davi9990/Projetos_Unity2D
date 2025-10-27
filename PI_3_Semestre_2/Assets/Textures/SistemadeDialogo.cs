using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class LinhaDialogo
{
    public Sprite fotoDoNPC;
    public string nomeNPC;
    [TextArea(2, 5)]
    public string texto;     
}

public class SistemadeDialogo : MonoBehaviour
{
    [Header("Referencias UI")]
    public GameObject painelDialogo;    
    public Image retratoNPC;
    public Text nomeNPCText;            
    public Text textoDialogoText;       
    public Button botaoAvancar;  

    [Header("Configuracao de Dialogo")]
    public List<LinhaDialogo> linhas;   // Lista de falas do NPC Janio,Quintiliano e NegoDan
    private int indiceAtual = 0;
    private bool dialogoAtivo = false;
    public Action aoFinalizarDialogo;    
   
    void Start()
    {
        painelDialogo.SetActive(false);
        if (botaoAvancar != null)
        {
            botaoAvancar.onClick.AddListener(AvancarDialogo);     
        }                   
    }
    // Inicia o dialogo
    public void IniciarDialogo()
    {
        if (linhas == null || linhas.Count == 0)
        {
            return;
        } 
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

             // Mostra a imagem do NPC se houver
            if (retratoNPC != null && linha.fotoDoNPC != null)
            {
                retratoNPC.sprite = linha.fotoDoNPC;
                retratoNPC.enabled = true;
            }
            else
            {
                retratoNPC.enabled = false;
            }
        }      
    }
    // Avanca para a proxima fala ou encerra o dialogo
    void AvancarDialogo()
    {
        if (!dialogoAtivo)            
        {
            return;
        }
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
    // Encerra o dialogo
    void EncerrarDialogo()
    {
        painelDialogo.SetActive(false);
        dialogoAtivo = false;
        aoFinalizarDialogo?.Invoke(); // Chama o callback para o MissoesMobile
    }
}