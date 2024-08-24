using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PecaCerta : MonoBehaviour
{
    // Variáveis para verificar se cada peça está no lugar correto
    private bool peca1NoLugar = false;
    private bool peca2NoLugar = false;
    private bool peca3NoLugar = false;

    // Referências para as peças e os espaços corretos
    public GameObject peca1;
    public GameObject peca2;
    public GameObject peca3;
    public GameObject espaco1;
    public GameObject espaco2;
    public GameObject espaco3;

    void Update()
    {
        // Verificar se todas as peças estão no lugar correto
        VerificarSePassaDeFase();
    }

    public void AtualizarPosicaoPeca(string pecaTag, bool noLugar)
    {
        // Atualiza o status das peças com base na tag
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
        // Se todas as peças estiverem no lugar correto, passa para a próxima fase
        if (peca1NoLugar && peca2NoLugar && peca3NoLugar)
        {
            Debug.Log("Todas as peças estão no lugar certo. Passando de fase!");
            SceneManager.LoadScene("Conclusão");
        }
    }
}
