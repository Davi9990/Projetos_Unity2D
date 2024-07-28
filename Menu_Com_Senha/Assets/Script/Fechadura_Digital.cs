using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Fechadura_Digital : MonoBehaviour
{
    public TextMeshProUGUI passwordText; // Alterar para TextMeshProUGUI
    public TextMeshProUGUI messageText; // Alterar para TextMeshProUGUI
    public int maxAttempts = 3; // N�mero m�ximo de tentativas permitidas
    public Button[] digitButtons; // Array para armazenar refer�ncias aos bot�es de d�gitos
    public Button deleteButton; // Refer�ncia ao bot�o de apagar
    public Button verifyButton; // Refer�ncia ao bot�o de verificar

    private string enteredPassword = ""; // Senha que est� sendo digitada
    private int attemptsLeft; // Tentativas restantes
    private Dictionary<string, string> users = new Dictionary<string, string>
    {
        {"user1", "1234"}, // Exemplo de usu�rio e senha
        {"user2", "5678"}, // Adicione mais usu�rios conforme necess�rio
    };

    void Start()
    {
        attemptsLeft = maxAttempts; // Inicializa as tentativas restantes
        ResetPasswordPanel();
    }

    // M�todo para adicionar d�gito � senha digitada
    public void AddDigit(string digit)
    {
        if (enteredPassword.Length < 4) // Limite de 4 d�gitos para a senha
        {
            enteredPassword += digit;
            UpdatePasswordText();
        }
    }

    // M�todo para apagar o �ltimo d�gito digitado
    public void DeleteDigit()
    {
        if (enteredPassword.Length > 0)
        {
            enteredPassword = enteredPassword.Substring(0, enteredPassword.Length - 1);
            UpdatePasswordText();
        }
    }

    // M�todo para verificar a senha
    public void VerifyPassword()
    {
        string username = "user1"; // Nome de usu�rio padr�o ou coleta de input

        if (users.ContainsKey(username))
        {
            if (users[username] == enteredPassword)
            {
                messageText.text = "Senha correta! Acesso concedido.";
                LoadNextScene();
            }
            else
            {
                attemptsLeft--;
                if (attemptsLeft > 0)
                {
                    messageText.text = $"Senha incorreta. Tentativas restantes: {attemptsLeft}.";
                }
                else
                {
                    messageText.text = "N�mero m�ximo de tentativas excedido. Sistema bloqueado.";
                }
            }
        }
        else
        {
            messageText.text = "Usu�rio n�o encontrado.";
        }

        enteredPassword = ""; // Resetar a senha digitada ap�s cada tentativa
        UpdatePasswordText();
    }

    // M�todo para atualizar o texto da senha no painel
    private void UpdatePasswordText()
    {
        passwordText.text = new string('*', enteredPassword.Length); // Mostrar asteriscos em vez dos d�gitos da senha
    }

    // M�todo para redefinir o painel de senha para um novo ciclo
    private void ResetPasswordPanel()
    {
        enteredPassword = "";
        attemptsLeft = maxAttempts;
        UpdatePasswordText();
        messageText.text = "Digite a senha e pressione 'Verificar'.";
    }

    // M�todo para carregar a pr�xima cena
    private void LoadNextScene()
    {
        SceneManager.LoadScene("Menu"); // Substitua "NomeDaProximaCena" pelo nome da cena desejada
    }
}
