using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class caixamove : MonoBehaviour
{
    public float speed = 2f;
    public float limitX = 2f;
    private int direction = 1;

    void Update()
    {
        transform.Translate(Vector3.right * direction * speed * Time.deltaTime);

        if (transform.position.x > limitX)
            direction = -1;
        else if (transform.position.x < -limitX)
            direction = 1;
    }
}
