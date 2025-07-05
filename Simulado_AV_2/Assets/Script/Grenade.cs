using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float delay = 3f;
    public float radius = 5f;
    public float force = 700f;
    public GameObject explosionEffect;


    void Start()
    {
        Invoke("Explode", delay);
    }

    // Update is called once per frame
    void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider nearbyObject in colliders)
        {
            if (nearbyObject.CompareTag("Atingível"))
            {
                Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(force, transform.position, radius);
                }

                // Efeito opcional (dano visual)
                Renderer rend = nearbyObject.GetComponent<Renderer>();
                if (rend != null) rend.material.color = Color.red;
            }
        }
        Destroy(gameObject);
    }
}
