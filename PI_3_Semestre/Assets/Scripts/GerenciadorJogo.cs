using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GerenciadorJogo : MonoBehaviour
{
    public static GerenciadorJogo instancia;

    public int vidas = 10;
    public int itensRevolta = 0;

    void Awake()
    {
        instancia = this;
    }

    public void TarefaConcluida()
    {
        Debug.Log("Tarefa Concluída!");
    }

    public void PunirJogador()
    {
        //nao deixe o capataz pegar vc
        vidas--;
        GerenciadorUI.instancia.AtualizarVidas(vidas);

        if (vidas <= 0)
        {
            GerenciadorUI.instancia.MostrarMensagem("Você perdeu todas as vidas!");
        }
    }

    public void ColetarItemRevolta()
    {
        //coletem itens para conseguir ir para fase
        itensRevolta++;
        GerenciadorUI.instancia.MostrarMensagem("Pegou item de revolta (" + itensRevolta + "/3)");

        if (itensRevolta >= 3)
        {
            RevoltaAcontece();
        }
    }

    private void RevoltaAcontece()
    {
        //Aqui vai comecar uma revolta, deem uma ajuda em relação a o que poderia ter, um video, umas imegens.
        GerenciadorUI.instancia.MostrarMensagem("A revolta começou no engenho!");
        SceneManager.LoadScene("CenaRevolta");
    }
}
