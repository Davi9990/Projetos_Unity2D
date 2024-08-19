using UnityEngine;

public class MinimapFollow : MonoBehaviour
{
    public Transform player; // Referência ao Transform do jogador

    void LateUpdate()
    {
        Vector3 newPosition = player.position;
        newPosition.z = transform.position.z; // Mantém a posição Z da câmera do minimapa
        transform.position = newPosition;
    }
}
