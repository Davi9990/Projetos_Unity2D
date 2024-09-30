using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inputs2 : MonoBehaviour
{
    public GameObject projetil;
    public Transform clone;
    public float speed;

    float lastTapTime = 0f;
    float doubleTap = 0.3f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                if (Time.time - lastTapTime < doubleTap)
                {
                    // Instanciar o projetil
                    GameObject NewFire = Instantiate(projetil, clone.position, Quaternion.identity);
                    // Configurar o Rigidbody2D do projetil
                    Rigidbody2D fireRb = NewFire.GetComponent<Rigidbody2D>();
                    // Definir a velocidade para cima
                    fireRb.velocity = new Vector2(0, 1) * speed;
                    // Destruir o projetil após 3 segundos
                    Destroy(NewFire, 3f);
                }
            }

            //Atualizar o tempo do ultimo toque
            lastTapTime = Time.time;
        }
    }
}
