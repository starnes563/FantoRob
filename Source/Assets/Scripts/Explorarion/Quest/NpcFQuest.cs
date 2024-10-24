using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcFQuest : NPCBattle
{    
   enum EstadoNpc
    {
        NAOATACOU,
        ATACOU,
        BATALHOU,
        RECLAMOU,
        PERDEU,
   }
    EstadoNpc MeuEstado;
    public Dialogo FalaAtacar;
    public Dialogo FalaDepoisDeAtacar;
    private Animator Anim;
    public int ID;
    // Start is called before the first frame update
    void Start()
    {        
        if (PlayerObjects.Missões != null && PlayerObjects.Missões.Count>0)
        {                            
            if (!StoryEvents.PrimeiraMissao[ID])            
            {                
                this.gameObject.SetActive(false);
                MeuEstado = EstadoNpc.NAOATACOU;
            }
            else
            {
                GameObject.FindWithTag("MainCamera").GetComponent<Diretor>().MinhaSetaGPS.Ativar(this.transform);
            }

        }
        else
        {
            this.gameObject.SetActive(false);
        }
        Anim = GetComponent<Animator>();
        CaixaDialogo = GameObject.FindWithTag("MainCamera").transform.GetChild(0).GetComponent<CaixaDialogo>();
       
        if (FalaAtacar != null)
        {
            FalaAtacar.LerOTexto(ManagerGame.Instance.Idm);
        }
        if (FalaDepoisDeAtacar != null)
        {
            FalaDepoisDeAtacar.LerOTexto(ManagerGame.Instance.Idm);
        }
        if (Robots.Count > 0)
        {
            foreach (FantoRob rob in Robots)
            {
                rob.Fisico.CarregarAtaques();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(MeuEstado == EstadoNpc.ATACOU && !CaixaDialogo.gameObject.activeSelf && !ManagerGame.Instance.EmBatalha)
        {           
            ManagerGame.Instance.GanhouBatalha += PerderBatalha;
            ManagerGame.Instance.StartBattle(Tipo, this);
            MeuEstado = EstadoNpc.BATALHOU;
        }
        if(MeuEstado == EstadoNpc.PERDEU && !CaixaDialogo.gameObject.activeSelf && !ManagerGame.Instance.EmBatalha)
        {                       
            CaixaDialogo.ReceberDialogo(FalaDepoisDeAtacar);
            MeuEstado = EstadoNpc.RECLAMOU;
            GameObject.FindWithTag("MainCamera").GetComponent<Diretor>().MinhaSetaGPS.gameObject.SetActive(false);
        }
    }
    public override void Falar(Walk walk)
    {
        walk.PararDeAndar();
        if (CaixaDialogo != null && this.Batalha && !ManagerGame.Instance.EmBatalha)
        {
            if (MeuEstado == EstadoNpc.RECLAMOU)
            {
                CaixaDialogo.ReceberDialogo(FalaDepoisDeAtacar);
                GameObject.FindWithTag("MainCamera").GetComponent<Diretor>().MinhaSetaGPS.gameObject.SetActive(false);
            }
            else
            {
                CaixaDialogo.ReceberDialogo(FalaAtacar);
                MeuEstado = EstadoNpc.ATACOU;
            }

        }
    }
    public override void TocarAnimacao(string animacao)
    {
        if(!ManagerGame.Instance.EmBatalha)
        {
            Anim.Play(animacao);
        }        
    }
    public void PerderBatalha()
    {        
        ManagerGame.Instance.GanhouBatalha -= PerderBatalha;
        MeuEstado = EstadoNpc.PERDEU;
        StoryEvents.PrimeiraMissao[ID] = false;
    }
}

