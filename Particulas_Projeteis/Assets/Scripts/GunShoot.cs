using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShoot : MonoBehaviour
{
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public Transform muzzlePoint;
    public float range = 100f;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (muzzleFlash != null)
        {
            Debug.Log("Disparando partícula do cano...");
            muzzleFlash.Play();
        }
        else
        {
            Debug.LogWarning("muzzleFlash está NULL no momento do tiro!");
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range))
        {
            // Instancia partícula no ponto de impacto (apontado pelo cursor)
            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 1f);
        }
    }
}
