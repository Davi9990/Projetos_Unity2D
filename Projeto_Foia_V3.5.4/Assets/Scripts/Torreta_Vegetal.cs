using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torreta_Vegetal : MonoBehaviour
{
    public float Range;
    public Transform Target;
    bool Detect = false;
    Vector2 Direction;
    public GameObject AlarmLight;
    public GameObject Gun;
    public GameObject Bullet;
    public float FireRate;
    float nextTimeToFire = 0;
    public Transform ShootPoint;
    public float Force;
    public LayerMask playerLayer;

    void Start()
    {
        if (Target != null)
        {
            Debug.Log("Target encontrado: " + Target.name);
        }
        else
        {
            Debug.LogWarning("Target não atribuído.");
        }
    }

    void Update()
    {
        if (Target == null)
        {
            Debug.LogWarning("Target não atribuído.");
            return;
        }

        Vector2 targetPos = Target.position;
        Direction = targetPos - (Vector2)transform.position;
        Debug.DrawRay(transform.position, Direction, Color.green); // Visualize o raio no editor

        RaycastHit2D rayInfo = Physics2D.Raycast(transform.position, Direction, Range, playerLayer);

        if (rayInfo.collider != null)
        {
            Debug.Log("Raio colidiu com: " + rayInfo.collider.gameObject.name);
            if (rayInfo.collider.gameObject.CompareTag("Player"))
            {
                if (!Detect)
                {
                    Debug.Log("Alvo detectado.");
                    Detect = true;
                    AlarmLight.GetComponent<SpriteRenderer>().color = Color.red;
                }
            }
        }
        else
        {
            if (Detect)
            {
                Debug.Log("Alvo fora de alcance.");
                Detect = false;
                AlarmLight.GetComponent<SpriteRenderer>().color = Color.blue;
            }
        }

        if (Detect)
        {
            Gun.transform.up = Direction;
            if (Time.time > nextTimeToFire)
            {
                Debug.Log("Pronto para atirar.");
                nextTimeToFire = Time.time + 1 / FireRate;
                Shoot();
            }
        }
    }

    void Shoot()
    {
        Debug.Log("Executando Shoot()");
        GameObject BulletIns = Instantiate(Bullet, ShootPoint.position, ShootPoint.rotation);
        if (BulletIns != null)
        {
            Rigidbody2D rb = BulletIns.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(Direction.normalized * Force, ForceMode2D.Impulse);
                Debug.Log("Bala disparada com força: " + Direction.normalized * Force);
            }
            else
            {
                Debug.LogWarning("Rigidbody2D não encontrado na bala.");
            }
        }
        else
        {
            Debug.LogWarning("Instanciação da bala falhou.");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}
