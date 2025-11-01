using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class LinhaDialogo
{
    public Sprite fotoDoNPC;           // Imagem do personagem que está falando
    public string nomeNPC;             // Nome do personagem
    [TextArea(2, 5)]
    public string texto;               // Texto da fala do personagem
}

public class SistemadeDialogo : MonoBehaviour
{
    [Header("Referências UI")]
    public GameObject painelDialogo;   // Painel principal da caixa de diálogo
    public Image retratoNPC;           // Imagem do personagem na UI
    public Text nomeNPCText;           // Texto com o nome do personagem
    public Text textoDialogoText;      // Texto principal do diálogo
    public Button botaoAvancar;        // Botão para avançar o diálogo

    [Header("Configuração de Diálogo")]
    public List<LinhaDialogo> linhas;  // Lista contendo as falas do NPC
    private int indiceAtual = 0;       // Índice da fala atual na lista
    private bool dialogoAtivo = false; // Indica se o diálogo está ativo
    public Action aoFinalizarDialogo;  // Evento chamado ao terminar o diálogo

    [Header("Efeitos Visuais")]
    public float velocidadeDigitacao = 0.03f;   // Tempo entre cada letra (efeito de digitação)
    public float duracaoAnimacaoPainel = 0.3f;  // Duração da animação de expansão/encolhimento do painel

    private Coroutine coroutineDigitacao;       // Guarda a referência da coroutine de digitação

    void Start()
    {
        painelDialogo.SetActive(false); // Painel começa invisível

        // Adiciona o evento de clique ao botão "Avançar"
        if (botaoAvancar != null)
            botaoAvancar.onClick.AddListener(AvancarDialogo);
    }

    // Método chamado para iniciar o diálogo
    public void IniciarDialogo()
    {
        // Se não houver falas, sai do método
        if (linhas == null || linhas.Count == 0)
            return;

        indiceAtual = 0;
        dialogoAtivo = true;
        painelDialogo.SetActive(true);

        // Inicia a animação de abertura do painel
        StartCoroutine(AnimarAberturaPainel());
    }

    // Anima a abertura da caixa de diálogo (expansão do painel)
    IEnumerator AnimarAberturaPainel()
    {
        // Escala inicial (0 = invisível)
        painelDialogo.transform.localScale = Vector3.zero;

        float tempo = 0;
        while (tempo < duracaoAnimacaoPainel)
        {
            tempo += Time.deltaTime;
            float t = Mathf.Clamp01(tempo / duracaoAnimacaoPainel);
            painelDialogo.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, t);
            yield return null;
        }

        painelDialogo.transform.localScale = Vector3.one;

        // Após abrir, mostra a primeira fala
        MostrarLinhaAtual();
    }

    // Mostra a fala atual do diálogo
    void MostrarLinhaAtual()
    {
        if (indiceAtual < linhas.Count)
        {
            var linha = linhas[indiceAtual];

            // Atualiza o nome do NPC
            nomeNPCText.text = linha.nomeNPC;

            // Atualiza o retrato do NPC se houver imagem
            if (retratoNPC != null && linha.fotoDoNPC != null)
            {
                retratoNPC.sprite = linha.fotoDoNPC;
                retratoNPC.enabled = true;
            }
            else
            {
                retratoNPC.enabled = false;
            }

            // Para qualquer digitação anterior antes de iniciar a nova
            if (coroutineDigitacao != null)
                StopCoroutine(coroutineDigitacao);

            // Inicia a digitação da fala atual
            coroutineDigitacao = StartCoroutine(EfeitoDigitacao(linha.texto));
        }
    }

    // Coroutine que faz o texto aparecer letra por letra
    IEnumerator EfeitoDigitacao(string textoCompleto)
    {
        textoDialogoText.text = ""; // Limpa o texto antes de começar

        // Escreve letra por letra com um pequeno intervalo
        foreach (char letra in textoCompleto)
        {
            textoDialogoText.text += letra;
            yield return new WaitForSeconds(velocidadeDigitacao);
        }
    }

    // Avança o diálogo quando o jogador clica no botão
    void AvancarDialogo()
    {
        // Se o diálogo não estiver ativo, não faz nada
        if (!dialogoAtivo)
            return;

        // Se o texto ainda estiver sendo digitado, completa instantaneamente
        if (coroutineDigitacao != null && textoDialogoText.text != linhas[indiceAtual].texto)
        {
            StopCoroutine(coroutineDigitacao);
            textoDialogoText.text = linhas[indiceAtual].texto;
            return;
        }

        // Avança para a próxima fala
        indiceAtual++;

        // Se ainda houver falas, mostra a próxima
        if (indiceAtual < linhas.Count)
            MostrarLinhaAtual();
        else
            // Se chegou ao fim, encerra o diálogo
            StartCoroutine(AnimarFechamentoPainel());
    }

    // Anima o fechamento (encolher) da caixa de diálogo antes de desativar
    IEnumerator AnimarFechamentoPainel()
    {
        float tempo = 0;
        Vector3 escalaInicial = painelDialogo.transform.localScale;

        // Encolhe de 1 até 0 gradualmente
        while (tempo < duracaoAnimacaoPainel)
        {
            tempo += Time.deltaTime;
            float t = Mathf.Clamp01(tempo / duracaoAnimacaoPainel);
            painelDialogo.transform.localScale = Vector3.Lerp(escalaInicial, Vector3.zero, t);
            yield return null;
        }

        painelDialogo.transform.localScale = Vector3.zero;

        // Agora realmente encerra o diálogo
        EncerrarDialogo();
    }

    // Fecha o painel e finaliza o diálogo
    void EncerrarDialogo()
    {
        painelDialogo.SetActive(false);
        dialogoAtivo = false;

        // Dispara evento para outras partes do jogo (ex: completar missão)
        aoFinalizarDialogo?.Invoke();
    }
}