using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrocaCarga : MonoBehaviour
{
    public int canasPorLinha = 5;
    public int linhasPorCamada = 5;
    public float espacamento = 0.1f;

    private List<GameObject> canas = new List<GameObject>();

    public void AdicionarCana(GameObject cana)
    {
        canas.Add(cana);
        cana.transform.SetParent(transform);

        int index = canas.Count - 1;
        int camada = index / (canasPorLinha * linhasPorCamada);
        int dentroCamada = index % (canasPorLinha * linhasPorCamada);
        int linha = dentroCamada / canasPorLinha;
        int coluna = dentroCamada % canasPorLinha;

        Vector3 pos = new Vector3(coluna * espacamento, camada * espacamento, -linha * espacamento);
        cana.transform.localPosition = pos;
        cana.transform.localRotation = Quaternion.identity;

        var sr = cana.GetComponent<SpriteRenderer>();
        if (sr) sr.enabled = true;
    }

    public List<GameObject> EntregarTodas() 
    {
        List<GameObject> entregues = new List<GameObject>(canas);
        foreach (var c in entregues) c.transform.SetParent(null);
        canas.Clear();
        return entregues;
    }

    public int Quantidade() { return canas.Count; }
}