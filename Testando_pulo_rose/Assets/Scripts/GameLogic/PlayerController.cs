using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    Animator anim;
    GameManager gameManager;
    Rigidbody2D rb;

    [Header("RayCastY")]
    [SerializeField] bool getBlock = false;
    [SerializeField] GameObject lastBlock;
    RaycastHit2D hit;
    public LayerMask layerMask;
    [SerializeField] private float distance;
    [SerializeField] Transform SpawnPoint;
    
    [Header("RayCastx")]
    public bool touchBlockSide = false;
    /*hitX1;
    RaycastHit2D hitX2;
    [SerializeField] private float distanceSides;
    public Transform SpawnPointXL;
    public Transform SpawnPointXR ;*/

    [Header("Jump Controller")]
    byte totalJump = 1;
    [SerializeField] private bool isJumping = false;
    [SerializeField] private bool isGrounded = false;

    float yAlturaMaxima = 2f;

    void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        rb = GetComponent<Rigidbody2D>(); 
        anim = GetComponent<Animator>();  

    } 
    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameOver)
        {
            return;
        }

        //BlockCheck();

        Jump();
    }

    void Jump()
    {
        if (isGrounded && gameManager.isInstanciate)
        {
            if (Input.GetKeyDown(KeyCode.P) || Input.touchCount > 0 || Input.GetMouseButtonDown(0))
            {
                print("Posso pular?");

                float a = Physics.gravity.magnitude;
                float m = rb.mass;

                float v = math.sqrt(2 * a * yAlturaMaxima);

                Vector2 f = m * v * Vector2.up ;

                rb.AddForce(f, ForceMode2D.Impulse);

                Debug.Log("v: " + v + " f: " + f + " a: " + a);  

                //gameManager.GetScussedPayer(false, false);
                anim.SetBool("isJump", true);
                isJumping = true;
                isGrounded = false;
                totalJump = 0;
            }
        }
        if (rb.velocity.y >= 0)
        {
            rb.gravityScale = 1;
        }
        else if (rb.velocity.y < 0)
        {
            rb.gravityScale = 3;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                Vector2 normal = contact.normal;

                if (Mathf.Abs(normal.x) > 0.5f)
                {
                    // Colidiu pela lateral
                    Debug.Log("Colidiu pela lateral!");

                    gameManager.GetScussedPayer(false, true);
                    rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                    touchBlockSide = true;
                    break;
                }
            }

            foreach (ContactPoint2D contact in collision.contacts)
            {
                Vector2 normal = contact.normal;

                if (Mathf.Abs(normal.y) > 0.6f)
                {
                    Debug.Log("Colidiu pela Horizontal!");
                    
                    rb.gravityScale = 1;
                    isGrounded = true;
                    isJumping = false;
                    anim.SetBool("isJump", false);
                        
                    // Colidiu pela lateral

                        if (!getBlock && lastBlock != collision.gameObject)
                        {
                            Debug.Log("Não é igual");
                            lastBlock = collision.gameObject;
                            getBlock = true;
                        }
                        
                        if (getBlock && lastBlock == collision.gameObject)
                        {
                            //GetNewPosY();
                            Debug.Log("Mesmo");
                            gameManager.GetScussedPayer(true, false);
                            getBlock = false;
                        }

                        else if (!getBlock && lastBlock == collision.gameObject)
                        {
                            print("É o mesmo");
                            //lastBlock = hit.collider.gameObject;
                        }

                        Debug.Log("Hit Block down. Get Block: " + getBlock + "GameObject: " + collision);
                    break;
                }
            }
        }
    }
}
