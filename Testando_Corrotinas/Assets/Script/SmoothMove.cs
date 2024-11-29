using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothMove : MonoBehaviour
{
    public Transform target; // Alvo para onde o objeto se mover�.
    public float duration = 2f; // Dura��o do movimento em segundos.

    void Start()
    {
        // Verifica se o alvo est� configurado para evitar erros.
        if (target != null)
        {
            StartCoroutine(MoveOverTime(transform, target.position, duration));
        }
        else
        {
            Debug.LogWarning("Target n�o configurado para SmoothMove.");
        }
    }

    /// <summary>
    /// Corrotina para mover um objeto at� uma posi��o alvo em um per�odo de tempo.
    /// </summary>
    /// <param name="obj">Objeto a ser movido.</param>
    /// <param name="targetPos">Posi��o de destino.</param>
    /// <param name="time">Dura��o do movimento.</param>
    /// <returns></returns>
    IEnumerator MoveOverTime(Transform obj, Vector3 targetPos, float time)
    {
        Vector3 startPos = obj.position; // Posi��o inicial do objeto.
        float elapsed = 0f;

        // Executa o movimento enquanto o tempo decorrido for menor que a dura��o especificada.
        while (elapsed < time)
        {
            // Interpola��o linear para suavizar o movimento.
            obj.position = Vector3.Lerp(startPos, targetPos, elapsed / time);
            elapsed += Time.deltaTime; // Incrementa o tempo decorrido.
            yield return null; // Espera o pr�ximo frame.
        }

        // Garante que a posi��o final seja exatamente a posi��o de destino.
        obj.position = targetPos;
    }
}
