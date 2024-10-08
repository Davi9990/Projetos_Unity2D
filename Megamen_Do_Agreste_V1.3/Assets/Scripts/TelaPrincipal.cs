using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TelaPrincipal : MonoBehaviour
{
    public GameObject Mensagem;

    float tempo;
    void Start()
    {
        tempo = 0;
    }

    // Update is called once per frame
    void Update()
    {
        tempo += Time.deltaTime;

        

        if(tempo > 1)
        {
            Mensagem.SetActive(false);
        }
        else
        {
            Mensagem.SetActive(true);
        }

        if(tempo > 2)
        {
            tempo = 0;
        }

        if (Input.touchCount > 0)
        {
            //Lembrar de criar um script especifico para telas aleatórias de gameplay e fazer ela ser chamada tanto aqui quanto no fim de cada tela
            SceneManager.LoadScene("Tela1");
        }


    }
}
