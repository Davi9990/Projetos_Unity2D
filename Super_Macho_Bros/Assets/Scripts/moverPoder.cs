using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class moverPoder : MonoBehaviour
{

    public float vel = 2.5f;
    public float forca = 3f;
    public float lifeTime = 5f; // Tempo de vida da prefab em segundos
    private Rigidbody2D rg;

    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime); // Destroi o game object após o tempo de vida definido
    }

    void Update()
    {
        transform.Translate(new Vector2(vel * Time.deltaTime, 0));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chao"))
        {
            rg.AddForce(new Vector2(0, forca), ForceMode2D.Impulse);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Enemys"))
        {
            Destroy(collision.gameObject); // Destroi o inimigo
            Destroy(gameObject); // Destroi o poder
        }
    }
}
