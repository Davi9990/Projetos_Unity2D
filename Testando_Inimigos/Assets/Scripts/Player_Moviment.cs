using UnityEngine;

public class Player_Moviment : MonoBehaviour
{
    [Header("Movimentação")]
    public float velocidade;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movimentar();
    }

    void Movimentar()
    {
        transform.Translate(new Vector2(Input.GetAxisRaw("Horizontal") * velocidade * Time.deltaTime
            , Input.GetAxisRaw("Vertical") * velocidade * Time.deltaTime));
    }
}
