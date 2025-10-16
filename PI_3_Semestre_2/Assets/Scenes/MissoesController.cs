using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissoesController : MonoBehaviour
{
    public Text textoMissao; // UI de texto na tela
    public int canasPlantadas = 0;
    public int canasColetadas = 0;
    public int canasEntregues = 0;
    public int troncosColetados = 0;

    private int etapa = 0; // controle da missão atual

    void Start()
    {
        AtualizarMissao("Plante 3 canas-de-açúcar.");
    }

    void Update()
    {
        // Exemplo de ações com teclas (para teste rápido)
        if (Input.GetKeyDown(KeyCode.P)) // plantar
        {
            PlantarCana();
        }

        if (Input.GetKeyDown(KeyCode.C)) // coletar
        {
            ColetarCana();
        }

        if (Input.GetKeyDown(KeyCode.D)) // depositar
        {
            DepositarCana();
        }

        if (Input.GetKeyDown(KeyCode.K)) // falar com capataz
        {
            FalarComCapataz();
        }

        if (Input.GetKeyDown(KeyCode.T)) // coletar tronco
        {
            ColetarTronco();
        }

        if (Input.GetKeyDown(KeyCode.F)) // colocar na fornalha
        {
            ColocarNaFornalha();
        }

        if (Input.GetKeyDown(KeyCode.M)) // pegar mapa
        {
            PegarMapa();
        }
    }

    void PlantarCana()
    {
        if (etapa == 0)
        {
            canasPlantadas++;
            Debug.Log("Cana plantada: " + canasPlantadas);

            if (canasPlantadas >= 3)
            {
                etapa = 1;
                AtualizarMissao("Agora colete 3 canas.");
            }
        }
    }

    void ColetarCana()
    {
        if (etapa == 1)
        {
            canasColetadas++;
            Debug.Log("Cana coletada: " + canasColetadas);

            if (canasColetadas >= 3)
            {
                etapa = 2;
                AtualizarMissao("Leve as canas até o depósito (aperte D).");
            }
        }
    }

    void DepositarCana()
    {
        if (etapa == 2)
        {
            canasEntregues += canasColetadas;
            canasColetadas = 0;
            Debug.Log("Canas entregues: " + canasEntregues);
            etapa = 3;
            AtualizarMissao("Fale com o capataz (aperte K).");
        }
    }

    void FalarComCapataz()
    {
        if (etapa == 3)
        {
            etapa = 4;
            AtualizarMissao("Colete 3 troncos de madeira (aperte T).");
        }
    }

    void ColetarTronco()
    {
        if (etapa == 4)
        {
            troncosColetados++;
            Debug.Log("Tronco coletado: " + troncosColetados);

            if (troncosColetados >= 3)
            {
                etapa = 5;
                AtualizarMissao("Coloque os troncos na fornalha (aperte F).");
            }
        }
    }

    void ColocarNaFornalha()
    {
        if (etapa == 5)
        {
            troncosColetados = 0;
            etapa = 6;
            AtualizarMissao("Pegue o mapa com o NPC (aperte M).");
        }
    }

    void PegarMapa()
    {
        if (etapa == 6)
        {
            etapa = 7;
            AtualizarMissao("Missão concluída! Parabéns!");
        }
    }

    void AtualizarMissao(string msg)
    {
        if (textoMissao != null)
            textoMissao.text = msg;
        Debug.Log(msg);
    }
}
