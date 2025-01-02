using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Controller : MonoBehaviour
{
    private static Player_Controller instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Mant�m o GameObject entre cenas
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject); // Evita m�ltiplas inst�ncias
        }
    }

    private void OnSceneLoaded(Scene cena, LoadSceneMode loadSceneMode)
    {
        // Verifica o ponto de spawn padr�o ap�s a transi��o de cena
        GameObject PosicaoInicial = GameObject.FindGameObjectWithTag("EndPoint4");
        if (PosicaoInicial != null)
        {
            Transform PosicaoInicialTransform = PosicaoInicial.transform;
            Vector3 posicaoInicialJogador = PosicaoInicialTransform.position;
            this.transform.position = posicaoInicialJogador;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se a colis�o foi com um objeto de transi��o
        if (collision.gameObject.CompareTag("EndPoint2"))
        {
            // Muda para o ponto de spawn relacionado a "EndPoint6"
            MudarPontoDeSpawn("EndPoint3");
        }
    }

    private void MudarPontoDeSpawn(string tagPonto)
    {
        // Procura pelo ponto de spawn na pr�xima cena
        GameObject NovoPonto = GameObject.FindGameObjectWithTag(tagPonto);
        if (NovoPonto != null)
        {
            Transform NovoPontoTransform = NovoPonto.transform;
            Vector3 novaPosicaoJogador = NovoPontoTransform.position;
            this.transform.position = novaPosicaoJogador;
        }
    }
}
