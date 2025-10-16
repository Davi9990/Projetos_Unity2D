using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Deposito : MonoBehaviour
{
    [TextArea(2, 3)]
    public string novoObjetivo = "Volte ao engenho para receber novas ordens.";

    void OnTriggerEnter(Collider outro)
    {
        Carregador carregador = outro.GetComponent<Carregador>();
        
        if(carregador != null && carregador.canasCarregadas.Count > 0)
        {
            carregador.EntregarCanas();

            GerenciadorJogo.instancia.TarefaConcluida();
            GerenciadorUI.instancia.MostrarMensagem("Cana Entregue!");
            GerenciadorUI.instancia.AtualizarObjetivo(novoObjetivo);
        }
    }
}
