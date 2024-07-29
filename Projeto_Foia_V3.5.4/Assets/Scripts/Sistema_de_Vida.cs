using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Sistema_de_Vida : MonoBehaviour
{
    public Image GreenBar;
    public Image RedBar;
    public Text arrowText;

    public int maxHealth = 5;
    int currentHealth;

    public float invincibleTime = 2;
    bool isInvicible;
    float invincibleTimer;

    public float speed = 5;
    Rigidbody rb;
    Vector2 lookDirection = new Vector2(0,-1);

    SpriteRenderer spriteRenderer;

    Vector2 direction;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
