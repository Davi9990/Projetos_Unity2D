using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallControler : MonoBehaviour
{
    Rigidbody2D rb;

    [Range(0.2f, 2f)]
    public float moveSpeedModifier = 0.5f;

    float dirX, dirY;

    Animator anim;

    static bool isDead;

    static bool moveAllowed;

    static bool youWin;

    [SerializeField]
    GameObject winText;


    void Start()
    {
        winText.gameObject.SetActive(false);
        
        youWin = false;

        moveAllowed = true;

        isDead = false;

        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

        anim.SetBool("BallDead", isDead);
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.acceleration.x * moveSpeedModifier;
        dirY = Input.acceleration.y * moveSpeedModifier;

        if (isDead) 
        {
            rb.velocity = new Vector2(0, 0);

            anim.SetBool("BallDead", isDead);

            Invoke("RestartScene", 1f);
        }

        if (youWin) 
        {
            rb.velocity = new Vector2(0, 0);

            winText.gameObject.SetActive (true);

            moveAllowed = false;

            anim.SetBool("BallDead", true);

            Invoke("RestartScene", 2f);
        }
    }

    private void FixedUpdate()
    {
        if (moveAllowed) 
        {
            rb.velocity = new Vector2(rb.velocity.x + dirX, rb.velocity.y + dirY);
        }
    }

    public static void setIsDeadTrue()
    {
        isDead = true;
    }

    public static void setYouWinToTrue()
    {
        youWin = true;
    }

    void ResterScene()
    {
        SceneManager.LoadScene("Scene01");
    }
}
