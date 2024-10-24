using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBattle : MonoBehaviour
{   
    public List<FantoRob> Robots = new List<FantoRob>();
    public int Tipo;
    //Loot
    public int Exp;
    public int Trend;
    public int Money;
    public bool Persegue;
    public bool Batalha;
    public bool GerarAtaques = false;   
    public List<string> Nome;
    [HideInInspector]
    public Dialogo TextoFala;
    public List<Dialogo> Dialogos;
    public List<int> InEscolhaDialogo;
    public int setencas;
    [HideInInspector]
    public CaixaDialogo CaixaDialogo;
    [HideInInspector]
    public Fala CaixaFala;
    [HideInInspector]
    public GameObject Player;    
    public GameObject Exclamacao;
    private float Distance;
    private Vector2 movimento;
    public float Velocidade;
    [HideInInspector]
    public Rigidbody2D Rb;
    [HideInInspector]
    public Animator animator;    
    float movimentoy;
    float movimentox;
    private float contador = 5;
    private float tempoEntrePosicoes = 5;
    private float tempoandando = 1;
    private bool podeandar = false;
    private bool instanciouExc = false;
    public int Dificuldade = 0;
    private bool JaAtacou = false;
    private bool falarAntesAtacar = false;
    public List<Quest> Missoes;
    //Variaveis para  o online
    [HideInInspector]
    public Sprite MeuSp;
    [HideInInspector]
    public string Name;
    [HideInInspector]
    public int Estrelas;
    [HideInInspector]
    public int Nivel;
    [HideInInspector]
    public bool GanhouAtaque = false;
    public bool Cutscene = false;
    private bool atacou = false;
    void Start()
    {
        animator = GetComponent<Animator>();
        CaixaDialogo = GameObject.FindWithTag("MainCamera").transform.GetChild(0).GetComponent<CaixaDialogo>();       
        Player = GameObject.FindWithTag("Player");
        SelecionarTextoFala();
        if(TextoFala != null)
        {
            TextoFala.LerOTexto(ManagerGame.Instance.Idm);
            setencas = TextoFala.Sentencas.Count;
        }       
       
        Rb = GetComponent<Rigidbody2D>();
        if(Robots.Count>0)
        {
            if (GerarAtaques)
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
                    foreach(Move mv in rob.Fisico.MovesAtivos)
                    {
                        if(mv!=null)
                        {
                            if(mv.UsoDeAcoes == 1) { overflowrisk = false; break; }
                        }
                    }
                    if(overflowrisk)
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
                }
            }
            else
            {
                foreach (FantoRob rob in Robots)
                {
                    if(rob !=null)
                    {
                        rob.Fisico.CarregarAtaques();
                    }                   
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!ManagerGame.Instance.EmBatalha && !CaixaDialogo.gameObject.activeSelf && !Cutscene)
        {
            if (Persegue && !JaAtacou)
            {
                Distance = Vector2.Distance(Player.transform.position, transform.position);
                if (Distance > 8f)
                {
                    AndarAleatoriamente();
                }
                else if (Distance < 8f && Distance > 3f)
                {
                    Perseguir();
                }
                else if (Distance < 2.5f)
                {
                    Attack();
                }
                animator.SetFloat("VelocidadeX", movimento.x);
                animator.SetFloat("VelocidadeY", movimento.y);
                animator.SetFloat("Speed", movimento.sqrMagnitude);

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
            if (atacou && !CaixaDialogo.gameObject.activeSelf && !ManagerGame.Instance.EmBatalha)
            {
                ManagerGame.Instance.StartBattle(Tipo, this);
                atacou = false;
            }
        }
        else if (movimento.sqrMagnitude > 0) { movimento = Vector2.zero; }
    }
    private void FixedUpdate()
    {
        if (Persegue && !JaAtacou)
        {
            Rb.MovePosition(Rb.position + movimento.normalized * Velocidade * Time.fixedDeltaTime);
        }        
    }
    private void SelecionarTextoFala()
    {
        if (!Cutscene)
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
        }
    }
    public virtual void Falar(Walk walk)
    {       
        if (!Cutscene)
        {
            Diretor.DesativarMenuPlayer();
            walk.PararDeAndar();
            if (CaixaDialogo != null)
            {                
               // Diretor.Instance.CaixaDialogo.ReceberDialogo(TextoFala);
               CaixaDialogo.ReceberDialogo(TextoFala);
                if (Batalha) { atacou = true; }
            }
            else
            {
                CaixaFala = GameObject.FindWithTag("MainCamera").transform.GetChild(0).GetComponent<Fala>();
                CaixaFala.DigitarNaTela("Bom dia.", "???");
                walk.LiberarAndar();
            }
        }
       
    }    
   
    void AndarAleatoriamente()
    {
        contador += Time.deltaTime;
        if(contador > tempoEntrePosicoes)
        {
            movimento = Random.insideUnitSphere;
            contador = 0;
            tempoandando = 0;
        }              
        if(tempoandando<0.5f)
        {
            tempoandando += Time.deltaTime;
            podeandar = true;
        }
        if(tempoandando>0.5f && podeandar)
        {
            movimento = new Vector2(0, 0);
            podeandar = false;
        }
    }
    void Perseguir()
    {        
        if(!instanciouExc)
        {
            instanciouExc = true;           
            Instantiate(Exclamacao,this.transform);
        }
        movimento = Player.transform.position - this.transform.position;        
    }
    public void Attack()
    {
        movimento = Vector2.zero;
        Diretor.DesativarMenuPlayer();
            if (!Cutscene)
            {           
            if (!falarAntesAtacar) { Falar(Player.GetComponent<Walk>()); falarAntesAtacar = true; }
                else { ManagerGame.Instance.StartBattle(Tipo, this); }
            }
            else
            {            
            ManagerGame.Instance.StartBattle(Tipo, this);
        }       
    }
    public virtual void TocarAnimacao(string animacao)
    {
        animator.Play(animacao);
    }
    public void DarMissao(int numero)
    {
        Missoes[numero].DarMissao();
    }
    public void PorEmAtivos(List<Move> m, Weapon f)
    {
        if (m != null && m.Count > 0)
        {            
            if (m.Count >= 2)
            {
               f.MovesAtivos.Add(m[m.Count - 2]);
            }
            if (m.Count >= 1)
            {
               f.MovesAtivos.Add(m[m.Count - 1]);
            }


        }
    }

}
