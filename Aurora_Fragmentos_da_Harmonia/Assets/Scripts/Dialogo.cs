using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogo : MonoBehaviour
{
    [Header("Componentes do diálogo")]
    public GameObject Caixa_de_dialogo;
    public Image[] KidsDialogos;
    public TextMeshProUGUI text;

    [Header("Referência ao inventário")]
    public Inventario inventario;

    [Header("Textos de cada diálogo")]
    public string[] textosDialogos; // definir manualmente pelo Inspector

    [Header("Textos Iniciais da Protagonista")]
    public string[] falasIniciais;
    private int indexFala = 0;

    private bool dialogoAberto = false;
    private bool dialogoInicialAtivo = false;
    public GameObject Cristal_Grande;

    void Start()
    {
        // Desativa todos os Kids e a caixa
        foreach (var kid in KidsDialogos)
            kid.enabled = false;

        if (Caixa_de_dialogo == null)
            Caixa_de_dialogo = gameObject;

        Caixa_de_dialogo.SetActive(false);

        Cristal_Grande.SetActive(false);

        IniciarDialogosIniciais();
    }

    //Diálogos Iniciais
    public void IniciarDialogosIniciais()
    {
        Caixa_de_dialogo.SetActive(true);
        dialogoAberto = true;
        dialogoInicialAtivo = true;
        indexFala = 0;

        MostrarFalaAtual();
    }

    public void ProximaDialogo()
    {
        if (dialogoInicialAtivo)
        {

            indexFala++;

            if (indexFala < falasIniciais.Length)
            {
                MostrarFalaAtual();
            }
            else
            {
                FecharDialogo();
                dialogoInicialAtivo = false;
            }
        }
        else
        {
            FecharDialogo();
        }
    }

    private void MostrarFalaAtual()
    {
        if(indexFala < falasIniciais.Length)
        {
            text.text = falasIniciais[indexFala];
        }
    }

    // Abre o diálogo dependendo do Index_Cristais
    public void AbrirDialogoDireto(int indexCristal)
    {
        //if (dialogoAberto) return;

        Caixa_de_dialogo.SetActive(true);

        if (indexCristal != 4 && dialogoAberto)
            return;

        dialogoAberto = true;

        // Desativa todas as imagens
        for (int i = 0; i < KidsDialogos.Length; i++)
            KidsDialogos[i].enabled = false;

        // Aqui você escolhe qual imagem mostrar dependendo do diálogo
        if (indexCristal == 1) KidsDialogos[0].enabled = true;
        else if (indexCristal == 2) KidsDialogos[0].enabled = true;
        else if (indexCristal == 3)
        {
            KidsDialogos[1].enabled = true;
            KidsDialogos[0].enabled = false;
            Cristal_Grande.SetActive(true);
        }
        else if(indexCristal == 4)
        {
            KidsDialogos[1].enabled = true;
        }

        // Define o texto manualmente via Inspector
        if (indexCristal - 1 < textosDialogos.Length)
            text.text = textosDialogos[indexCristal - 1];
        else
            text.text = "Texto não definido.";
    }

    // Chamado pelo botão para fechar
    public void FecharDialogo()
    {
        Caixa_de_dialogo.SetActive(false);

        // Desativa todas as imagens
        for (int i = 0; i < KidsDialogos.Length; i++)
            KidsDialogos[i].enabled = false;

        dialogoAberto = false;
    }
}
