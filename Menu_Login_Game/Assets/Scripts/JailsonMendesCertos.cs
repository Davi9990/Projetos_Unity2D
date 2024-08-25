using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JailsonMendesCertos : MonoBehaviour
{
    //Variaveis para verificar se cada peça está no lugar correto
    public bool ovo1NoLugar = false;
    public bool ovo2NoLugar = false;
    public bool ovo3NoLugar = false;
    public bool ovo4NoLugar = false;

    //Referencias para as peças e os espaços corretos
    public GameObject ovo1;
    public GameObject ovo2;
    public GameObject ovo3;
    public GameObject ovo4;
    public GameObject espaco1;
    public GameObject espaco2;
    public GameObject espaco3;
    public GameObject espaco4;

    // Start is called before the first frame update
    void Update()
    {
        VerificarSePassaDeFase();
    }

    public void AtualizarPosicaoOvo(string ovotag, bool nolugar)
    {
        //Atualiza o status das peças com base na tag
        if (ovotag == "Ovo1")
        {
            ovo1NoLugar = nolugar;
        }
        else if (ovotag == "Ovo2")
        {
            ovo2NoLugar = nolugar;
        }
        else if (ovotag == "Ovo3")
        {
            ovo3NoLugar = nolugar;
        }
        else if(ovotag == "Ovo4")
        {
            ovo4NoLugar= nolugar;
        }
    }

    private void VerificarSePassaDeFase()
    {
        //Se todas as peças estiverem no lugar correto, passa para a próxima fase
        if (ovo1NoLugar && ovo2NoLugar && ovo3NoLugar && ovo4NoLugar)
        {
            Debug.Log("Todas as peças estão no lugar certo. Passando de fase!");
            SceneManager.LoadScene("TelaFinal");
        }
    }
}
