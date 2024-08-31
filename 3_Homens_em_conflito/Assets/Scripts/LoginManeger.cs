using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginManeger : MonoBehaviour
{
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;

    public Button entrarButton;

    private List<User> usuarios = new List<User>();
    private string senha, email;

    void Start()
    {
        entrarButton.interactable = false;
        emailInput.onValueChanged.AddListener(delegate { ValidateInputs(); });
        passwordInput.onValueChanged.AddListener(delegate { ValidateInputs(); });

        LoadUsers();
    }

    void ValidateInputs()
    {
        email = emailInput.text;
        senha = passwordInput.text;

        bool inputsValidos = email.Length >= 4 && senha.Length >= 4;
        entrarButton.interactable = inputsValidos; 
    }

    public void Entrar()
    {
        // Checar se o usuário existe na lista de usuários registrados
        foreach (var user in usuarios)
        {
            if (user.Email == "RobertoSantos@gmail.com" && user.Senha == "40028922") // Comparando com as entradas do usuário
            {
                Debug.Log("Login Bem-Sucedido!");
                SceneManager.LoadScene("Game");
                return;
            }
        }
        Debug.Log("Login Falhou. Verifique suas credenciais");
        // Exibir mensagem de erro ou feedback para o usuário
    }

    private void LoadUsers()
    {
        int userCount = PlayerPrefs.GetInt("UserCount", 0);

        for (int i = 0; i < userCount; i++)
        {
            string email = PlayerPrefs.GetString($"User_{i}_Email", "");
            string senha = PlayerPrefs.GetString($"User_{i}_Senha", "");
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(senha))
            {
                usuarios.Add(new User(email, senha));
            }
        }

        // Adicionar um usuário padrão fixo para login
        usuarios.Add(new User("RobertoSantos@gmail.com", "40028922"));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
