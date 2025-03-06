using System.Collections.Generic;
using UnityEngine;

public class Desenhando : MonoBehaviour
{
    public GameObject linhaPrefab; // Prefab com um LineRenderer
    private LineRenderer linhaAtual;
    private List<Vector3> pontos = new List<Vector3>();

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10f));

            if (touch.phase == TouchPhase.Began)
            {
                CriarNovaLinha(touchPos);
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                AdicionarPonto(touchPos);
            }
        }
    }

    void CriarNovaLinha(Vector3 posicao)
    {
        GameObject novaLinha = Instantiate(linhaPrefab);
        linhaAtual = novaLinha.GetComponent<LineRenderer>();
        linhaAtual.positionCount = 1;
        linhaAtual.SetPosition(0, posicao);
        pontos.Clear();
        pontos.Add(posicao);
    }

    void AdicionarPonto(Vector3 posicao)
    {
        
        if (pontos.Count == 0 || Vector3.Distance(pontos[pontos.Count - 1], posicao) > 0.1f)
        {
            pontos.Add(posicao);
            linhaAtual.positionCount = pontos.Count;
            linhaAtual.SetPosition(pontos.Count - 1, posicao);
        }
    }
}
