using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TelaPrincipal : MonoBehaviour
{
    public GameObject Mensagem;

    [SerializeField] TextMeshProUGUI Principal;
    public GameObject TelaDeFundo;

    public Color c1;
    public Color c2;
    public Color c3;
    public Color c4;
    public Color c5;

    int cor;

    int ciclo = 0;
    void Start()
    {
        StartCoroutine(pisca());
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.touchCount > 0)
        {
            Touch toque = Input.GetTouch(0);
            StartCoroutine(transicao());
        }


    }

    IEnumerator pisca()
    {

        yield return new WaitForSecondsRealtime(1f);

        Mensagem.SetActive(false);

        
        yield return new WaitForSecondsRealtime(1f);
        Mensagem.SetActive(true);
        


        StartCoroutine(pisca());
    }

    IEnumerator transicao()
    {
        ciclo++;
        cor = Random.Range(1,5);

        switch (cor)
        {
            case 1:
                Principal.color = c1;
                break;

            case 2:
                Principal.color = c2;
                break;

            case 3:
                Principal.color = c3;
                break;

            case 4:
                Principal.color = c4;
                break;

            case 5:
                Principal.color = c5;
                break;

        }

        

        yield return new WaitForSecondsRealtime(3f);
        

        if(ciclo >= 10)
        {
            //Lembrar de criar um script especifico para telas aleatórias de gameplay e fazer ela ser chamada tanto aqui quanto no fim de cada tela
            SceneManager.LoadScene("Tela1");
        }

        

        StartCoroutine(transicao());
    }
}
