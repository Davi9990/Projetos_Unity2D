using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiro_De_Pedrada : MonoBehaviour
{
    // Atirar
    public Transform PontoDeTiroPedra1;
    public Transform PontoDeTiroPedra2;
    public GameObject Pedra;
    public float speedBullets;
    public float fireRate = 1f;
    public float nextFireTime = 0f;
    public Rigidbody2D Player;
    private PlayerLogica Pause;

    private PlayerLogica virando;

    void Start()
    {
        virando = GetComponent<PlayerLogica>();
        Player = GetComponent<Rigidbody2D>();

        if (virando == null)
        {
            Debug.LogError("PlayerLogica não encontrado");
        }
    }

    void Update()
    {
       
            AtualizarPontosDeTiro();
            AtirarPedraEmMendigo();
  
    }

    private void AtualizarPontosDeTiro()
    {
        if (virando != null)
        {
            // Se o jogador estiver virando para a esquerda, inverta a posição X dos pontos de tiro
            float direction = virando.isFacingRight ? 1 : -1;

            PontoDeTiroPedra1.localPosition = new Vector3(Mathf.Abs(PontoDeTiroPedra1.localPosition.x) * direction,
                                                          PontoDeTiroPedra1.localPosition.y,
                                                          PontoDeTiroPedra1.localPosition.z);

            PontoDeTiroPedra2.localPosition = new Vector3(Mathf.Abs(PontoDeTiroPedra2.localPosition.x) * direction,
                                                          PontoDeTiroPedra2.localPosition.y,
                                                          PontoDeTiroPedra2.localPosition.z);
        }
    }

    private void AtirarPedraEmMendigo()
    {
        if (Input.GetKeyDown(KeyCode.F) && Time.time >= nextFireTime)
        {
            GameObject newPedra = Instantiate(Pedra, PontoDeTiroPedra1.position, Quaternion.identity);
            GameObject newPedra2 = Instantiate(Pedra, PontoDeTiroPedra2.position, Quaternion.identity);

            // Acessa os Rigidbody2D dos projéteis instanciados
            Rigidbody2D PedraRb = newPedra.GetComponent<Rigidbody2D>();
            Rigidbody2D PedraRb2 = newPedra2.GetComponent<Rigidbody2D>();

            float direction = virando != null && virando.isFacingRight ? 1 : -1;

            float bulletSpeed = speedBullets + Mathf.Abs(Player.velocity.y);
            PedraRb.velocity = new Vector2(direction * bulletSpeed, 0);
            PedraRb2.velocity = new Vector2(direction * bulletSpeed, 0);

            Destroy(newPedra, 2f);
            Destroy(newPedra2, 2f);
            nextFireTime = Time.time + fireRate;
        }
    }
}
