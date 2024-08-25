using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CadastroMeneger : MonoBehaviour
{
    public TMP_InputField nomeInput;
    public TMP_InputField senhaInput;
    public TMP_InputField emailInput;
    public Button cadastrarButton;

    private List<User> usuarios = new List<User>();
    private string nome, senha, email;

    void Start()
    {
        cadastrarButton.interactable = false;
        nomeInput.onValueChanged.AddListener(delegate { ValidateInputs(); });
        senhaInput.onValueChanged.AddListener(delegate { ValidateInputs(); });
        emailInput.onValueChanged.AddListener(delegate { ValidateInputs(); });

        LoadUsers();
    }

    void ValidateInputs()
    {
        nome = nomeInput.text;
        senha = senhaInput.text;
        email = emailInput.text;

        bool inputsValidos = nome.Length >= 5 && senha.Length >= 5 && email.Length >= 5;
        cadastrarButton.interactable = inputsValidos;
    }

    public void Cadastrar()
    {
        // Adicionar um novo usuário à lista de usuários registrados
        User novoUsuario = new User(nome, senha, email);
        usuarios.Add(novoUsuario);
        SaveUsers();
        Debug.Log("Usuário cadastrado com sucesso!");

        // Redirecionar para a tela de login
        SceneManager.LoadScene("MenuInicial");
    }

    private void LoadUsers()
    {
        int userCount = PlayerPrefs.GetInt("UserCount", 0);

        for (int i = 0; i < userCount; i++)
        {
            string nome = PlayerPrefs.GetString($"User_{i}_Nome", "");
            string senha = PlayerPrefs.GetString($"User_{i}_Senha", "");
            string email = PlayerPrefs.GetString($"User_{i}_Email", "");
            if (!string.IsNullOrEmpty(nome) && !string.IsNullOrEmpty(senha) && !string.IsNullOrEmpty(email))
            {
                usuarios.Add(new User(nome, senha, email));
            }
        }
    }

    private void SaveUsers()
    {
        int userCount = usuarios.Count;
        PlayerPrefs.SetInt("UserCount", userCount);

        for (int i = 0; i < userCount; i++)
        {
            PlayerPrefs.SetString($"User_{i}_Nome", usuarios[i].Nome);
            PlayerPrefs.SetString($"User_{i}_Senha", usuarios[i].Senha);
            PlayerPrefs.SetString($"User_{i}_Email", usuarios[i].Email);
        }

        PlayerPrefs.Save();
    }
}
