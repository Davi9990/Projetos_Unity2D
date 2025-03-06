using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TelaPrincipal : MonoBehaviour
{
    public GameObject Mensagem;
    public GameObject TelaDeFundo;

    private AudioSource som;



    [SerializeField] TextMeshProUGUI Principal;
    

    public Color c1;
    public Color c2;
    public Color c3;
    public Color c4;
    public Color c5;

    int cor = 0;
    int coratual = 0;

    int ciclo = 0;

    bool Play = true;
    void Start()
    {
        StartCoroutine(pisca());

        som = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.touchCount > 0 && Play == true)
        {
            Touch toque = Input.GetTouch(0);
            if(toque.phase == TouchPhase.Began)
            {
                Play = false;
                som.Play();
                Mensagem.SetActive(false);
                StartCoroutine(transicao());
            }
            
        }


    }

    IEnumerator pisca()
    {

        yield return new WaitForSecondsRealtime(1f);

        Mensagem.SetActive(false);

        
        yield return new WaitForSecondsRealtime(1f);
        Mensagem.SetActive(true);


        if (Play)
        {
            StartCoroutine(pisca());
        }
        else
        {
            Mensagem.SetActive(false);
        }
    }

    IEnumerator transicao()
    {
        ciclo++;
        coratual = cor;
        

        while(cor == coratual)
        {
            cor = Random.Range(1, 5);
        }

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

        

        yield return new WaitForSecondsRealtime(0.2f);
        

        if(ciclo >= 30)
        {
            //Lembrar de criar um script especifico para telas aleatórias de gameplay e fazer ela ser chamada tanto aqui quanto no fim de cada tela
            SceneManager.LoadScene("ConDia");
        }

        

        StartCoroutine(transicao());
    }
}
