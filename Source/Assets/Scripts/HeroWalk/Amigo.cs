using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amigo : MonoBehaviour
{
    private GameObject neftari;   
    public bool seguir = false;
    public int Velocidade = 2;
    Rigidbody2D Rb;
    private Vector2 movimento;
    public float Distancia;
    private CaixaDialogo CaixaDialogo;
    [HideInInspector]
    public Dialogo TextoFala;
    public List<Dialogo> Dialogos;
    public List<int> InEscolhaDialogo;
    private Animator animator;
    float movimentoy;
    float movimentox;
    bool andei = false;  
    // Start is called before the first frame update
    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        CaixaDialogo = GameObject.FindWithTag("MainCamera").transform.GetChild(0).GetComponent<CaixaDialogo>();
        SelecionarTextoFala();        
        animator = GetComponent<Animator>();
        if (ManagerGame.Instance.HeroAtual!=null)
        {
            neftari = ManagerGame.Instance.HeroAtual;
            seguir = true;
        }        
    }
    // Update is called once per frame
    void Update()
    {
        if (!ManagerGame.Instance.EmBatalha)
        {
            float dist = Vector2.Distance(neftari.transform.position, this.transform.position);
            if (dist >= Distancia && seguir)
            {
                movimento = neftari.transform.position - this.transform.position;
                animator.SetFloat("VelocidadeX", movimento.x);
                animator.SetFloat("VelocidadeY", movimento.y);
                animator.SetFloat("Speed", movimento.sqrMagnitude);
                andei = true;
            }
            else if (andei)
            {
                //calcula o idle
                if (movimento.y < 0) { movimentoy = movimento.y * -1; }
                else { movimentoy = movimento.y; }
                if (movimento.x < 0) { movimentox = movimento.x * -1; }
                else { movimentox = movimento.x; }

                if (movimentoy > movimentox)
                {
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
                else
                {
                    animator.SetBool("Costas", false);
                    animator.SetBool("Frente", false);

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
                animator.SetFloat("Speed", 0);
            }

        }
    }
    private void FixedUpdate()
    {
        if (seguir)
        {           
            Rb.MovePosition(Rb.position + movimento.normalized * Velocidade * Time.fixedDeltaTime);
        }
    }
    public void Falar(Walk walk)
    {
        SelecionarTextoFala();
        if (TextoFala != null && CaixaDialogo != null)
        {
            walk.PararDeAndar();
            CaixaDialogo.ReceberDialogo(TextoFala);
        }        
    }
    public void SelecionarTextoFala()
    {
        foreach (Dialogo fala in Dialogos)
        {
            if (InEscolhaDialogo[Dialogos.IndexOf(fala)] <= PlayerStatus.ControleDeCena)
            {
                TextoFala = fala;
                
            }
            else
            {
                break;
            }
        }
        if(TextoFala != null)
        {
            TextoFala.LerOTexto(ManagerGame.Instance.Idm);
        }
        
    }
    public void TocarAnimacao(string animacao)
    {
        animator.Play(animacao);
    }
}
