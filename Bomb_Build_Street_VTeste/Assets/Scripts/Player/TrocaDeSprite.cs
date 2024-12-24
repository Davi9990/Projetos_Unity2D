using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrocaDeSprite : MonoBehaviour
{
    //Trocando Prefabs
    public GameObject Osvaldo_Normal;
    public GameObject Osvaldo_Grande;
    public GameObject Osvaldo_Giga;

    private ScoreManeger valor;
    void Start()
    {
        Osvaldo_Normal.gameObject.SetActive(true);
        Osvaldo_Grande.gameObject.SetActive(false);
        Osvaldo_Giga.gameObject.SetActive(false);

        valor = GetComponent<ScoreManeger>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (valor.score)
        {
            case 4000: //BOTAR NO N�VEL 2
                Osvaldo_Normal.gameObject.SetActive(false);
                Osvaldo_Grande.gameObject.SetActive(true);
                break;

            case 16000: //BOTAR NO N�VEL 3
                Osvaldo_Grande.gameObject.SetActive(false);
                Osvaldo_Giga.gameObject.SetActive(true);
                break;

            case 40000: SceneManager.LoadScene("Vitoria"); break;
        }
    }
}
