using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManeger : MonoBehaviour
{
    public static GameObject PlayerPegador; //Quem é o Pegador
    public List<GameObject> Players = new List<GameObject>();

    public static void TrocarPapeis(GameObject novoPegador)
    {

        //Atualiza o pegador
        if(PlayerPegador != null)
        {
            PlayerPegador.tag = "Player_Fugitivo";
            PlayerPegador.GetComponent<SpriteRenderer>().color = Color.blue;
        }

        //Define o novo Pegador
        PlayerPegador = novoPegador;
        PlayerPegador.tag = "Player_Pegador";
        PlayerPegador.GetComponent <SpriteRenderer>().color = Color.red;
    }
}
