using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    GameManager gameManager;

    SpriteRenderer spriteRenderer;
    Rigidbody2D rbBlock;
    [SerializeField] private Color[] color = new Color[5];
    [SerializeField] private int sideSpawn = 0;
    
    private float v = 1;

    private float timeToReachCenter;
    
    private float xI = 12;

    private float currentY;

    void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        rbBlock = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();       
    }
    // Start is called before the first frame update
    void Start()
    {
        currentY = gameManager.yCurrent;
        timeToReachCenter = gameManager.tCurrent;

        while (sideSpawn == 0)
        {
            sideSpawn = Random.Range(-1, 2);
        }

        xI = sideSpawn * xI;

        print("sideSpawn: " + sideSpawn + " xI: " + xI);
        transform.position = new Vector3(xI, currentY, 0f);

        byte randomColor = (byte)Random.Range(0, color.Length);
        spriteRenderer.color = color[randomColor];

        Move();

        Invoke("Stop", timeToReachCenter);
    }
    void Move()
    {
       v = (xI) / timeToReachCenter;

       v = -v;
       
       rbBlock.velocity = new Vector2(v, 0f);
    }

    void Stop()
    {
       rbBlock.velocity = new Vector2(0f, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (gameManager.playerSucced)
            {
                Stop();
            }
        }
    }
}
