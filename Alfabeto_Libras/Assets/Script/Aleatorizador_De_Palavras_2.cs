using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Aleatorizador_De_Palavras_2 : MonoBehaviour
{
    public TextMeshProUGUI Text; // Referência ao TextMeshPro
    public float TempoEmbaralhando = 4f; // Tempo de "embaralhamento"
    public float TempoEmTela = 5f;

    public List<string> Palavras = new List<string>
    {
        "Verde",
        "Amarelo",
        "Lilás",
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
        // Inicia o efeito de embaralhamento
        StartCoroutine(EmbaralharEExibirPalavra());
    }

    void Update()
    {
        
    }

    // Método para pegar uma palavra aleatória da lista
    public string RandomPalavra()
    {
        int randomIndex = Random.Range(0, Palavras.Count);
        return Palavras[randomIndex];
    }

    // Corrotina para embaralhar palavras antes de exibir a palavra sorteada
    private IEnumerator EmbaralharEExibirPalavra()
    {
        // Sorteia a palavra antes do embaralhamento
        string palavraSorteada = RandomPalavra();
        float tempoRestante = TempoEmbaralhando;

        // Enquanto o tempo de embaralhamento não acabar
        while (tempoRestante > 0)
        {
            // Exibe uma palavra aleatória da lista durante o efeito de embaralhamento
            Text.text = RandomPalavra();

            // Espera o tempo restante (sem intervalo fixo)
            yield return null;  // Nenhum tempo fixo, continua até o tempo acabar

            // Reduz o tempo restante
            tempoRestante -= Time.deltaTime;
        }

        // Após o embaralhamento, exibe a palavra sorteada (e ela não muda mais)
        Text.text = palavraSorteada;

        //Espera o tempo de exibição sorteada(5 segundos) 
        yield return new WaitForSeconds(TempoEmTela);

        //Torna o Text invisivel após o tempo passar
        Text.gameObject.SetActive(false); // Desativa o GameObject que conntém o Text
    }
}
