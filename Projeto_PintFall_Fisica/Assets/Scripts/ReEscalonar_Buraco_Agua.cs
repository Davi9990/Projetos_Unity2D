using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReEscalonar_Buraco_Agua : MonoBehaviour
{
    private Transform targetTransform;
    public float originalScaleX;
    public float openScaleX;
    public float openTimer;
    public float closedTimer;
    public bool isOpen = false;
    //public SistemasDeVidas vida;
    public int dano;
    public AudioClip damageSound;

    void Start()
    {
        if(targetTransform == null)
        {
            targetTransform = transform;
            targetTransform.localScale = new Vector3(openScaleX, targetTransform.localScale.y, targetTransform.localScale.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOpen)
        {
            closedTimer += Time.deltaTime;
            if(closedTimer >= 2 )
            {
                targetTransform.localScale = new Vector3(openScaleX, targetTransform.localScale.y, targetTransform.localScale.z);
                isOpen = true;
                closedTimer = 0f;
            }
        }
        else
        {
            openTimer += Time.deltaTime;
            if(openTimer >= 2 )
            {
                targetTransform.localScale = new Vector3(originalScaleX, targetTransform.localScale.y, targetTransform.localScale.z);
                isOpen = false;
                openTimer = 0f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SistemasDeVidas vida = collision.gameObject.GetComponent<SistemasDeVidas>();

            if (vida != null)
            {
                vida.vidaatual = vida.vida;
                vida.vida -= dano;
                vida.AtualizarHudDeVida();
            }
            else
            {
                Debug.LogWarning("SistemasDeVidas n�o encontrado no objeto do player");
            }
            Debug.Log("Levando Dano continuo");

            if(damageSound != null)
            {
                AudioSource.PlayClipAtPoint(damageSound, Camera.main.transform.position, 1.0f);
            }
        }
    }
}
