using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorCanas : MonoBehaviour
{
    [System.Serializable]
    public class PontoSpawn { public Transform ponto; public GameObject instanciaAtual; }

    public GameObject prefabCana;
    public List<PontoSpawn> pontos = new List<PontoSpawn>();
    public float respawnSegundos = 20f;

    void Start()
    {
        foreach (var p in pontos)
        {
            if (p.ponto != null && prefabCana != null)
            {
                var go = Instantiate(prefabCana, p.ponto.position, p.ponto.rotation);
                p.instanciaAtual = go;
                var pickup = go.GetComponent<CanaPickup>();
                if (pickup) pickup.gerador = this;
            }
        }
    }

    public void RegistrarColeta(CanaPickup pickup)
    {
        for (int i = 0; i < pontos.Count; i++)
        {
            if (pontos[i].instanciaAtual == pickup.gameObject)
            {
                StartCoroutine(RespawnCoroutine(i));
                pontos[i].instanciaAtual = null;
                break;
            }
        }
    }

    IEnumerator RespawnCoroutine(int indice)
    {
        yield return new WaitForSeconds(respawnSegundos);
        var p = pontos[indice];
        if (p.ponto != null && prefabCana != null)
        {
            var go = Instantiate(prefabCana, p.ponto.position, p.ponto.rotation);
            p.instanciaAtual = go;
            var pickup = go.GetComponent<CanaPickup>();
            if (pickup) pickup.gerador = this;
        }
    }
}