using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atirando_Projeteis : MonoBehaviour
{
    public Transform Hand;
    public GameObject fire;
    public Rigidbody2D corpo;
    public float speed = 5;
    //public Vector2 movement;

    public float fireRate = 1f;
    public float nextFireTime = 0f;


    // Start is called before the first frame update
    void Start()
    {
        corpo = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && Time.time >= nextFireTime)
        {
            FireProjectile();
        }
        
    }

    private void FireProjectile()
    {
        GameObject newFire = Instantiate(fire,Hand.position, Quaternion.identity);
        //Acessa o RigidBody2d do projétil instanciado
        Rigidbody2D fireRb = newFire.GetComponent<Rigidbody2D>();
        //Aplica uma força ao projetil na direção para a qual a mão está apontando
        fireRb.velocity = Hand.right * speed;
        Destroy(newFire, 6f);
        nextFireTime = Time.time + fireRate;
    }

    
}
