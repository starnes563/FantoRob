using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponMethods : MonoBehaviour
{     
    [HideInInspector]
    public BattleManager battleManager;
    [HideInInspector]
    public ManDiferenciado Diferenciado;
    [HideInInspector]
    public RobotManager myRobot;
    [HideInInspector]
    public Status status;
    [HideInInspector]
    public GerenciadorDialogo gerenciadorDialogo;
    [HideInInspector]
    public TextoDeBatalha textoDeBatalha;
    public GameObject AnimacaoEspecial;
    // ataques elementais usam o nucleo na hora de calcular, mas diminuem menos a resistencia. false = não tem, true = tem
    // controle de combos tentar transformar isso em um array;
    // 0 - representa que o ataque não foi realizado no turno
    private int[] moves = new int[6];
    //battle manager acessa essa variavel apra ligar e desligar os turnos
    [HideInInspector]
    public bool animacaoexecutando = false;

    private float multiplicadorfora = 1;
    [HideInInspector]
    public float regitrarAumentoEnergia = 0;
    [HideInInspector]
    public AnimationController RobotAnimator;
    //essa sequencia de variaveis servem pra serem lidas pela IA e montar os combos
    [HideInInspector]
    public Weapon weapon;
    private bool animacaoEspecial = false;
    [HideInInspector]
    public Cortante MeuCortante = new Cortante();
    [HideInInspector]
    public Impacto MeuImapcto = new Impacto();
    [HideInInspector]
    public Escudo MeuEscudo = new Escudo();    
    [HideInInspector]
    public Bolha MinhaBolha = new Bolha();
    [HideInInspector]
    public canhao MeuCanhao = new canhao();
    [HideInInspector]
    public Cajado MeuCajado = new Cajado();
    [HideInInspector]
    public event Action Combo;
    IEnumerator BarraEnergiaEscudo;
    IEnumerator BarraPoderCajado;
    bool iniciou = false;
    // Start is called before the first frame update
    void Start()
    {
        ReiniciaCombo();        
        myRobot = GetComponent<RobotManager>();
        status = myRobot.MyStatus;
        RobotAnimator = myRobot.RobotAnimator;
        weapon = status.NucleoFisico;
        IniciarArma();
        iniciou = true;
    }
    private void OnEnable()
    {       
        if(iniciou)
        {
            IniciarArma();
        }        
    }
    // Update is called once per frame
    void Update()
    {
        if(animacaoEspecial && !animacaoexecutando)
        {
            usarAnimacaoEspecial();
        }
        if(weapon.Model == 0)
        {
            MeuCortante.Atualiza();
        }
    }
    public void IniciarArma()
    {

        //limpar armas
        LimparArma();

        switch (weapon.Model)
        {
            case 0:
                if(myRobot.PlayerRobot)
                {
                    MeuCortante.ReceberStatus(myRobot, Cortante.Tipo.JOGADOR,battleManager.screemManager.UIFisico, this);
                }
                else
                {
                    MeuCortante.ReceberStatus(myRobot, Cortante.Tipo.RIVAL, battleManager.screemManager.UIFisico, this);
                }
                break;
            case 1:
                if (myRobot.PlayerRobot)
                {
                    MeuImapcto.Ativar(Impacto.Tipo.JOGADOR, battleManager, battleManager.screemManager.UIFisico,this);
                }
                else
                {
                    MeuImapcto.Ativar(Impacto.Tipo.RIVAL, battleManager, battleManager.screemManager.UIFisico, this);
                }
                break;
            case 2:
                if (myRobot.PlayerRobot)
                {
                    MeuEscudo.Iniciar(Escudo.Tipo.JOGADOR,myRobot,battleManager, battleManager.screemManager.UIFisico,this);
                }
                else
                {
                    MeuEscudo.Iniciar(Escudo.Tipo.RIVAL, myRobot, battleManager, battleManager.screemManager.UIFisico,this);
                }
                break;
            case 3:
                if (myRobot.PlayerRobot)
                {
                    MinhaBolha.Ativar(Bolha.Tipo.JOGADOR, myRobot, battleManager, battleManager.screemManager.UIFisico, this);
                }
                else
                {
                    MinhaBolha.Ativar(Bolha.Tipo.RIVAL, myRobot, battleManager, battleManager.screemManager.UIFisico, this);
                }                
                break;
            case 4:
                if (myRobot.PlayerRobot)
                {
                    MeuCajado.Ativar(myRobot,Cajado.Tipo.JOGADOR,battleManager.screemManager.UIFisico, this);
                }
                else
                {
                    MeuCajado.Ativar(myRobot, Cajado.Tipo.RIVAL, battleManager.screemManager.UIFisico, this);
                }
                break;
            case 5:
                if (myRobot.PlayerRobot)
                {
                    MeuCanhao.Iniciar(this, canhao.Tipo.JOGADOR, battleManager.screemManager.UIFisico);
                }
                else
                {
                    MeuCanhao.Iniciar(this, canhao.Tipo.RIVAL, battleManager.screemManager.UIFisico);
                }
                break;
        }
    }
    public void LimparArma()
    {
        if(myRobot != null)
        {
            MeuEscudo.LimparArma();
            MeuCortante.Desativar();
            MeuCajado.Desativar();
            MeuCanhao.Desativar();
            if (myRobot.PlayerRobot)
            {
                MeuImapcto.Limpar(battleManager, Impacto.Tipo.JOGADOR);
                MinhaBolha.Limpar(Bolha.Tipo.JOGADOR, battleManager);

            }
            else
            {
                MeuImapcto.Limpar(battleManager, Impacto.Tipo.RIVAL);
                MinhaBolha.Limpar(Bolha.Tipo.RIVAL, battleManager);
            }
        }       
    }
   public void UsarAtaque(int id)
    {
        if (myRobot.bateriaAtual == 0)
        {
            myRobot.SomNegado();
            gerenciadorDialogo.DigitarNaTela(status.Nome + " " + textoDeBatalha.textos[0]);
            textoDeBatalha.TextoPadrao = false;
        }
        else
        {
            if(myRobot.acoesAtual - weapon.Ataque[id].UsoDeAcoes<0)
            {
                myRobot.SomNegado();
            }
            else
            {
                //inicia animacao
                if (battleManager.BattleState == BattleManager.BattleStateMachine.PLAYERTURN)
                {                    
                    battleManager.BattleState = BattleManager.BattleStateMachine.PLAYERANIMATION;
                }
                if (battleManager.BattleState == BattleManager.BattleStateMachine.ENEMYTURN)
                {
                    battleManager.BattleState = BattleManager.BattleStateMachine.ENEMYANIMATION;
                }                
                AttackAnimation anim;              
                if (weapon.Ataque[id].Only)
                {                    
                    anim = Instantiate(weapon.Ataque[id].AnimacaoDeAtaque, ManagerGame.Instance.Regiao.Posicao.transform).GetComponent<AttackAnimation>();
                }
                else
                { 
                    // instancia invertido
                    if(weapon.Ataque[id].InstanciaInvertido)
                    {
                        if(battleManager.PosicaoDeInstanciamentoAnimacao == battleManager.PlayerRobotPosition)
                        {
                            anim = Instantiate(weapon.Ataque[id].AnimacaoDeAtaque, battleManager.
                    EnemyRobotPosition.transform).GetComponent<AttackAnimation>();
                        }
                        else
                        {
                            anim = Instantiate(weapon.Ataque[id].AnimacaoDeAtaque, battleManager.
                    PlayerRobotPosition.transform).GetComponent<AttackAnimation>();
                        }
                        
                    }
                    else
                    {                       
                        anim = Instantiate(weapon.Ataque[id].AnimacaoDeAtaque, battleManager.
                    PosicaoDeInstanciamentoAnimacao.transform,false).GetComponent<AttackAnimation>();                        
                    }
                    
                }
                gerenciadorDialogo.DigitarNaTela(status.Nome + " " + textoDeBatalha.textos[1] + " " +
                    weapon.Ataque[id].Nome);
                textoDeBatalha.TextoPadrao = false;
                anim.BattleManager = battleManager;
                anim.WeaponMethod = this;
                if (weapon.Ataque[id].Elemental)
                {
                    anim.MeuTipo = AttackAnimation.Tipo.ELEMENTAL;
                }
                //calculo do ataque
                if (!weapon.Ataque[id].Diferenciado)
                {
                    multiplicadorfora = combo(weapon.Ataque[id].Id);
                    Atack(weapon.Ataque[id].Forca * weapon.Forca / 100, multiplicadorfora, weapon.Ataque[id].Elemental,
                    weapon.Ataque[id].Efeito, weapon.Ataque[id].AumentoEfeito, weapon.Ataque[id].GastoEnergia,
                    weapon.Ataque[id].Precisao, weapon.Ataque[id].UsoDeAcoes, weapon.Ataque[id].Elemento, status.NucleoElemental,id);
                }
                else
                {
                    StartCoroutine(battleManager.Atacar(0, false, 0, 0, 100, 0, 0, true,id));
                    Diferenciado.AtacarDiferenciado(weapon.Ataque[id].Move2.Id, this);
                    myRobot.GastoBateria(weapon.Ataque[id].GastoEnergia + regitrarAumentoEnergia);
                    myRobot.DiminuiAcoes(weapon.Ataque[id].UsoDeAcoes);
                }
            }            
        }
    }
    public void Atack(float forca, float multiplicador, bool elemental, int efeito, float aumentoefeito, float gasto, int precisao, int acoes
        ,int elemrob, int elematk, int id)
    {
        float dano;
        if (elemental)
        {
            dano = forca * multiplicador * myRobot.AtaqueEnergeticoAtual;
            
        }
        else
        {
            dano = forca * multiplicador * myRobot.AtaqueAtual;
        }       
        StartCoroutine(battleManager.Atacar(dano, elemental, efeito, aumentoefeito, precisao,elematk,elemrob, false,id));
        myRobot.GastoBateria(gasto+regitrarAumentoEnergia);
        myRobot.DiminuiAcoes(acoes);
        multiplicador = 1;
    }
    public float combo(int idataque)
    {
        float multiplicadorCalculado;
        for (int i = 0; i < 6; i++)
        {
            if (moves[i] == 0)
            {
                moves[i] = idataque;
                break;
            }
        }
        multiplicadorCalculado = calculaMultiplicadorCombo();       
        return multiplicadorCalculado;
    }
    private float calculaMultiplicadorCombo()
    {
        float multiplicadorCombo = 1;
        if (weapon.Combo.Count > 0)
        {           
            for (int i = 0; i < weapon.Combo.Count; i++)
            {               
                if (moves[0] == weapon.Combo[i].ComboID[1] && moves[1] == weapon.Combo[i].ComboID[1]
                   && moves[2] == 0 && moves[3] == 0 && moves[4] == 0 && moves[5] == 0)
                {
                    multiplicadorCombo = 1.1f;
                    Combo?.Invoke();
                    ManagerGame.Instance.Fezcombo(1);
                    if (weapon.Combo[i].ComboID[2] == 0)
                    {
                        ReiniciaCombo();
                    }
                    break;
                }
                if (moves[0] == weapon.Combo[i].ComboID[1] && moves[1] == weapon.Combo[i].ComboID[1]
                   && moves[2] == weapon.Combo[i].ComboID[2] && moves[3] == 0 && moves[4] == 0 && moves[5] == 0)
                {
                    multiplicadorCombo = 1.15f;
                    Combo?.Invoke();
                    ManagerGame.Instance.Fezcombo(2);
                    if (weapon.Combo[i].ComboID[3] == 0)
                    {
                        ReiniciaCombo();
                    }
                    break;
                }
                if (moves[0] == weapon.Combo[i].ComboID[1] && moves[1] == weapon.Combo[i].ComboID[1]
                   && moves[2] == weapon.Combo[i].ComboID[2] && moves[3] == weapon.Combo[i].ComboID[3]
                   && moves[4] == 0 && moves[5] == 0)
                {
                    if (weapon.Combo[i].ComboID[4] == 0)
                    {
                        multiplicadorCombo = 1.2f;
                        Combo?.Invoke();
                        ManagerGame.Instance.Fezcombo(3);
                        ReiniciaCombo();
                    }
                    break;
                }
                if (moves[0] == weapon.Combo[i].ComboID[1] && moves[1] == weapon.Combo[i].ComboID[1]
                   && moves[2] == weapon.Combo[i].ComboID[2] && moves[3] == weapon.Combo[i].ComboID[3]
                   && moves[4] == weapon.Combo[i].ComboID[4] && moves[5] == 0)
                {
                    if (weapon.Combo[i].ComboID[5] == 0)
                    {
                        multiplicadorCombo = 1.42f;
                        Combo?.Invoke();
                        ManagerGame.Instance.Fezcombo(4);
                        ReiniciaCombo();
                    }
                    break;
                }
                if (moves[0] == weapon.Combo[i].ComboID[1] && moves[1] == weapon.Combo[i].ComboID[1]
                   && moves[2] == weapon.Combo[i].ComboID[2] && moves[3] == weapon.Combo[i].ComboID[3]
                   && moves[4] == weapon.Combo[i].ComboID[4] && moves[5] == weapon.Combo[i].ComboID[5])
                {
                    multiplicadorCombo = 1.6f;
                    Combo?.Invoke();
                    ManagerGame.Instance.Fezcombo(5);
                    animacaoEspecial = true;
                    break;
                }
            }
        }
        //exit:
        return multiplicadorCombo;
    }    
    
    void usarAnimacaoEspecial()
    {
        animacaoexecutando = true;
        if (battleManager.BattleState == BattleManager.BattleStateMachine.PLAYERTURN)
        {
             XFX x = Instantiate(AnimacaoEspecial, battleManager.EnemyRobotPosition.transform).GetComponent<XFX>();
             x.MinhaCor = myRobot.MinhaCor;
            // battleManager.Atacar(x.Dano, false, 0, 0, 101,status.NucleoElemental,status.NucleoElemental, false,0);
             battleManager.BattleState = BattleManager.BattleStateMachine.PLAYERANIMATION;
        }
        if (battleManager.BattleState == BattleManager.BattleStateMachine.ENEMYTURN)
        {
            XFX x = Instantiate(AnimacaoEspecial, battleManager.PlayerRobotPosition.transform).GetComponent<XFX>();
            x.MinhaCor = myRobot.MinhaCor;
           // battleManager.Atacar(x.Dano, false, 0, 0, 101,status.NucleoElemental, status.NucleoElemental, false,0);
            battleManager.BattleState = BattleManager.BattleStateMachine.ENEMYANIMATION;
        }        
     }
    public void ReiniciaCombo()
    {
        for (int i = 0; i < 6; i++)
        {
            moves[i] = 0;
        }
    }
    public void AumentarGasto(float aumento)
    {       
        regitrarAumentoEnergia = aumento;
    }
    public void DiminuirGasto()
    {       
       regitrarAumentoEnergia = 0f;
    }
    public void AtirarCanhao(GameObject A, int forca)
    {
        AttackAnimation anim;
        anim = Instantiate(A, ManagerGame.Instance.Regiao.Posicao.transform).GetComponent<AttackAnimation>();
        anim.BattleManager = battleManager;
        anim.WeaponMethod = this;
        Atack(forca, 1, false, 0, 0, 0, 100, 0, 9, 9,0);
    }
    public void AtualizarBarraEscudo()
    {
        if(BarraEnergiaEscudo !=null)
        {
            StopCoroutine(BarraEnergiaEscudo);
        }
        BarraEnergiaEscudo = MeuEscudo.AtualizaBarraDeEnergia();
        StartCoroutine(BarraEnergiaEscudo);
    }
    public void AtualizaPoderCajado()
    {
        if (BarraPoderCajado != null)
        {
            StopCoroutine(BarraPoderCajado);
        }
        BarraPoderCajado = MeuCajado.AtualizaBarraDePoder();
        StartCoroutine(BarraPoderCajado);
    }
    public void AnimacaoAtivarArma()
    {
        myRobot.ParticulaAcao();
        myRobot.AnimacaoArmaOn();
    }
    public void DesativarAnimArma()
    {
        myRobot.AnimacaoArmaOff();
    }
    public void DesativarCortanteQuebrado()
    {
        if(weapon.Model ==0)
        {
            MeuCortante.DesativarQuebrado();
        }
    }
}


