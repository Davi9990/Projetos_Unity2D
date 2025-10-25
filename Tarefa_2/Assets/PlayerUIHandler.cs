using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerUIHandler : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;

    private bool isPaused = false;

    //Esse método será chamado automaticamente pelo PlayerInput
    public void OnOpenMenu(InputAction.CallbackContext context)
    {
        //Apenas reage quando o botão é pressionado (não quando solto)
        if (context.performed)
        {
            ToggleMenu();
        }
    }

    //Chamado manualmente por botão da UI
    public void ToggleMenu()
    {
        if (menuPanel == null)
        {
            //Debug.LogWarning("menuPanel não atribuído em PlayerUIHandler.");
            return;
        }

        isPaused = !isPaused;
        menuPanel.SetActive(isPaused);

        if (isPaused)
        {
            // Pausa o jogo
            Time.timeScale = 0f;
            //Debug.Log("Menu aberto — jogo pausado");
        }
        else
        {
            // Retoma o jogo
            Time.timeScale = 1f;
            //Debug.Log("Menu fechado — jogo retomado");
        }
    }
    
    //Método wrapper sem parêmetro para usar no botão da UI
    public void ToggleMenuByUI()
    {
        ToggleMenu();
    }
}
