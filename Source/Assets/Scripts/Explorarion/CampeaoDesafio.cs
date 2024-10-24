using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampeaoDesafio : NPCBattle
{
    enum EstadoNpc
    {
        NAOATACOU,
        ATACOU,
        BATALHOU,
        RECLAMOU,
        TOCOUMUSICA,
    }
    EstadoNpc MeuEstado;
    public Dialogo FalaAtacar;
    public Dialogo FalaDepoisDeAtacar;
    private Animator Anim;
    public int MinhaEstrela;
    public int MinhaCena;
    public GameObject Gatilhos;
    // Start is called before the first frame update
    void Start()
    {       
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
                PorEmAtivos(rob.MovimentoAmbos, rob.Fisico);
                PorEmAtivos(rob.MovimentoInimigo, rob.Fisico);
                PorEmAtivos(rob.Fisico.MovimentosAmbos, rob.Fisico);
                PorEmAtivos(rob.Fisico.MovimentosInimigo, rob.Fisico);
                for (int i = 0; i < Random.Range(2, 5); i++)
                {
                    int nivelmv = 1;
                    switch (Dificuldade)
                    {
                        case 0:
                            nivelmv = 1;
                            break;
                        case 1:
                            nivelmv = Random.Range(1, 2);
                            break;
                        case 2:
                            nivelmv = Random.Range(1, 2);
                            break;
                        case 3:
                            nivelmv = Random.Range(1, 3);
                            break;
                        case 4:
                            nivelmv = Random.Range(1, 3);
                            break;
                        case 5:
                            nivelmv = Random.Range(2, 4);
                            break;
                        case 6:
                            nivelmv = Random.Range(2, 3);
                            break;
                        case 7:
                            nivelmv = 4;
                            break;
                        case 8:
                            nivelmv = 4;
                            break;
                        case 9:
                            nivelmv = 4;
                            break;
                        case 10:
                            nivelmv = 4;
                            break;
                    }
                    rob.Fisico.MovesAtivos.Add(Instantiate(Constructor.MoveConstructor(nivelmv)));
                }
                //retira ate ficar igua o attacksmax e nao dar erro na hora de carregar o ataque
                while (rob.Fisico.MovesAtivos.Count > rob.Fisico.AttacksMax)
                {
                    rob.Fisico.MovesAtivos.RemoveAt(Random.Range(0, rob.Fisico.MovesAtivos.Count));
                }
                //conferir se tem pelo menos um ataque de uma ação
                bool overflowrisk = true;
                foreach (Move mv in rob.Fisico.MovesAtivos)
                {
                    if (mv != null)
                    {
                        if (mv.UsoDeAcoes == 1) { overflowrisk = false; break; }
                    }
                }
                if (overflowrisk)
                {
                    rob.Fisico.MovesAtivos.RemoveAt(Random.Range(0, rob.Fisico.MovesAtivos.Count));
                    rob.Fisico.MovesAtivos.Add(Instantiate(Constructor.MoveConstructor(1)));
                }
                //carrega os ataque
                rob.Fisico.CarregarAtaques();
                //criar combo
                if (Dificuldade < 3 && rob.Fisico.Model == 5)
                {
                    rob.Fisico.MontarArma(rob.Fisico.AttacksMax, Random.Range(3, 4));
                }
                else if (Dificuldade >= 3)
                {
                    if (rob.Fisico.Model == 5)
                    {
                        rob.Fisico.MontarArma(rob.Fisico.AttacksMax, Random.Range(3, 8));
                    }
                    else
                    {
                        rob.Fisico.MontarArma(rob.Fisico.AttacksMax, Random.Range(3, 7));
                    }
                }
            
            rob.Integridade = calcularStatus(rob.IntegridadeMinima, rob.IntegridadeMaxima);
                //rob.Integridade = 5;
                rob.Resistencia = calcularStatus(rob.ResistenciaMinima, rob.ResistenciaMaxima);
               // rob.Resistencia = 2;
                rob.Ataque = calcularStatus(rob.AtaqueMinimo, rob.AtaqueMaximo);
                rob.AtaqueElemental = calcularStatus(rob.AtaqueElementalMinimo, rob.AtaqueElementalMaximo);
                rob.Velocidade = calcularStatus(rob.VelocidadeMinima, rob.VelocidadeMaxima);
                rob.IntegridadeAtual = rob.Integridade;
            }      
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (MeuEstado == EstadoNpc.ATACOU && !CaixaDialogo.gameObject.activeSelf && !ManagerGame.Instance.EmBatalha)
        {
            ManagerGame.Instance.StartBattle(Tipo, this);
            ManagerGame.Instance.GanhouBatalha += DarEstrela;
            ManagerGame.Instance.PerdeuBatalha += FimBatalha;
            MeuEstado = EstadoNpc.BATALHOU;
        }
        if (MeuEstado == EstadoNpc.BATALHOU && !CaixaDialogo.gameObject.activeSelf && !ManagerGame.Instance.EmBatalha)
        {            
            CaixaDialogo.ReceberDialogo(FalaDepoisDeAtacar);
            MeuEstado = EstadoNpc.RECLAMOU;
            GameObject.FindWithTag("Player").GetComponent<Walk>().PararDeAndar();
        }
        if (MeuEstado == EstadoNpc.RECLAMOU && !CaixaDialogo.gameObject.activeSelf && !ManagerGame.Instance.EmBatalha)
        {
            Gatilhos.SetActive(false);
            GetComponent<VenceuCampeao>().Tocar();
            MeuEstado = EstadoNpc.TOCOUMUSICA;           
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
        if (!ManagerGame.Instance.EmBatalha)
        {
            Anim.Play(animacao);
        }
    }
    int calcularStatus(int minimo, int maximo)
    {
        int st;
        float ale = 70 / 100;
        st = Mathf.RoundToInt(minimo + ((maximo - minimo) * ale));
        return st;
    }
    void DarEstrela()
    {
        if (PlayerStatus.Estrelas == MinhaEstrela - 1)
        {
            GetComponent<VenceuCampeao>().primeiravez = false;
            PlayerStatus.Estrelas = MinhaEstrela;
        }
        if (PlayerStatus.ControleDeCena == MinhaCena)
        {
            PlayerStatus.ControleDeCena = MinhaCena+1;           
        }
        
        FimBatalha();
    }
    void FimBatalha()
    {
        ManagerGame.Instance.GanhouBatalha -= DarEstrela;
        ManagerGame.Instance.PerdeuBatalha -= FimBatalha;
        if (StoryEvents.FalhouEsfihas) { StoryEvents.FalhouEsfihas = false; }
    }   

}
