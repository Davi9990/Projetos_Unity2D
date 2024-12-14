using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class Player_Controler : MonoBehaviourPun
{
    public float speed = 10f;
    public float jumpForce = 5f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (!photonView.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>());
        }
    }

    void Update()
    {
        if (!photonView.IsMine) return;

        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(moveHorizontal, 0, 1) * speed * Time.deltaTime;
        transform.Translate(movement);

        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.velocity.y) < 0.01f)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
