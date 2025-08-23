using UnityEngine;

public class Inimigo_Move_Bomba : MonoBehaviour
{
    [Header("Localizando Player / Movimentação")]
    public string playerTag = "Player";
    private GameObject player;
    public float velocidade = 5f;
    public float distanceAttack = 15f;
    private Rigidbody2D rb;

    [Header("Atirando com Molotov")]
    public GameObject molotovPrefab;
    public float velocidadeProjetil = 10f;
    public Transform PontodeTiro;
    public float lifeTimeProjetil = 2f;
    private float tempodoultimotiro = 0f;
    public float tempodeespera = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //Procura pelo jogador com base e nba tag
        player = GameObject.FindGameObjectWithTag(playerTag);

        tempodoultimotiro = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
       
        if(player != null)
        {
            float distanciaParaoJogador = Vector2.Distance(transform.position, player.transform.position);
            
            if(distanciaParaoJogador <= distanceAttack)
            {
                Perseguir();
                Atirando();
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
    }

    void Perseguir()
    {
        Vector2 direction = ((Vector2)player.transform.position - (Vector2)transform.position).normalized;
        rb.velocity = direction * velocidade;

        float ang = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = ang;
    }

    void Atirando()
    {
        //Cuuldown do tiro
        if (Time.time >= tempodoultimotiro + tempodoultimotiro)
        {
            if (molotovPrefab == null || PontodeTiro == null) return;

            Vector2 direction = ((Vector2)player.transform.position - (Vector2)transform.position).normalized;

            float zRot = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            GameObject Molotov = Instantiate(molotovPrefab, PontodeTiro.position, Quaternion.identity);


            Rigidbody2D rbMolotov = Molotov.GetComponent<Rigidbody2D>();

            if (rbMolotov != null)
            {
                rbMolotov.gravityScale = 0;
                rbMolotov.velocity = direction * velocidadeProjetil;
            }

            Destroy(Molotov, lifeTimeProjetil);
            tempodoultimotiro = Time.time;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,distanceAttack);
    }
}
