using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FantoMascaraFugitivo : NPCBattle
{
    public int ID;
    public bool Primeiro;
    //this script handles all the movement, and animations that changes de sprite renderer.
    public float FPS = 10;
    public float Speed = 5;
    //0-FRENTE
    //1-DIRETA
    //2-COSTAS
    //3-ESQUERDA
    public LoopSpriteAnimation[] Movimentando = new LoopSpriteAnimation[4];
    public LoopSpriteAnimation[] Idle = new LoopSpriteAnimation[4];
    Vector2 movement;
    
    [HideInInspector]
    public int actualCicle = 0;
    IEnumerator actualCicleCoroutine;
    [Range(-1, 1)]
    public int XInicial = 0;
    [Range(-1, 1)]
    public int YInicial = 0;
    [HideInInspector]
    public int X = 0;   
    [HideInInspector]
    public int Y = 0;    
    enum EstadoNpc
    {
        NAOATACOU,
        ATACOU,
        JOGGANHOU,
        JOGPERDEU,
        FUGINDO,
    }
    EstadoNpc MeuEstado;
    public Dialogo FalaAtacar;
    public Dialogo FalaFugir;
    public Dialogo FalaFicar;
    Walk player;
    public MusicaFugirFanto MinhaCaixa;
    bool acionoumusica;
    enum MoveState
    {
        IDLE,
        MOVENDO
    }
    MoveState MyState;
    // Start is called before the first frame update
    void Start()
    {
        if(StoryEvents.DesafiosCamp[4].Interagiveis[ID])
        {
            this.gameObject.SetActive(false);
        }
        else if(!Primeiro)
        {
            if(!StoryEvents.DesafiosCamp[4].Interagiveis[ID-1])
            {
                this.gameObject.SetActive(false);
            }
        }
        SpriteRenderer spRender = GetComponent<SpriteRenderer>();
        foreach (LoopSpriteAnimation anim in Movimentando)
        {
            anim.SetSpriteRenderer(spRender, FPS);
        }
        foreach (LoopSpriteAnimation anim in Idle)
        {
            anim.SetSpriteRenderer(spRender, FPS);
        }
        Rb = GetComponent<Rigidbody2D>();
        MyState = MoveState.IDLE;
        preparaBatalha();
        FalaAtacar.LerOTexto(ManagerGame.Instance.Idm);
        FalaFugir.LerOTexto(ManagerGame.Instance.Idm);
        FalaFicar.LerOTexto(ManagerGame.Instance.Idm);

    }

    // Update is called once per frame
    void Update()
    {
        switch (MyState)
        {
            case MoveState.MOVENDO:
                
                moveCalc();               
                break;
            case MoveState.IDLE:
                
                moveCalc();
                break;
        }
        if (MeuEstado == EstadoNpc.ATACOU && !CaixaDialogo.gameObject.activeSelf && !ManagerGame.Instance.EmBatalha)
        {
            ManagerGame.Instance.StartBattle(Tipo, this);
            ManagerGame.Instance.GanhouBatalha += PlayerGanhou;
            ManagerGame.Instance.PerdeuBatalha += PlayerPerdeu;            
        }
        if (MeuEstado == EstadoNpc.JOGGANHOU && !CaixaDialogo.gameObject.activeSelf && !ManagerGame.Instance.EmBatalha)
        {
            CaixaDialogo.ReceberDialogo(FalaFugir);
            player.PararDeAndar();
            MeuEstado = EstadoNpc.FUGINDO;            
        }
        if (MeuEstado == EstadoNpc.JOGPERDEU && !CaixaDialogo.gameObject.activeSelf && !ManagerGame.Instance.EmBatalha)
        {
            player.PararDeAndar();
            CaixaDialogo.ReceberDialogo(FalaFicar);           
        }
        if (MeuEstado == EstadoNpc.FUGINDO && !CaixaDialogo.gameObject.activeSelf && !ManagerGame.Instance.EmBatalha)
        {
            X = XInicial;
            Y = YInicial;
            if(!acionoumusica)
            {
                acionoumusica = true;
                MinhaCaixa.Acionar();
            }
            
        }
    }
    public override void Falar(Walk walk)
    {
        walk.PararDeAndar();
        player = walk;
        if (CaixaDialogo != null && this.Batalha && !ManagerGame.Instance.EmBatalha)
        {
            if (MeuEstado == EstadoNpc.JOGPERDEU)
            {
                CaixaDialogo.ReceberDialogo(FalaFicar);
            }
            else
            {
                CaixaDialogo.ReceberDialogo(FalaAtacar);
                MeuEstado = EstadoNpc.ATACOU;
                StoryEvents.DesafiosCamp[4].Interagiveis[ID] = true;
            }
        }
    }
    void preparaBatalha()
    {
        CaixaDialogo = GameObject.FindWithTag("MainCamera").transform.GetChild(0).GetComponent<CaixaDialogo>();
        if (TextoFala != null)
        {
            TextoFala.LerOTexto(ManagerGame.Instance.Idm);
            setencas = TextoFala.Sentencas.Count;
        }

        Rb = GetComponent<Rigidbody2D>();
        if (Robots.Count > 0)
        {
            if (GerarAtaques)
            {
                foreach (FantoRob rob in Robots)
                {
                    PorEmAtivos(rob.MovimentoAmbos, rob.Fisico);
                    PorEmAtivos(rob.MovimentoInimigo, rob.Fisico);
                    PorEmAtivos(rob.Fisico.MovimentosAmbos, rob.Fisico);
                    PorEmAtivos(rob.Fisico.MovimentosInimigo, rob.Fisico);
                    for (int i = 0; i < Random.Range(1, 4); i++)
                    {
                        int nivelmv = 1;
                        switch (PlayerStatus.Estrelas)
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
                        }
                        rob.Fisico.MovesAtivos.Add(Instantiate(Constructor.MoveConstructor(nivelmv)));
                    }
                    //retira ate ficar igua o attacksmax e nao dar erro na hora de carregar o ataque
                    while (rob.Fisico.MovesAtivos.Count > rob.Fisico.AttacksMax)
                    {
                        rob.Fisico.MovesAtivos.RemoveAt(Random.Range(0, rob.Fisico.MovesAtivos.Count));
                    }
                    //carrega os ataque
                    rob.Fisico.CarregarAtaques();
                    //criar combo
                    if (PlayerStatus.Estrelas < 3 && rob.Fisico.Model == 5)
                    {
                        rob.Fisico.MontarArma(rob.Fisico.AttacksMax, Random.Range(3, 4));
                    }
                    else if (PlayerStatus.Estrelas >= 3)
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
                }
            }
            else
            {
                foreach (FantoRob rob in Robots)
                {
                    if (rob != null)
                    {
                        rob.Fisico.CarregarAtaques();
                    }
                }
            }
        }
    }
    void FixedUpdate()
    {
        Rb.MovePosition(Rb.position + movement * Speed * Time.fixedDeltaTime);
    }    
    public void moveCalc()
    {
        movement.x = X;
        movement.y = Y;
        movement = movement.normalized;
        if (movement.sqrMagnitude > 0.001f)
        {
            loopBlendTree(Movimentando);
        }
        else if (MyState != MoveState.IDLE)
        {
            if (actualCicleCoroutine != null) { StopCoroutine(actualCicleCoroutine); }
            actualCicleCoroutine = Idle[actualCicle].Animate();
            StartCoroutine(actualCicleCoroutine);         
            MyState = MoveState.IDLE;
        }

    }
    void loopBlendTree(LoopSpriteAnimation[] cicle)
    {
        int cicleCorroutine = 0;
        if (movement.y < -0.001f) { cicleCorroutine = 0; }
        if (movement.x > 0.001f) { cicleCorroutine = 1; }
        if (movement.y > 0.001f) { cicleCorroutine = 2; }
        if (movement.x < -0.001f) { cicleCorroutine = 3; }
        if (actualCicle != cicleCorroutine && MyState == MoveState.MOVENDO || MyState != MoveState.MOVENDO)
        {
            if (actualCicleCoroutine != null) { StopCoroutine(actualCicleCoroutine); }
            actualCicle = cicleCorroutine;
            actualCicleCoroutine = cicle[actualCicle].Animate();
            StartCoroutine(actualCicleCoroutine);
            MyState = MoveState.MOVENDO;           
           
        }
    }   
    void PlayerGanhou()
    {
        MeuEstado = EstadoNpc.JOGGANHOU;
        ManagerGame.Instance.GanhouBatalha -= PlayerGanhou;
        ManagerGame.Instance.PerdeuBatalha -= PlayerPerdeu;
    }
    void PlayerPerdeu()
    {
        MeuEstado = EstadoNpc.JOGPERDEU;
        ManagerGame.Instance.GanhouBatalha -= PlayerGanhou;
        ManagerGame.Instance.PerdeuBatalha -= PlayerPerdeu;
    }
}
