using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPraSelecao : MonoBehaviour
{
    public Transform Hand;
    GameObject ArmaSelecionada;
    public GameObject TiroFogo, EscudoDagua, Rocha, Selecao;
    public Button b1, b2, b3;
    int Selecionado = 0; //Vamos considerar que ele já vai começar com as três armas por enquanto

    
    void Start()
    {
        Selecao.SetActive(false);
        BotaoSelecionado();
    }

    // Update is called once per frame
    void Update()
    {
        
        abrirmenu();
        TrocaRápida();
        atirando();
    }

    public void abrirmenu()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            PausaDespausa();
        }

    }

    public void trocaarma(string n)
    {
        switch (n)
        {
            case "Fogo": Selecionado = 1; ArmaAtual();  break;
            case "Agua": Selecionado = 2; ArmaAtual(); break;
            case "Terra": Selecionado = 3; ArmaAtual(); break;
        }
        PausaDespausa();
    }

    void TrocaRápida()
    {
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Selecionado--;
            if(Selecionado < 0)
            {
                Selecionado = 3;
            }
            ArmaAtual();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            Selecionado++;
            if(Selecionado > 3)
            {
                Selecionado = 0;
            }
            ArmaAtual();
        }
    }

    void PausaDespausa()
    {
        if(Time.timeScale == 0)
        {
            Selecao.SetActive(false);
            Time.timeScale = 1;
        }
        else if(Time.timeScale == 1)
        {
            //Ainda precisa desativar comandos de movimentação
            Selecao.SetActive(true);
            Time.timeScale = 0;
        }
    }

    void BotaoSelecionado()
    {
        switch (Selecionado)
        {
            default: b1.Select(); break;
            case 1: b1.Select(); break;
            case 2: b2.Select(); break;
            case 3: b3.Select(); break;
        }
    }

    void ArmaAtual()
    {
        switch (Selecionado)
        {
            case 0: ArmaSelecionada = null; Debug.Log("Não tem nada selecionada"); break;
            case 1: ArmaSelecionada = TiroFogo; Debug.Log("Arma De Fogo"); break;
            case 2: ArmaSelecionada = EscudoDagua; Debug.Log("Arma de Agua");  break;
            case 3: ArmaSelecionada = Rocha; Debug.Log("Arma de Terra"); break;
        }
        BotaoSelecionado();
    }


    void atirando()
    {
        //Não esquecer de trocar a posição dos vetores pra frente do objeto
        if (Input.GetKeyDown(KeyCode.J) && ArmaSelecionada != null)
        {
            switch (Selecionado)
            {
                case 1: Instantiate(ArmaSelecionada, Hand.position, Quaternion.identity); break;
                case 2: Instantiate(ArmaSelecionada, Hand.position, Quaternion.identity); break;
                case 3: Instantiate(ArmaSelecionada, Hand.position, Quaternion.identity); break;
            }
            
        }
        else if(Input.GetKeyDown(KeyCode.J) && ArmaSelecionada == null)
        {
            Debug.Log("NADA SELECIONADO PARA ATIRAR");
        }
    }
}
