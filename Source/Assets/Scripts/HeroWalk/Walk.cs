using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour
{
    public float Velocidade = 2;
    private Animator animator;
  // [HideInInspector]
    public bool CanIWalk = false;
    public bool PlayerCanFight = true;
    [HideInInspector]
    public GameObject playerMenu;
    [HideInInspector]
    public GameObject Canvas;
    Vector2 movimento;
    Rigidbody2D Rb;
    float movimentoy;
    float movimentox;
    public List<Quest> MissoesAtuais;
    public AudioSource AudioSource;
    public AudioClip SomAndando;
    public int RoupaNeftari;
    public bool PossoComandar = true;
    private Vector3 proximaposicao;
    private float proximadistancia;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Canvas = GameObject.FindWithTag("MainCamera").transform.GetChild(1).gameObject;
        playerMenu = Canvas.transform.GetChild(1).transform.gameObject;
        Rb = GetComponent<Rigidbody2D>();        
    }
    // Update is called once per frame
    void Update()
    {
        if (!ManagerGame.Instance.EmBatalha)
        {
            if (CanIWalk)
            {                
                if (Input.GetButtonDown("Jump"))
                {
                    abrirmenu();
                }

                if (PossoComandar)
                {
                    movimento.x = Input.GetAxisRaw("Horizontal");
                    movimento.y = Input.GetAxisRaw("Vertical");
                    movimento = movimento.normalized;
                }
                else
                {
                    if (!indoPosicao())
                    {
                        movimento = Vector2.zero; PossoComandar = true;
                    }

                }

                animator.SetFloat("VelocidadeX", movimento.x);
                animator.SetFloat("VelocidadeY", movimento.y);
                animator.SetFloat("Speed", movimento.sqrMagnitude);
                if (movimento.sqrMagnitude > 0 && !AudioSource.isPlaying)
                {
                    AudioSource.PlayOneShot(SomAndando);
                }

                if (movimento.sqrMagnitude < 0.1f && AudioSource.isPlaying)
                {
                    AudioSource.Stop();
                }
                //calcula o idle
                if (movimento.y < 0) { movimentoy = movimento.y * -1; }
                else { movimentoy = movimento.y; }
                if (movimento.x < 0) { movimentox = movimento.x * -1; }
                else { movimentox = movimento.x; }

                if (movimentoy > movimentox)
                {
                    animator.SetBool("Costas", false);
                    animator.SetBool("Frente", false);
                    animator.SetBool("Direita", false);
                    animator.SetBool("Esquerda", false);

                    if (movimento.y > 0)
                    {
                        animator.SetBool("Costas", true);
                        animator.SetBool("Frente", false);
                    }
                    else
                    {
                        animator.SetBool("Costas", false);
                        animator.SetBool("Frente", true);
                    }
                }
                else if (movimentoy < movimentox)
                {
                    animator.SetBool("Costas", false);
                    animator.SetBool("Frente", false);
                    animator.SetBool("Direita", false);
                    animator.SetBool("Esquerda", false);
                    if (movimento.x > 0)
                    {
                        animator.SetBool("Direita", true);
                        animator.SetBool("Esquerda", false);
                    }
                    else
                    {
                        animator.SetBool("Direita", false);
                        animator.SetBool("Esquerda", true);
                    }
                }
            }
        }
    }
    private void FixedUpdate()
    {
        if(!CanIWalk)
        {
            parar();
        }
        Rb.MovePosition(Rb.position + movimento * Velocidade * Time.fixedDeltaTime);        
    }
    public void PararDeAndar()
    {        
        CanIWalk = false;        
    }
    void parar()
    {
        if (movimento.y < 0) { movimentoy = movimento.y * -1; }
        else { movimentoy = movimento.y; }
        if (movimento.x < 0) { movimentox = movimento.x * -1; }
        else { movimentox = movimento.x; }
        if (movimentoy > movimentox)
        {
            animator.SetBool("Costas", false);
            animator.SetBool("Frente", false);
            animator.SetBool("Direita", false);
            animator.SetBool("Esquerda", false);

            if (movimento.y > 0)
            {
                animator.SetBool("Costas", true);
                animator.SetBool("Frente", false);
            }
            else
            {
                animator.SetBool("Costas", false);
                animator.SetBool("Frente", true);
            }
        }
        else if (movimentoy < movimentox)
        {
            animator.SetBool("Costas", false);
            animator.SetBool("Frente", false);
            animator.SetBool("Direita", false);
            animator.SetBool("Esquerda", false);
            if (movimento.x > 0)
            {
                animator.SetBool("Direita", true);
                animator.SetBool("Esquerda", false);
            }
            else
            {
                animator.SetBool("Direita", false);
                animator.SetBool("Esquerda", true);
            }
        }
        movimento = Vector2.zero;
        animator.SetFloat("VelocidadeX", movimento.x);
        animator.SetFloat("VelocidadeY", movimento.y);
        animator.SetFloat("Speed", movimento.sqrMagnitude);
        AudioSource.Stop();

    }
    void abrirmenu()
    {
        PararDeAndar();
        Canvas.SetActive(true);
        SonsMenu.Confimar();
        playerMenu.GetComponent<PlayerMenu>().Active = true;
        playerMenu.GetComponent<PlayerMenu>().MontarQuadroJogador();
        playerMenu.GetComponent<PlayerMenu>().playerObjects = GetComponent<PlayerObjects>();
        playerMenu.GetComponent<PlayerMenu>().player = this;        
    }
    public void FecharMenu()
    {
        Canvas.SetActive(false);
        playerMenu.GetComponent<PlayerMenu>().Active = false;
        Time.timeScale = 1f;
        CanIWalk = true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Gateway")
        {           
            CollisonHandler col = other.GetComponent<CollisonHandler>();
            PlayerStatus.NextHeroPosition = col.SpawnPoint.transform.position;
            ManagerGame.Instance.SceneToLoad = col.SceneToLoad;
            //posicao            
            PlayerStatus.ProximaAnimacao = col.PosicaoDoAnimatorProximaCena;               
            PararDeAndar();
            animator.Rebind();
            animator.Play(col.PosicaoAnimatorCenaAtual);            
            //Amigos
            if (PlayerStatus.MarcusV != null && PlayerStatus.MarcusV.activeSelf)
            {

                PlayerStatus.MarcusAtivo = true;
            }
            else
            {
                PlayerStatus.MarcusAtivo = false;
            }
            if (PlayerStatus.Luiza != null && PlayerStatus.Luiza.activeSelf)
            {

                PlayerStatus.LuizaAtiva = true;
            }
            else
            {
                PlayerStatus.LuizaAtiva = false;
            }
            Diretor.DesativarMenuPlayer();
           GameObject.FindWithTag("MainCamera").GetComponent<Diretor>().TrocarACena();
        }
    }
    public void AndarParaEssaPosicao(Vector3 posicao, float distancia)
    {
        proximaposicao = posicao;
        proximadistancia = distancia;
        PossoComandar = false;
    }
    bool indoPosicao()
    {
        movimento = proximaposicao - this.transform.position;
        return (Vector3.Distance(proximaposicao, this.transform.position) > 0.5);
    }
    public void LiberarAndar()
    {
        CanIWalk = true;
    }
    public void CurarParty()
    {
        foreach (FantoRob fanto in PlayerObjects.RobotsInUse)
        {
            if (fanto != null)
            {
                fanto.Curartudo();
            }
        }
    }
}
