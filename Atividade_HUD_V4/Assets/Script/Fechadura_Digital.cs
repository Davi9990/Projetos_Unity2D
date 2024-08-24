using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Fechadura_Digital : MonoBehaviour
{
    public TextMeshProUGUI passwordText; // Alterar para TextMeshProUGUI
    public TextMeshProUGUI messageText; // Alterar para TextMeshProUGUI
    public string correctPassword = "1234"; // Senha correta
    public int maxAttempts = 3; // N�mero m�ximo de tentativas permitidas
    public Button[] digitButtons; // Array para armazenar refer�ncias aos bot�es de d�gitos
    public Button deleteButton; // Refer�ncia ao bot�o de apagar
    public Button verifyButton; // Refer�ncia ao bot�o de verificar

    private string enteredPassword = ""; // Senha que est� sendo digitada
    private int attemptsLeft; // Tentativas restantes

    void Start()
    {
        attemptsLeft = maxAttempts; // Inicializa as tentativas restantes
        ResetPasswordPanel();
    }

    // M�todo para adicionar d�gito � senha digitada
    public void AddDigit(string digit)
    {
        if (enteredPassword.Length < correctPassword.Length)
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
        if (enteredPassword == correctPassword)
        {
            messageText.text = "Senha correta! Acesso concedido.";
            DisableAllButtons();
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
                DisableAllButtons();
            }
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
        EnableAllButtons();
    }

    private void DisableAllButtons()
    {
        foreach (Button button in digitButtons)
        {
            button.interactable = false;
        }
        deleteButton.interactable = false;
        verifyButton.interactable = false;
    }

    private void EnableAllButtons()
    {
        foreach (Button button in digitButtons)
        {
            button.interactable = true;
        }
        deleteButton.interactable = true;
        verifyButton.interactable = true;
    }
}
