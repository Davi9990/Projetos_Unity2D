using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CriarPersonagem : MonoBehaviour
{
    public InputField inputNome;
    public Dropdown dropdownSexo;
    public Button botaoCriar, botaoSair;
    public Text textoStatus;
    public GameObject prefabMasculino, prefabFeminino;
    public Transform spawnPoint;

    void Start()
    {
        botaoCriar.onClick.AddListener(VerificarOuCriarPersonagem);
        botaoSair.onClick.AddListener(() => SceneManager.LoadScene("CenaSelecionarPersonagem"));
    }
    void VerificarOuCriarPersonagem()
    {
        string usuario = PlayerPrefs.GetString("usuarioAtual");
        string chave = usuario + "_personagens";

        ListaDePersonagens lista;
        // Carrega personagens salvos
        if (PlayerPrefs.HasKey(chave))
        {
            lista = JsonUtility.FromJson<ListaDePersonagens>(PlayerPrefs.GetString(chave));
        }
        else
        {
            lista = new ListaDePersonagens { personagens = new List<DadosPersonagem>() };
        }
        string nomeDigitado = inputNome.text.Trim();
        string sexoSelecionado = dropdownSexo.options[dropdownSexo.value].text;

        // --- Limite de 2 personagens (1 de cada sexo) ---
        if (lista.personagens.Count >= 2)
        {
            textoStatus.text = "Você já criou o máximo de 2 personagens (1 masculino e 1 feminino).";
            SceneManager.LoadScene("CenaSelecionarPersonagem");
            return;
        }
        // --- Verifica se já existe personagem do mesmo sexo ---
        foreach (var p in lista.personagens)
        {
            if (p.sexo.Equals(sexoSelecionado, System.StringComparison.OrdinalIgnoreCase))
            {
                textoStatus.text = "Você já criou um personagem do sexo " + sexoSelecionado + ".";
                return;
            }
        }
        // --- Verifica se o nome está vazio ---
        if (string.IsNullOrEmpty(nomeDigitado))
        {
            textoStatus.text = "O nome do personagem não pode estar vazio.";
            return;
        }
        // --- Verifica se já existe um personagem com esse nome ---
        foreach (var p in lista.personagens)
        {
            if (p.nome.Equals(nomeDigitado, System.StringComparison.OrdinalIgnoreCase))
            {
                textoStatus.text = "Já existe um personagem com esse nome.";
                return;
            }
        }
        // --- Cria e salva o personagem ---
        SalvarPersonagem(lista, chave, nomeDigitado, sexoSelecionado);

        // --- Cria o modelo 3D para visualização ---
        CriarModelo3D(sexoSelecionado);
    }
    void SalvarPersonagem(ListaDePersonagens lista, string chave, string nomeDigitado, string sexoSelecionado)
    {
        // Cria o personagem com os dados básicos
        DadosPersonagem personagem = new DadosPersonagem
        {
            nome = nomeDigitado,
            sexo = sexoSelecionado,
            spriteNome = (sexoSelecionado.ToLower() == "masculino") ? "sprite_masculino" : "sprite_feminino"
        };
        lista.personagens.Add(personagem);

        // Salva no PlayerPrefs
        string json = JsonUtility.ToJson(lista);
        PlayerPrefs.SetString(chave, json);
        PlayerPrefs.Save();

        textoStatus.text = "Personagem criado com sucesso!";
    }
    void CriarModelo3D(string sexoSelecionado)
    {
        GameObject prefab = null;

        if (sexoSelecionado.ToLower() == "masculino" && prefabMasculino != null)
            prefab = prefabMasculino;
        else if (sexoSelecionado.ToLower() == "feminino" && prefabFeminino != null)
            prefab = prefabFeminino;

        if (prefab != null && spawnPoint != null)
        {
            GameObject personagem3D = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
            personagem3D.name = sexoSelecionado + "_Personagem";
        }
    }
}
