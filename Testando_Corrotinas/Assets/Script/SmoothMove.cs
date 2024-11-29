using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothMove : MonoBehaviour
{
    public Transform target; // Alvo para onde o objeto se moverá.
    public float duration = 2f; // Duração do movimento em segundos.

    void Start()
    {
        // Verifica se o alvo está configurado para evitar erros.
        if (target != null)
        {
            StartCoroutine(MoveOverTime(transform, target.position, duration));
        }
        else
        {
            Debug.LogWarning("Target não configurado para SmoothMove.");
        }
    }

    /// <summary>
    /// Corrotina para mover um objeto até uma posição alvo em um período de tempo.
    /// </summary>
    /// <param name="obj">Objeto a ser movido.</param>
    /// <param name="targetPos">Posição de destino.</param>
    /// <param name="time">Duração do movimento.</param>
    /// <returns></returns>
    IEnumerator MoveOverTime(Transform obj, Vector3 targetPos, float time)
    {
        Vector3 startPos = obj.position; // Posição inicial do objeto.
        float elapsed = 0f;

        // Executa o movimento enquanto o tempo decorrido for menor que a duração especificada.
        while (elapsed < time)
        {
            // Interpolação linear para suavizar o movimento.
            obj.position = Vector3.Lerp(startPos, targetPos, elapsed / time);
            elapsed += Time.deltaTime; // Incrementa o tempo decorrido.
            yield return null; // Espera o próximo frame.
        }

        // Garante que a posição final seja exatamente a posição de destino.
        obj.position = targetPos;
    }
}
