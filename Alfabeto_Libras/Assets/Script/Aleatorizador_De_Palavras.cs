using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Aleatorizador_De_Palavras : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public List<string> Palavras = new List<string>
    {
        "Verde",
        "Amarelo",
        "Lilais",
        "Azul",
        "Rosa",
        "Roxo", 
        "Laranja",
        "Branco",
        "Marrom",
        "Preto"
    };
    void Start()
    {
        //Sorteia e exibe uma palavra ao iniciar
        string palavraSorteada = RandomPalavra();
        Text.text = palavraSorteada; //Atualiza o Texto na Tela
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string RandomPalavra()
    {
        int randomIndex =  Random.Range(0, Palavras.Count);
        return Palavras[randomIndex];
    }
}
