using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PecaCerta : MonoBehaviour
{
    // Vari�veis para verificar se cada pe�a est� no lugar correto
    private bool peca1NoLugar = false;
    private bool peca2NoLugar = false;
    private bool peca3NoLugar = false;

    // Refer�ncias para as pe�as e os espa�os corretos
    public GameObject peca1;
    public GameObject peca2;
    public GameObject peca3;
    public GameObject espaco1;
    public GameObject espaco2;
    public GameObject espaco3;

    void Update()
    {
        // Verificar se todas as pe�as est�o no lugar correto
        VerificarSePassaDeFase();
    }

    public void AtualizarPosicaoPeca(string pecaTag, bool noLugar)
    {
        // Atualiza o status das pe�as com base na tag
        if (pecaTag == "Peca")
        {
            peca1NoLugar = noLugar;
        }
        else if (pecaTag == "Peca2")
        {
            peca2NoLugar = noLugar;
        }
        else if (pecaTag == "Peca3")
        {
            peca3NoLugar = noLugar;
        }
    }

    private void VerificarSePassaDeFase()
    {
        // Se todas as pe�as estiverem no lugar correto, passa para a pr�xima fase
        if (peca1NoLugar && peca2NoLugar && peca3NoLugar)
        {
            Debug.Log("Todas as pe�as est�o no lugar certo. Passando de fase!");
            SceneManager.LoadScene("Conclus�o");
        }
    }
}
