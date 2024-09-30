using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TirosContinuos : MonoBehaviour
{
    public GameObject PROJETIL;
    public Transform clone;
    public float speed;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Input.touchCount; i++) 
        {
            if (Input.GetTouch(i).phase == TouchPhase.Stationary) 
            {
                GameObject Newfire = Instantiate(PROJETIL, clone.position, Quaternion.identity);
                Rigidbody2D fireRb = Newfire.GetComponent<Rigidbody2D>();
                fireRb.velocity = new Vector2(0, 1) * speed;
                Destroy( Newfire, 3f );
            }
        } 
    }
}
