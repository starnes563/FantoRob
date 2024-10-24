using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatilhoDialogo : MonoBehaviour
{
    [HideInInspector]
    public Diretor Diretor;
    public Dialogo MeuDialogo;
    public bool OnStart = false;
    private bool mostrou = false;   
    public CaixaDialogo CaixaDeDialogo;
    public string PosicaoJogador;
    public string BoolAnimator;
    public int NumeroDaCena;
    private Walk jogador;
    // Start is called before the first frame update
    void Start()
    {
        Diretor = GameObject.FindWithTag("MainCamera").GetComponent<Diretor>();
        if (CaixaDeDialogo == null)
        {
            CaixaDeDialogo = GameObject.FindWithTag("MainCamera").transform.GetChild(0).GetComponent<CaixaDialogo>();
        }
        MeuDialogo.LerOTexto(ManagerGame.Instance.Idm);
    }
    private void Update()
    {
        if(OnStart && !mostrou)
        {
            ComeçaDialogo();
        }
    }
    public void ComeçaDialogo()
    {        
        if (Diretor.PodeIniciar && PlayerStatus.ControleDeCena == NumeroDaCena)
        {
            if(jogador == null)
            {
                jogador = GameObject.FindWithTag("Player").GetComponent<Walk>();
            }           
            jogador.PararDeAndar();
            if (PosicaoJogador != "") {
                Animator anim = GameObject.FindWithTag("Player").GetComponent<Animator>();
                anim.SetBool("Frente", false);
                anim.SetBool("Costas", false);
                anim.SetBool("Direita", false);
                anim.SetBool("Esquerda", false);
                anim.SetBool(BoolAnimator, true);
                anim.Play(PosicaoJogador);               
            }
            Diretor.DesativarMenuPlayer();
            CaixaDeDialogo.ReceberDialogo(MeuDialogo);
            mostrou = true;
            PlayerStatus.ControleDeCena++;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!OnStart && collision.tag == "Player" && !mostrou)
        {
            jogador = collision.GetComponent<Walk>();
            ComeçaDialogo();           
        }
    }

}
