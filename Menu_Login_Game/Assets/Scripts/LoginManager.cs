using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    public TMP_InputField nomeInput;
    public TMP_InputField senhaInput;
    public TMP_InputField emailInput;
    public Button entrarButton;
    public Button cadastrarButton;

    private List<User> usuarios = new List<User>();
    private string nome, senha, email;

    void Start()
    {
        entrarButton.interactable = false;
        cadastrarButton.onClick.AddListener(GoToCadastro);
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

        bool inputsValidos = nome.Length >= 4 && senha.Length >= 4 && email.Length >= 4;
        entrarButton.interactable = inputsValidos;
    }

    public void Entrar()
    {
        // Checar se o usuário existe na lista de usuários registrados
        foreach (var user in usuarios)
        {
            if (user.Nome == nome && user.Senha == senha && user.Email == email)
            {
                Debug.Log("Login bem-sucedido!");
                SceneManager.LoadScene("ArrastandoOvos");
                return;
            }
        }

        Debug.Log("Login falhou. Verifique suas credenciais.");
        // Exibir mensagem de erro ou feedback para o usuário
    }

    public void GoToCadastro()
    {
        SceneManager.LoadScene("Cadastramento");
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
}
