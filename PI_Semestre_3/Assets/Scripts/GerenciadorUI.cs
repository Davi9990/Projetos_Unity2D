using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GerenciadorUI : MonoBehaviour
{
    public static GerenciadorUI instancia;

    public Text textoVidas;
    public Text textoMensagem;
    public Text textoObjetivo;

    void Awake()
    {
        instancia = this;
    }

    public void AtualizarVidas(int v)
    {
        if(textoVidas)
            textoVidas.text = "Vidas: " + v;
    }

    public void MostrarMensagem(string msg)
    {
        if (!textoMensagem) return;

        textoMensagem.text = msg;
        CancelInvoke(nameof(LimparMensagem));
        Invoke(nameof(LimparMensagem), 2f);
    }

    public void AtualizarObjetivo(string objetivo)
    {
        if(textoObjetivo)
            textoObjetivo.text = "" + objetivo;
    }

    private void LimparMensagem()
    {
        if(textoMensagem)
            textoMensagem.text = "";
    }
}
