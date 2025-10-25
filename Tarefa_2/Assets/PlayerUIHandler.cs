using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerUIHandler : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;

    private bool isPaused = false;

    //Esse m�todo ser� chamado automaticamente pelo PlayerInput
    public void OnOpenMenu(InputAction.CallbackContext context)
    {
        //Apenas reage quando o bot�o � pressionado (n�o quando solto)
        if (context.performed)
        {
            ToggleMenu();
        }
    }

    //Chamado manualmente por bot�o da UI
    public void ToggleMenu()
    {
        if (menuPanel == null)
        {
            //Debug.LogWarning("menuPanel n�o atribu�do em PlayerUIHandler.");
            return;
        }

        isPaused = !isPaused;
        menuPanel.SetActive(isPaused);

        if (isPaused)
        {
            // Pausa o jogo
            Time.timeScale = 0f;
            //Debug.Log("Menu aberto � jogo pausado");
        }
        else
        {
            // Retoma o jogo
            Time.timeScale = 1f;
            //Debug.Log("Menu fechado � jogo retomado");
        }
    }
    
    //M�todo wrapper sem par�metro para usar no bot�o da UI
    public void ToggleMenuByUI()
    {
        ToggleMenu();
    }
}
