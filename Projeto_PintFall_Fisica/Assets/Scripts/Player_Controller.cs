using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Controller : MonoBehaviour
{
    private static Player_Controller instance;

    private GameObject pontoDeSpawnAtual;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene cena, LoadSceneMode loadSceneMode)
    {
        StartCoroutine(PosicionarNoPontoDeSpawn());
    }

    private IEnumerator PosicionarNoPontoDeSpawn()
    {
        yield return null;

       if(pontoDeSpawnAtual == null)
       {
            pontoDeSpawnAtual = GameObject.FindGameObjectWithTag("EndPoint2");
       }

       if(pontoDeSpawnAtual != null)
        {
            transform.position = pontoDeSpawnAtual.transform.position;

            foreach(Transform child in transform)
            {
                child.position = pontoDeSpawnAtual.transform.position;
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EndPoint"))
        {
            MudarPontoDeSpawn("EndPoint3");
        }
    }

    private void MudarPontoDeSpawn(string tagPonto)
    {
        pontoDeSpawnAtual = GameObject.FindGameObjectWithTag(tagPonto);

        if(pontoDeSpawnAtual != null)
        {
            transform.position = pontoDeSpawnAtual.transform.position;

            foreach (Transform child in transform)
            {
                child.position = pontoDeSpawnAtual.transform.position;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
