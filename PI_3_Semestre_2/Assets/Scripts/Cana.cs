using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cana : MonoBehaviour
{
    private bool CanaColetada = false;

    void OnTriggerEnter(Collider outro)
    {
        Carregador carregador = outro.GetComponent<Carregador>();
        if (carregador != null && !CanaColetada)
        {
            CanaColetada = true;

            // adiciona a cana ao jogador
            carregador.AdicionarCana(this.gameObject);           

            // inicia respawn
            StartCoroutine(Renascer());
        }
    }

    IEnumerator Renascer()
    {
        yield return new WaitForSeconds(5f); // espera 20 segundos
        CanaColetada = false;
        gameObject.SetActive(true); // reaparece no campo
    }
}
