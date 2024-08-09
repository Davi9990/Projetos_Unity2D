using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform Player;
    void Start()
    {
        Player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Player.position.x >= transform.position.x)
            transform.position = new Vector3(Player.position.x,Player.position.y,transform.position.z);
    }
}
