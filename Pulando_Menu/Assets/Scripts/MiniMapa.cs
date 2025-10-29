using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapa : MonoBehaviour
{
    public Transform jogador;

    void LateUpdate()
    {
        Vector3 novaPosicao = jogador.position;
        novaPosicao.y = transform.position.y; // mantém a altura fixa
        transform.position = novaPosicao;     // segue o jogador
        transform.rotation = Quaternion.Euler(90f, 0f, 0f); // rotação fixa (sem girar)
    }
}
