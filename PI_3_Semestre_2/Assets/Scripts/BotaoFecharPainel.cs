using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotaoFecharPainel : MonoBehaviour
{
    // Esse é o botao de fechar do quiz
    public GameObject painel;

    public void Fechar()
    {
        painel.SetActive(false);
    }
}
