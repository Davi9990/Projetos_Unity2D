using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirando_e_Atirando : MonoBehaviour
{
    private Vector3 position;

    public Transform Hand;
    public float speedBullets;
    public GameObject Bullets;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                Vector3 touchpos =  Camera.main.ScreenToWorldPoint(touch.position);
                touchpos.z = 0f;
                transform.position = touchpos;
            }
            else if(touch.phase == TouchPhase.Ended)
            {
                Atirar();
            }
        }
    }

    public void Atirar()
    {
        GameObject newFire = Instantiate(Bullets,Hand.position, Quaternion.identity);
        Rigidbody2D fireRb = newFire.GetComponent<Rigidbody2D>();

        float bulletSpeed = speedBullets + Mathf.Abs(fireRb.velocity.x);
        fireRb.velocity = new Vector2(bulletSpeed, 0f);

        Destroy(newFire, 3f);
    }
}
