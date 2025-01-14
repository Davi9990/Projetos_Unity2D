using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparo_Padrao : MonoBehaviour
{
    public Transform Hand;
    public GameObject Balas;
    public float speedBulllets;
    public float fireRate = 1f;
    private float nextFireTime = 0f;
    private PlayerLogica virando;
    public Rigidbody2D Player;
    private PlayerLogica Pause;

    void Start()
    {
        Pause = GetComponent<PlayerLogica>();
        virando = GetComponent<PlayerLogica>();
        Player = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Pause.isPaused == false)
        {
            Atirar();
        }
    }

    private void Atirar()
    {
        if(Input.GetKeyDown(KeyCode.F) && Time.time >= nextFireTime)
        {
            GameObject newFire = Instantiate(Balas, Hand.position, Quaternion.identity);
            // Acessa o Rigidbody2D do projétil instanciado
            Rigidbody2D firerb = newFire.GetComponent<Rigidbody2D>();

            // Verifica a direção do player usando o isFacingRight
             float direction = virando != null && virando.isFacingRight ? 1 : -1;

            // Atribui uma velocidade ao projétil considerando a velocidade do jogador
            float bulletSpeed = speedBulllets + Mathf.Abs(Player.velocity.x); // Soma a velocidade do projétil à do jogador
            firerb.velocity = new Vector2(direction * bulletSpeed, 0);

            Destroy(newFire, 6f);
            nextFireTime = Time.time + fireRate;
        }
    }
}
