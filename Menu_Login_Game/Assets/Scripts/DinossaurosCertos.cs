using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DinossaurosCertos : MonoBehaviour
{
    //Variaveis para verificar se cada peça está no lugar correto
    public bool Dino1NoLugar = false;
    public bool Dino2NoLugar = false;
    

    //Referencias para as peças e os espaços corretos
    public GameObject Dino1;
    public GameObject Dino2;
    public GameObject espaco1;
    public GameObject espaco2;

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
            Dino1NoLugar = nolugar;
        }
        else if (ovotag == "Ovo2")
        {
            Dino2NoLugar = nolugar;
        }
       
    }

    private void VerificarSePassaDeFase()
    {
        //Se todas as peças estiverem no lugar correto, passa para a próxima fase
        if (Dino1NoLugar && Dino2NoLugar)
        {
            Debug.Log("Todas as peças estão no lugar certo. Passando de fase!");
            SceneManager.LoadScene("QuebraCabeça");
        }
    }
}
