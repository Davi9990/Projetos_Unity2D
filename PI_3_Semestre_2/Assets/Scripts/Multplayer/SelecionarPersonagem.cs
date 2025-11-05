using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.Collections.Generic;

public class SelecionarPersonagem : MonoBehaviour
{
    [Header("Referências de Interface")]
    public Transform painelPersonagens, spawnVisualizacao;
    public GameObject prefabBotaoPersonagem, prefabMasculino, prefabFeminino, painelConfirmacao;
    public Text textoInfoSelecionado;
    public Button botaoJogar, botaoCriar, botaoSim, botaoNao;
    public Image imagemPersonagem;

    private GameObject modeloAtual;
    private ListaDePersonagens lista;
    private DadosPersonagem personagemSelecionado;
    private int indiceSelecionado = -1, indiceParaDeletar = -1;
    private string chave;

    void Start()
    {
        string usuario = PlayerPrefs.GetString("usuarioAtual");
        chave = usuario + "_personagens";

        // Carrega a lista de personagens salvos, se houver
        if (PlayerPrefs.HasKey(chave))
        {
            string json = PlayerPrefs.GetString(chave);
            lista = JsonUtility.FromJson<ListaDePersonagens>(json);
        }
        else
        {
            lista = new ListaDePersonagens { personagens = new List<DadosPersonagem>() };
            textoInfoSelecionado.text = "Nenhum personagem salvo.";
        }

        AtualizarListaUI();

        botaoJogar.onClick.AddListener(Jogar);
        botaoCriar.onClick.AddListener(IrParaCriarPersonagem);

        painelConfirmacao.SetActive(false);
        botaoSim.onClick.AddListener(ConfirmarDelecao);
        botaoNao.onClick.AddListener(CancelarDelecao);
    }

    void AtualizarListaUI()
    {
        foreach (Transform filho in painelPersonagens)
            Destroy(filho.gameObject);

        for (int i = 0; i < lista.personagens.Count; i++)
        {
            int index = i;
            var personagem = lista.personagens[i];
            GameObject botao = Instantiate(prefabBotaoPersonagem, painelPersonagens);

            botao.transform.Find("TextoNome").GetComponent<Text>().text = personagem.nome;

            // Botão de Selecionar
            botao.GetComponent<Button>().onClick.AddListener(() => Selecionar(index));

            // Botão de Deletar
            Button btnDeletar = botao.transform.Find("BotaoDeletar").GetComponent<Button>();
            btnDeletar.onClick.AddListener(() => MostrarPainelConfirmacao(index));
        }
    }

    void Selecionar(int index)
    {
        indiceSelecionado = index;
        personagemSelecionado = lista.personagens[index];

        textoInfoSelecionado.text =
            $"Nome: {personagemSelecionado.nome}\n" +
            $"Sexo: {personagemSelecionado.sexo}";

        PlayerPrefs.SetString("personagemSelecionado", JsonUtility.ToJson(personagemSelecionado));

        MostrarModelo3D(personagemSelecionado.sexo);
    }

    void MostrarModelo3D(string sexo)
    {
        if (modeloAtual != null)
            Destroy(modeloAtual);

        GameObject prefab = null;
        if (sexo.ToLower() == "masculino")
            prefab = prefabMasculino;
        else if (sexo.ToLower() == "feminino")
            prefab = prefabFeminino;

        if (prefab != null && spawnVisualizacao != null)
        {
            modeloAtual = Instantiate(prefab, spawnVisualizacao.position, spawnVisualizacao.rotation);
            modeloAtual.transform.localScale = Vector3.one * 1.2f; // Ajuste para visualização mobile
        }
    }

    void MostrarPainelConfirmacao(int index)
    {
        indiceParaDeletar = index;
        painelConfirmacao.SetActive(true);
    }

    void ConfirmarDelecao()
    {
        if (indiceParaDeletar >= 0 && indiceParaDeletar < lista.personagens.Count)
        {
            lista.personagens.RemoveAt(indiceParaDeletar);
            string novoJson = JsonUtility.ToJson(lista);
            PlayerPrefs.SetString(chave, novoJson);
            PlayerPrefs.Save();

            indiceSelecionado = -1;
            textoInfoSelecionado.text = "";
            if (modeloAtual != null) Destroy(modeloAtual);

            AtualizarListaUI();
        }

        indiceParaDeletar = -1;
        painelConfirmacao.SetActive(false);
    }

    void CancelarDelecao()
    {
        indiceParaDeletar = -1;
        painelConfirmacao.SetActive(false);
    }

    void IrParaCriarPersonagem()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("CenaCriarPersonagem");
    }

    void Jogar()
    {
        if (indiceSelecionado >= 0)
        {
            PhotonNetwork.LoadLevel("CenaRede");
        }
        else
        {
            textoInfoSelecionado.text = "Selecione um personagem primeiro!";
        }
    }
}
