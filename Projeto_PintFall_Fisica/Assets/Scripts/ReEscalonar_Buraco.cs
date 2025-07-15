using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ReEscalonar_Buraco : MonoBehaviour
{
    //public float Reload;
    private Transform targetTransform;
    public float originalScaleX;
    public float openScaleX;
    public float openTimer;
    //ublic bool TimerIsRunning = true;
    public float closedTimer;
    public bool isOpen = false;
    //public BoxCollider2D chao;
    public TilemapCollider2D colider;

    void Start()
    {
        //TimerIsStop = false;
        if(targetTransform == null)
        {
            targetTransform = transform;
            targetTransform.localScale = new Vector3(openScaleX, targetTransform.localScale.y, targetTransform.localScale.z);
        }

        //ao.enabled = true;
        colider.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOpen)
        {
            closedTimer += Time.deltaTime;
            if(closedTimer >= 3f )
            {
                targetTransform.localScale = new Vector3(openScaleX, targetTransform.localScale.y, targetTransform.localScale.z);
                isOpen = true;
                closedTimer = 0f;
                //chao.enabled = false;
                colider.enabled = false;
            }
        }
        else
        {
            openTimer += Time.deltaTime;
            if(openTimer >= 3f)
            {
                targetTransform.localScale = new Vector3(originalScaleX, targetTransform.localScale.y, targetTransform.localScale.z);
                isOpen = false;
                openTimer = 0f;
                //chao.enabled = true;
                colider.enabled = true;
            }
        }

       
    }

}
