using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    private IEnumerator newShakeCoroutine;
    private Vector3 initialPosition;
    private bool coroutineAllowed;


    void Start()
    {
        initialPosition = transform.position;
        coroutineAllowed = true;
    }

    private void OnMouseDown()
    {
        if (coroutineAllowed) 
        {
            StartShaking();
        }
    }

    private void StartShaking()
    {
        coroutineAllowed = false;
        newShakeCoroutine = ShakeCoroutine();
        StartCoroutine(newShakeCoroutine);
        Invoke("StopShaking", 0.5f);
    }

    private void StopShaking()
    {
        StopCoroutine(newShakeCoroutine);
        transform.position = initialPosition;
        coroutineAllowed = true;
    }

    private IEnumerator ShakeCoroutine()
    {
        coroutineAllowed = false;

        while (true)
        {
            float offsetX = Random.Range(-0.1f, 0.1f);
            float offsetY = Random.Range(-0.1f, 0.1f);

            transform.position = new Vector3(transform.position.x + offsetX, transform.position.y + offsetY, 0f);

            yield return new WaitForSeconds(0.01f);

            transform.position = initialPosition;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
