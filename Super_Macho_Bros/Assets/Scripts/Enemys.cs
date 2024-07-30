using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemys : MonoBehaviour
{
    Rigidbody2D rbGoomba;
    GameObject currentPrefab;
    public GameObject prefabUm, prefabDois;
    [SerializeField] float speed = 2f;
    [SerializeField] Transform point1, point2;
    [SerializeField] LayerMask layer;
    [SerializeField] bool isColliding;
    private bool isDead = false;

    private void Awake()
    {
        rbGoomba = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        ChangePrefab(prefabUm);
    }

    private void FixedUpdate()
    {
        rbGoomba.velocity = new Vector2(speed, rbGoomba.velocity.y);

        isColliding = Physics2D.Linecast(point1.position, point2.position, layer);

        if (isColliding)
        {
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            speed *= -1;
        }
    }

    void Update()
    {
        

        StartCoroutine(ChangePrefabCoroutine());
    }

    void ChangePrefab(GameObject newPrefab)
    {
        if (currentPrefab != null)
        {
            Destroy(currentPrefab);
        }

        currentPrefab = Instantiate(newPrefab, transform.position, transform.rotation);
        currentPrefab.transform.parent = transform;
    }

    System.Collections.IEnumerator ChangePrefabCoroutine()
    {
        yield return new WaitForSeconds(2);
        ChangePrefab(prefabUm);

        yield return new WaitForSeconds(2);
        ChangePrefab(prefabDois);
    }
}
