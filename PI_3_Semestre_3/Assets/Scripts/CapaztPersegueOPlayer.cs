using System.Collections;
using UnityEngine;

public class CapaztPersegueOPlayer : MonoBehaviour
{
    public enum EstadoInimigo { Patrulha, Perseguicao, Buscando}

    [Header("Movimento")]
    public float velocidade = 2f;
    public float raioDeMovimento = 10f;
    public float gravidade = -9.81f;

    [Header("Visao e Perseguicao")]
    public float raioDeVisao = 12f;                   
    public string[] tagsQueBloqueiamVisao;
    public float tempoMaximoDeBusca = 5f;
    //public float intervaloPerseguicao = 60f;          
    public float eyeHeight = 1.5f;                    

    [Header("Ataque")]
    public float distanciaAtaque = 1.5f;
    public int danoAoPlayer = 1;
    public float tempoEntreDanos = 1f;                

    [Header("Referencias")]
    public Animator animador;

    // estado interno
    private EstadoInimigo estadoAtual = EstadoInimigo.Patrulha;
    private bool perseguindo = false;
    private bool andando = false;
    private Transform jogador;
    private CharacterController controlador;
    private Vector3 destinoAtual;
    private float velocidadeVertical = 0f;
    private float ultimoDanoTempo = -999f;
    private Coroutine rotinaDeBusca;

    // indicador visual
    private GameObject esferaVisao;

    void Start()
    {
        controlador = GetComponent<CharacterController>();
        if (controlador == null)
            controlador = gameObject.AddComponent<CharacterController>();

        GameObject objJogador = GameObject.FindWithTag("Player");
        if (objJogador != null)
        {
            jogador = objJogador.transform;
            //StartCoroutine(PerseguirACadaMinuto());
        }
        else
        {
            Debug.LogWarning("Nenhum objeto com a tag 'Player' foi encontrado!");
        }

        EscolherNovoDestino();

        if (animador == null)
            animador = GetComponent<Animator>();

        CriarIndicadorVisao();
        AtualizarIndicadorVisao();

        Debug.Log("Inimigo iniciado no estado: " + estadoAtual);
    }

    void Update()
    {
        bool jogadorVisivel = JogadorEstaDentroDoRaioDeVisao() && JogadorEhVisivel();

        if (jogadorVisivel)
        {
            if(estadoAtual != EstadoInimigo.Perseguicao)
            {
                MudarEstado(EstadoInimigo.Perseguicao);
            }
        }
        else
        {
           if(estadoAtual == EstadoInimigo.Perseguicao)
            {
                MudarEstado(EstadoInimigo.Buscando);
            }
        }

        switch (estadoAtual)
        {
            case EstadoInimigo.Patrulha:
                AndarAleatoriamente();
                break;
            case EstadoInimigo.Perseguicao:
            case EstadoInimigo.Buscando:
                PerseguirJogador();
                break;
        }

        AplicarGravidade();
        ChecarAplicarDanoAoContato();

        if(esferaVisao != null)
        {
            esferaVisao.transform.position = transform.position + Vector3.up * 0.05f;

            float diametro = raioDeVisao * 2f;
            if(!Mathf.Approximately(esferaVisao.transform.localScale.x, diametro))
                AtualizarIndicadorVisao();
        }
    }

    void MudarEstado(EstadoInimigo novoEstado)
    {
        if (estadoAtual == novoEstado) return;

        if(estadoAtual == EstadoInimigo.Buscando && rotinaDeBusca != null)
        {
            StopCoroutine(rotinaDeBusca);
            rotinaDeBusca = null;
        }

        estadoAtual = novoEstado;
        Debug.Log("Inimigo mudou para o estado: " + estadoAtual);

        if(novoEstado == EstadoInimigo.Perseguicao)
        {
            destinoAtual = jogador.position;
        }
        else if(novoEstado == EstadoInimigo.Buscando)
        {
            destinoAtual = jogador.position;
            rotinaDeBusca = StartCoroutine(ContadorDeBusca());
        }
        else if(novoEstado == EstadoInimigo.Patrulha)
        {
            EscolherNovoDestino();
            AtualizarAnimacao(false);
        }
    }

    IEnumerator ContadorDeBusca()
    {
        yield return new WaitForSeconds(tempoMaximoDeBusca);

        if(estadoAtual == EstadoInimigo.Buscando)
        {
            MudarEstado(EstadoInimigo.Patrulha);
        }

        rotinaDeBusca = null;
    }

    void AndarAleatoriamente()
    {
        Vector3 direcao = destinoAtual - transform.position;
        direcao.y = 0f;

        if (direcao.magnitude > 0.5f)
        {
            controlador.Move(direcao.normalized * velocidade * Time.deltaTime);
            transform.forward = Vector3.Lerp(transform.forward, direcao.normalized, Time.deltaTime * 5f);
            AtualizarAnimacao(true);
        }
        else
        {
            AtualizarAnimacao(false);
        }

        if (Vector3.Distance(transform.position, destinoAtual) < 0.5f)
            EscolherNovoDestino();
    }

