using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma2 : MonoBehaviour
{
    public enum Direction { Vertical, Horizontal }
    public Direction moveDirection = Direction.Vertical;

    public float speed = 2f;
    public float distance = 3f;

    private Vector3 startPos;
    private bool movingForward = true;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        MovePlatform();
    }

    void MovePlatform()
    {
        Vector3 targetPos;

        if (moveDirection == Direction.Vertical)
        {
            targetPos = movingForward ? startPos + Vector3.up * distance : startPos;
        }
        else // Horizontal
        {
            targetPos = movingForward ? startPos + Vector3.right * distance : startPos;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        if (transform.position == targetPos)
        {
            movingForward = !movingForward;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}
