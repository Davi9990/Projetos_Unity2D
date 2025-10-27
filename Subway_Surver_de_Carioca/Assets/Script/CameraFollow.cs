using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0f, 6f, -8f);
    public float smoothTime = 0.15f;
    Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (player == null) return;
        Vector3 target = player.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, smoothTime);
        transform.LookAt(player.transform.position + Vector3.up * 1.5f);
    }
}
