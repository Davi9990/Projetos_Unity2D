using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShooter : MonoBehaviour
{
    public Vector2 shootDirection = new Vector2(1f,0f);
    public float force = 5f;

    private bool shot = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!shot && Input.GetKeyDown(KeyCode.Escape))
        {
            GetComponent<Rigidbody2D>().AddForce(shootDirection.normalized * force, ForceMode2D.Impulse);
            shot = true;
        }
    }
}
