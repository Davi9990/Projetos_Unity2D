using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotaoFecharPainel : MonoBehaviour
{
    [Header("Referência ao painel do quiz")]
    public GameObject painel;

    [Header("Referência ao script principal do quiz")]
    public QuizAntirracismo quizAntirracismo;

    public void Fechar()
    {
        // Quando clicar em "Fechar", anima o encolhimento do painel
        if (quizAntirracismo != null && painel.activeSelf)
        {
            quizAntirracismo.StartCoroutine(quizAntirracismo.AnimarFechamentoPainel());
        }
    }
}
