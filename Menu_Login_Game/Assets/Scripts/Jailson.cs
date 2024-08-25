using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jailson : MonoBehaviour
{
    private JailsonMendesCertos gameManeger2;

    public string espacoCorretoTag;


    // Start is called before the first frame update
    void Start()
    {

        gameManeger2 = GameObject.Find("GameManeger").GetComponent<JailsonMendesCertos>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(espacoCorretoTag))
        {
            Debug.Log($"{gameObject.tag} foi colocada no espa�o correto");
            gameManeger2.AtualizarPosicaoOvo(gameObject.tag, true);
        }
        else
        {
            Debug.Log($"{gameObject.tag} foi colocada no espa�o errado!");
            gameManeger2.AtualizarPosicaoOvo(gameObject.tag, false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(espacoCorretoTag))
        {
            Debug.Log($"{gameObject.tag} foi removida do espa�o correto");
            gameManeger2.AtualizarPosicaoOvo(gameObject.tag, false);
        }
    }
}

