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
    public int maxAttempts = 3; // Número máximo de tentativas permitidas
    public Button[] digitButtons; // Array para armazenar referências aos botões de dígitos
    public Button deleteButton; // Referência ao botão de apagar
    public Button verifyButton; // Referência ao botão de verificar

    private string enteredPassword = ""; // Senha que está sendo digitada
    private int attemptsLeft; // Tentativas restantes

    void Start()
    {
        attemptsLeft = maxAttempts; // Inicializa as tentativas restantes
        ResetPasswordPanel();
    }

    // Método para adicionar dígito à senha digitada
    public void AddDigit(string digit)
    {
        if (enteredPassword.Length < correctPassword.Length)
        {
            enteredPassword += digit;
            UpdatePasswordText();
        }
    }

    // Método para apagar o último dígito digitado
    public void DeleteDigit()
    {
        if (enteredPassword.Length > 0)
        {
            enteredPassword = enteredPassword.Substring(0, enteredPassword.Length - 1);
            UpdatePasswordText();
        }
    }

    // Método para verificar a senha
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
                messageText.text = "Número máximo de tentativas excedido. Sistema bloqueado.";
                DisableAllButtons();
            }
        }

        enteredPassword = ""; // Resetar a senha digitada após cada tentativa
        UpdatePasswordText();
    }

    // Método para atualizar o texto da senha no painel
    private void UpdatePasswordText()
    {
        passwordText.text = new string('*', enteredPassword.Length); // Mostrar asteriscos em vez dos dígitos da senha
    }

    // Método para redefinir o painel de senha para um novo ciclo
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
