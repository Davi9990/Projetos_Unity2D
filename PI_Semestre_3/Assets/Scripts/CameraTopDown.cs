using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTopDown : MonoBehaviour
{ 
    [Header("Alvo")]
    public Transform alvo; // arraste o jogador

    [Header("Configuração")]
    public float altura = 20f;   // altura da câmera em relação ao jogador
    public float distancia = 0f; // se quiser deixar a câmera mais atrás no eixo Z

    void LateUpdate()
    {
        if (alvo == null) return;

        // posição da câmera (sempre em cima do jogador)
        transform.position = new Vector3(alvo.position.x, altura, alvo.position.z - distancia);

        // rotação fixa para olhar sempre para baixo
        transform.rotation = Quaternion.Euler(45f, 0f, 0f);
    }
}