using UnityEngine;

public class MinimapFollow : MonoBehaviour
{
    public Transform player; // Refer�ncia ao Transform do jogador

    void LateUpdate()
    {
        Vector3 newPosition = player.position;
        newPosition.z = transform.position.z; // Mant�m a posi��o Z da c�mera do minimapa
        transform.position = newPosition;
    }
}