    void PerseguirJogador()
    {
        if (jogador == null) return;

        Vector3 alvo = (estadoAtual == EstadoInimigo.Perseguicao) ? jogador.position : destinoAtual;

        Vector3 direcao = (alvo - transform.position);
        direcao.y = 0f;
        Vector3 direcaoNormalizada = direcao.normalized;

        float distanciaRestante = Vector3.Distance(transform.position, alvo);

        if(estadoAtual == EstadoInimigo.Buscando && distanciaRestante < 0.5f)
        {
            AtualizarAnimacao(false);
            return;
        }

       
        // Movimento Normal
        controlador.Move(direcaoNormalizada * velocidade * Time.deltaTime);
        transform.forward = Vector3.Lerp(transform.forward, direcaoNormalizada, Time.deltaTime * 5f);
        AtualizarAnimacao(true);

    }

    void EscolherNovoDestino()
    {
        Vector2 pontoAleatorio = Random.insideUnitCircle * raioDeMovimento;
        destinoAtual = new Vector3(transform.position.x + pontoAleatorio.x, transform.position.y, transform.position.z + pontoAleatorio.y);
    }

    void AplicarGravidade()
    {
        if (controlador.isGrounded && velocidadeVertical < 0)
            velocidadeVertical = -2f;

        velocidadeVertical += gravidade * Time.deltaTime;
        controlador.Move(Vector3.up * velocidadeVertical * Time.deltaTime);
    }


    void AtualizarAnimacao(bool estaAndando)
    {
        if (andando != estaAndando)
        {
            andando = estaAndando;
            if (animador != null) animador.SetBool("Andando", andando);
        }
    }

    bool JogadorEstaDentroDoRaioDeVisao()
    {
        if (jogador == null) return false;
        float dist = Vector3.Distance(transform.position, jogador.position);
        return dist <= raioDeVisao;
    }

    bool JogadorEhVisivel()
    {
        if (jogador == null) return false;

        Vector3 origem = transform.position + Vector3.up * eyeHeight;
        Vector3 destino = jogador.position + Vector3.up * 0.9f;
        Vector3 direcao = destino - origem;
        float distancia = direcao.magnitude;

        Debug.DrawRay(origem, direcao, Color.green);

        RaycastHit hit;
        if (Physics.Raycast(origem, direcao.normalized, out hit, distancia))
        {
            if(hit.collider.transform == jogador || hit.collider.CompareTag("Player"))
            {
                Debug.DrawRay(origem, direcao, Color.green);
                return true;
            }

            if (tagsQueBloqueiamVisao != null && tagsQueBloqueiamVisao.Length > 0)
            {
                foreach (string tag in tagsQueBloqueiamVisao)
                {
                    if (hit.collider.CompareTag(tag))
                    {
                        Debug.DrawRay(origem, direcao, Color.red);
                        Debug.Log("Visão bloqueado por: " + hit.collider.name + "com a tag: " + tag);
                        return false;
                    }    
                }
            }

            Debug.DrawRay(origem, direcao, Color.red);
            return hit.collider.transform == jogador || hit.collider.CompareTag("Player");
        }
        return true;
    }

    void AplicarDanoAoPlayer()
    {
        if (jogador == null) return;

        MissoesNoturnas missoes = jogador.GetComponent<MissoesNoturnas>();
        if (missoes != null)
        {
            missoes.PerderVida(danoAoPlayer);
        }
        else
        {
            jogador.SendMessage("AplicarDano", danoAoPlayer, SendMessageOptions.DontRequireReceiver);
        }
    }

    void ChecarAplicarDanoAoContato()
    {
        if (jogador == null) return;

        float distancia = Vector3.Distance(transform.position, jogador.position);
        if (distancia <= distanciaAtaque && Time.time - ultimoDanoTempo >= tempoEntreDanos)
        {
            AplicarDanoAoPlayer();
            ultimoDanoTempo = Time.time;
        }
    }

    #region Indicador de Visão (Esfera translúcida)
    void CriarIndicadorVisao()
    {
        esferaVisao = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        esferaVisao.name = "IndicadorVisao_" + gameObject.name;
        //esferaVisao.transform.SetParent(transform);

        //esferaVisao.transform.localPosition = new Vector3(0f, 0.05f, 0f);

        esferaVisao.transform.localPosition = new Vector3(0f,0.05f,0f);

        // remove o collider
        var col = esferaVisao.GetComponent<Collider>();
        if (col != null) Destroy(col);

        // material transparente
        Material mat = new Material(Shader.Find("Standard"));
        mat.color = new Color(1f, 0f, 0f, 0.5f); // vermelho quase invisível
        mat.SetFloat("_Mode", 3); // transparente
        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        mat.SetInt("_ZWrite", 0);
        mat.DisableKeyword("_ALPHATEST_ON");
        mat.EnableKeyword("_ALPHABLEND_ON");
        mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        mat.renderQueue = 3000;

        // força o refresh imediato para o Unity renderizar transparente
        Renderer rend = esferaVisao.GetComponent<Renderer>();
        rend.sharedMaterial = mat;
        rend.enabled = true;

        AtualizarIndicadorVisao();
    }

    void AtualizarIndicadorVisao()
    {
        if (esferaVisao != null)
        {
            float diametro = raioDeVisao * 2f;
            float espessura = 0.1f; // Altura do disco

            esferaVisao.transform.localScale = new Vector3(diametro, espessura, diametro);

            esferaVisao.SetActive(raioDeVisao > 0f);
        }
    }
    #endregion

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, raioDeMovimento);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, raioDeVisao);
    }
}
