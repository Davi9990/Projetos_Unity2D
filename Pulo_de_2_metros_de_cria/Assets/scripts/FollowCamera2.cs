using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public float yOffset = 2f;

    private float highestY;

    void LateUpdate()
    {
        if (target.position.y > highestY)
        {
            highestY = target.position.y;
        }

        Vector3 desiredPosition = new Vector3(transform.position.x, highestY + yOffset, transform.position.z);
        Vector3 smoothed = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothed;
    }
}
