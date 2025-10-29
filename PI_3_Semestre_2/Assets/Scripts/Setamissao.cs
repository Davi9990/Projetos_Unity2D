using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetaMissao : MonoBehaviour
{
    public Transform jogador, alvoMissao;        
    public Image setaImagem;     
    public float distanciaMaxima = 50f; 

    void Update()
    {
        if (alvoMissao == null || jogador == null || setaImagem == null) return;

        Vector3 direcao = alvoMissao.position - jogador.position;
        direcao.y = 0;

        float angulo = Mathf.Atan2(direcao.x, direcao.z) * Mathf.Rad2Deg;
        setaImagem.rectTransform.rotation = Quaternion.Euler(0, 0, -angulo);

        float distancia = Vector3.Distance(jogador.position, alvoMissao.position);
        setaImagem.enabled = distancia > 2f && distancia < distanciaMaxima;
    }

    public void DefinirAlvo(Transform novoAlvo)
    {
        alvoMissao = novoAlvo;
    }
}
