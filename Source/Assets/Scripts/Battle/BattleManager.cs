using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [HideInInspector]
    public enum BattleStateMachine
    {
        PLAYERSTART,
        START,
        IDLE,
        PLAYERTURN,
        PLAYERANIMATION,
        PLAYERITEM,
        PLAYERTRADE,
        ENEMYTURN,
        ENEMYANIMATION,
        ENEMYTRADE,
        OTHERANIMATION,
    }
    public BattleStateMachine BattleState;
    [HideInInspector]
    public BattleStateMachine Reiniciar = BattleStateMachine.PLAYERSTART;
    [HideInInspector]
    public ActiveRobotManager ActiveRobotManager;
    [HideInInspector]
    public GameObject PlayerRobot;
    [HideInInspector]
    public GameObject EnemyRobot;
    [HideInInspector]
    public TransitionManager screemManager;
    [HideInInspector]
    public bool TurnoPlayer = false;
    [HideInInspector]
    public bool TurnoEnemy = false;
    [HideInInspector]
    public RobotManager playerManager;
    [HideInInspector]
    public RobotManager enemyManager;
    [HideInInspector]
    public WeaponMethods playerWeapon;
    [HideInInspector]
    public WeaponMethods enemyWeapon;
    [HideInInspector]
    public Status playerStatus;
    [HideInInspector]
    public Status enemyStatus;
    private float contadorAtaque = 0;
    [HideInInspector]
    public int contadorOverlockP = 0;
    [HideInInspector]
    private int contadorOverlockE = 0;
    private int tipodeDanoProximo;
    public float FatorDeDivisaoResistencia;
    //variaveis para decidir quem ataca e quem toma dano
    private RobotManager roboQueTomaDano;
    private RobotManager roboQueAtaca;
    
    float[] fatorelementAtac = new float[6];
    private float fatorArmaAtacante;
    //variaveis para caluclo das vantagens para o dano dano
    private float fatorElementalPlayer = 1;
    float[] fatorelementPlayer = new float[6];
    private float fatorElementalEnemy = 1;
    float[] fatorelementEnemy = new float[6];
    private float fatorArmaPlayer = 1;
    private float fatorArmaEnemy = 1;
    //Objetos Vazios Para Marcar a Posicao
    public GameObject PlayerRobotPosition;
    public GameObject WaitingPlayerRobotPosition;
    public GameObject EnemyRobotPosition;
    public GameObject WaitingEnemyRobotPosition;
    [HideInInspector]
    public GameObject PosicaoDeInstanciamentoAnimacao;
    [HideInInspector]
    public bool EnemyCanAttack = false;
    private bool diferenciado = false;
    //variaveis para gestão do ataque no fim da animação

    float danoIntegridade;
    float danoResistencia;
    int efeito;
    float fatorEfeito;
    int precis;
    //eventos
    public event Action JogadorFimTurno;
    public event Action RivalFimTurno;
    public event Action EventoMarcus;
    int contadorMarcus = 0;
    //barreiras
    public bool barreiraelempl;
    public bool barreirafisicapl;
    public bool barreiraelemriv;
    public bool barreirafisicariv;

    public GameObject AvisoFisico;
    public GameObject AvisoElemental;
    public AudioSource SourceAcertou;
    // Start is called before the first frame update
    private void Awake()
    {
        screemManager = GetComponent<TransitionManager>();
        ActiveRobotManager = GetComponent<ActiveRobotManager>();
    }
    void OnEnable()
    {
        GetRobots();
        screemManager.AtualizarMenuAtaques(ActiveRobotManager.ActivePlayerRobot.GetComponent<Status>().NucleoFisico, ActiveRobotManager.ActivePlayerRobot.GetComponent<WeaponMethods>());
        BattleState = BattleStateMachine.PLAYERSTART;
    }
    // Update is called once per frame
    void Update()
    {
        contadorAtaque += Time.deltaTime;
        switch (BattleState)
        {
            case BattleStateMachine.PLAYERSTART:

                break;
            case BattleStateMachine.START:
                if (PosicaoDeInstanciamentoAnimacao != null) { PosicaoDeInstanciamentoAnimacao = null; }
                if (playerManager.acoesAtual > 0 && !playerManager.KO && !enemyManager.KO)
                {
                    BattleState = BattleStateMachine.IDLE;
                }
                break;
            case BattleStateMachine.IDLE:
                screemManager.MostrarCaixaDeAcao();
                break;
            case BattleStateMachine.PLAYERTURN:
                //mostra menu
                screemManager.MostrarMenuAtaques();
                //finaliza turno;
                if (playerManager.acoesAtual <= 0 || enemyManager.KO)
                {
                    if (!playerWeapon.animacaoexecutando) { PlayerEndTurn(); }
                }
                break;
            case BattleStateMachine.PLAYERANIMATION:
                screemManager.EsconderMenuAtaques();
                if (playerManager.KO)
                {
                    PlayerEndTurn();
                }
                break;
            case BattleStateMachine.PLAYERITEM:
                if (playerManager.acoesAtual <= 0)
                {
                    screemManager.EsconderMenuItem();
                    BattleState = BattleStateMachine.START;
                }
                break;
            case BattleStateMachine.PLAYERTRADE:
                break;
            case BattleStateMachine.ENEMYTURN:
                screemManager.EsconderCaixaDeAcao();
                //finaliza turno
                if (enemyManager.acoesAtual <= 0 || playerManager.KO)
                {
                    if (!enemyWeapon.animacaoexecutando) { EnemyEndTurn(); }
                }
                break;
            case BattleStateMachine.ENEMYTRADE:
                screemManager.EsconderCaixaDeAcao();
                break;
            case BattleStateMachine.ENEMYANIMATION:
                if (enemyManager.KO)
                {
                    EnemyEndTurn();
                }
                break;
            case BattleStateMachine.OTHERANIMATION:
                break;

        }
        if (playerManager.Overlock && (contadorOverlockP == 3))
        {
            playerManager.OverlockOFF();
        }
        if (enemyManager.Overlock && (contadorOverlockE == 3))
        {
            enemyManager.OverlockOFF();
        }
    }
    public void GetRobots()
    {
        PlayerRobot = ActiveRobotManager.ActivePlayerRobot;
        EnemyRobot = ActiveRobotManager.ActiveEnemyRobot;

        playerManager = PlayerRobot.GetComponent<RobotManager>();
        enemyManager = EnemyRobot.GetComponent<RobotManager>();

        playerStatus = PlayerRobot.GetComponent<Status>();
        enemyStatus = EnemyRobot.GetComponent<Status>();

        playerWeapon = PlayerRobot.GetComponent<WeaponMethods>();
        enemyWeapon = EnemyRobot.GetComponent<WeaponMethods>();
        calculoDeFatores();
    }
    // esse void faz todos os calculos relativos ao ataque ele vai receber as caracteriticas do ataque processa-las e envia-las para o toma dano apropriado
    public IEnumerator Atacar(float dano, bool elemental, int efeitoN, float aumento, int precisao, int elemrob, int elematk, bool dif, int id)
    {
        if (!dif)
        {
            diferenciado = false;
            float danoA;
            float Resistencia;
            efeito = efeitoN;
            fatorEfeito = aumento;
            precis = precisao;
            // executa o ataque


            // calcula o dano do player.
            if (efeito == 0)
            {
                if (roboQueTomaDano.broke)
                {
                    Resistencia = 0.75f;
                }
                else
                {
                    Resistencia = roboQueTomaDano.ResistenciaAtual;
                }
                if (elemental)
                {
                    if (fatorelementAtac[id] < 1) { tipodeDanoProximo = 0; }
                    else if (fatorelementAtac[id] == 1) { tipodeDanoProximo = 2; }
                    else { tipodeDanoProximo = 3; }
                    danoA = calculoDano(dano, roboQueAtaca.AtaqueAtual, Resistencia, fatorelementAtac[id]);
                    danoIntegridade = danoA / 15;
                    danoResistencia = danoA / 24;
                    if (elematk != elemrob)
                    {
                        danoIntegridade *= 0.7f;
                    }
                    danoIntegridade = roboQueAtaca.MyWeapon.MeuCajado.Agir(danoIntegridade, roboQueTomaDano.MyStatus.Integridade);
                    if (roboQueTomaDano.PlayerRobot && barreiraelempl)
                    {
                        danoIntegridade *= 0.8f;
                        danoResistencia *= 0.8f;
                    }
                    if (!roboQueTomaDano.PlayerRobot && barreiraelemriv)
                    {
                        danoIntegridade *= 0.8f;
                        danoResistencia *= 0.8f;
                    }
                    if (StoryEvents.TutorialMarcus && roboQueAtaca.PlayerRobot && !roboQueTomaDano.broke)
                    {
                        contadorMarcus++;
                        if (contadorMarcus >= 3) { contadorMarcus = 0; EventoMarcus?.Invoke(); }
                    }
                }
                else
                {
                    if (fatorArmaAtacante < 1) { tipodeDanoProximo = 0; }
                    else { tipodeDanoProximo = 1; }
                    danoA = calculoDano(dano, roboQueAtaca.AtaqueAtual, Resistencia, fatorArmaAtacante);
                    danoIntegridade = danoA / 50;
                    danoResistencia = danoA / 2;
                    danoResistencia = roboQueAtaca.MyWeapon.MeuCajado.Agir(danoResistencia, roboQueTomaDano.MyStatus.Resistencia);
                    if (roboQueTomaDano.PlayerRobot && barreirafisicapl)
                    {
                        danoIntegridade *= 0.8f;
                        danoResistencia *= 0.8f;
                    }
                    if (!roboQueTomaDano.PlayerRobot && barreirafisicariv)
                    {
                        danoIntegridade *= 0.8f;
                        danoResistencia *= 0.8f;
                    }
                }
                if (roboQueAtaca.MyWeapon.weapon.Model == 1)
                {
                    danoIntegridade = roboQueAtaca.MyWeapon.MeuImapcto.CalculaAtaque(danoIntegridade);
                    danoResistencia = roboQueAtaca.MyWeapon.MeuImapcto.CalculaAtaque(danoResistencia);
                }
            }

        }
        else
        {
            diferenciado = true;
        }
        contadorAtaque = 0;
        yield return null;
    }    
    public void ExecutarDano(float fatorres, int fatorpres)
    {
        if (roboQueTomaDano != null)
        {
            if (!diferenciado)
            {
                if (UnityEngine.Random.Range(0, 101) < precis - fatorpres)
                {
                    if (efeito == 0)
                    {
                        roboQueTomaDano.TomarDano(danoIntegridade, danoResistencia * fatorres, tipodeDanoProximo);
                    }
                    else
                    {
                        pegouInfeccao(efeito, roboQueTomaDano, fatorEfeito);
                        if (BattleState == BattleManager.BattleStateMachine.PLAYERANIMATION)
                        {
                            BattleState = BattleManager.BattleStateMachine.PLAYERTURN;
                        }
                        if (BattleState == BattleManager.BattleStateMachine.ENEMYANIMATION)
                        {
                            BattleState = BattleManager.BattleStateMachine.ENEMYTURN;
                        }
                    }
                }
                else
                {
                    roboQueTomaDano.FloatTextON("Drible");
                    if (BattleState == BattleManager.BattleStateMachine.PLAYERANIMATION)
                    {
                        BattleState = BattleManager.BattleStateMachine.PLAYERTURN;
                    }
                    if (BattleState == BattleManager.BattleStateMachine.ENEMYANIMATION)
                    {
                        BattleState = BattleManager.BattleStateMachine.ENEMYTURN;
                    }
                }
            }
            else
            {
                if (BattleState == BattleManager.BattleStateMachine.PLAYERANIMATION)
                {
                    BattleState = BattleManager.BattleStateMachine.PLAYERTURN;
                }
                if (BattleState == BattleManager.BattleStateMachine.ENEMYANIMATION)
                {
                    BattleState = BattleManager.BattleStateMachine.ENEMYTURN;
                }
            }
        }
    }
    //inicializadores e finalizadores de turno
    public void PlayerStartTurn()
    {
        //decide as posicoes de ataque
        //EnemyRobotPosition.transform.position = ActiveRobotManager.ActiveEnemyRobot.transform.position;
        PosicaoDeInstanciamentoAnimacao = EnemyRobotPosition;
        roboQueTomaDano = enemyManager;
        roboQueAtaca = playerManager;       
        for (int i = 0; i < 6; i++)
        {
            fatorelementAtac[i] = fatorelementPlayer[i];
        }
        fatorArmaAtacante = fatorArmaPlayer;
        contadorAtaque = 0;
        playerManager.MeuTurno = true;
        BattleState = BattleStateMachine.PLAYERTURN;
    }
    public void PlayerEndTurn()
    {
        BattleState = BattleStateMachine.PLAYERANIMATION;
        playerManager.MeuTurno = false;
        playerWeapon.ReiniciaCombo();
        if (playerManager.Overlock)
        {
            contadorOverlockP++;
        }
        if (!EnemyCanAttack && playerManager.acoesAtual == 0)
        {
            EnemyCanAttack = true;
        }
        playerManager.LowBatterieVerify();
        JogadorFimTurno?.Invoke();
        Reiniciar = BattleStateMachine.START;
        playerManager.SoltarFumaca();
    }
    public void EnemyStartTurn()
    {
        //decide posicoes da batalha
        //PlayerRobotPosition.transform.position = ActiveRobotManager.ActivePlayerRobot.transform.position;
        PosicaoDeInstanciamentoAnimacao = PlayerRobotPosition;
        roboQueTomaDano = playerManager;
        roboQueAtaca = enemyManager;      
        for (int i = 0; i < 6; i++)
        {
            fatorelementAtac[i] = fatorelementEnemy[i];
        }
        fatorArmaAtacante = fatorArmaEnemy;
        contadorAtaque = 0;
        enemyManager.MeuTurno = true;
        BattleState = BattleStateMachine.ENEMYTURN;
    }
    public void EnemyEndTurn()
    {
        BattleState = BattleStateMachine.ENEMYANIMATION;
        PlayerRobotPosition.transform.position = ActiveRobotManager.ActivePlayerRobot.transform.position;
        enemyManager.MeuTurno = false;
        EnemyRobot.GetComponent<IA>().StartAttacking = false;
        EnemyRobot.GetComponent<IA>().newCombo = true;
        EnemyRobot.GetComponent<IA>().instancia = 0;
        enemyWeapon.ReiniciaCombo();
        if (enemyManager.Overlock)
        {
            contadorOverlockE++;
        }
        enemyManager.LowBatterieVerify();
        enemyManager.SoltarFumaca();
        RivalFimTurno?.Invoke();
    }
    private float calculoDano(float dano, float ataqueAtual, float resistenciaAtual, float fatordeMult)
    {
        float danocalc = ((dano * ataqueAtual / 40) / (resistenciaAtual)) * fatordeMult + (UnityEngine.Random.Range(120, 200) / 70);
        return danocalc;
    }
    //Calculos
    private void calculoDeFatores()
    {
        int pnucleo = playerStatus.NucleoElemental;
        int parma = playerStatus.NucleoFisico.Model;

        int enucleo = enemyStatus.NucleoElemental;
        int earma = enemyStatus.NucleoFisico.Model;

        //fator elemental player
        fatorElementalPlayer = fatorElemental(pnucleo, enucleo);
        for(int i = 0; i< playerStatus.NucleoFisico.Ataque.Length; i++)
        {
            if (playerStatus.NucleoFisico.Ataque[i] != null)
            {
                if (playerStatus.NucleoFisico.Ataque[i].Elemental)
                {
                    fatorelementPlayer[i] = fatorElemental(playerStatus.NucleoFisico.Ataque[i].Elemento, enucleo);
                }
                else
                {
                    fatorelementPlayer[i] = 0.5f;
                }
            }
        }
        // fator arma player
        fatorArmaPlayer = fatorArma(parma, earma);
        // fator elemental enemy
        fatorElementalEnemy = fatorElemental(enucleo, pnucleo);
        for (int i = 0; i < enemyStatus.NucleoFisico.Ataque.Length; i++)
        {
            if(enemyStatus.NucleoFisico.Ataque[i]!=null)
            {
                if (enemyStatus.NucleoFisico.Ataque[i].Elemental)
                {
                    fatorelementEnemy[i] = fatorElemental(enemyStatus.NucleoFisico.Ataque[i].Elemento, pnucleo);
                }
                else
                {
                    fatorelementEnemy[i] = 0.5f;
                }
            }
           
        }
        //fator arma enemy
        fatorArmaEnemy = fatorArma(earma, parma);
    }
    public float fatorElemental(int elementalAtivo, int elementalPassivo)
    {
        //0 - Vermelho
        //1 - Azul
        //2 - Amarelo
        //3 - Verde
        //4 - Laranja
        //5 - Roxo
        //fator 1
        float multiplicadorNucleo = 1f;
        //fator 0.5
        if (elementalAtivo == elementalPassivo)
        {
            multiplicadorNucleo = 0.5f;
            goto end_of_calc;
        }
        //fator 2
        if (elementalAtivo == 0 && elementalPassivo == 3 || elementalAtivo == 0 && elementalPassivo == 5)
        {
            multiplicadorNucleo = 2f;
            goto end_of_calc;
        }
        if (elementalAtivo == 1 && elementalPassivo == 0 || elementalAtivo == 1 && elementalPassivo == 4)
        {
            multiplicadorNucleo = 2f;
            goto end_of_calc;
        }
        if (elementalAtivo == 2 && elementalPassivo == 1 || elementalAtivo == 2 && elementalPassivo == 3)
        {
            multiplicadorNucleo = 2f;
            goto end_of_calc;
        }
        if (elementalAtivo == 3 && elementalPassivo == 4 || elementalAtivo == 3 && elementalPassivo == 5)
        {
            multiplicadorNucleo = 2f;
            goto end_of_calc;
        }
        if (elementalAtivo == 4 && elementalPassivo == 0 || elementalAtivo == 4 && elementalPassivo == 2)
        {
            multiplicadorNucleo = 2f;
            goto end_of_calc;
        }
        if (elementalAtivo == 5 && elementalPassivo == 1 || elementalAtivo == 5 && elementalPassivo == 2)
        {
            multiplicadorNucleo = 2f;
            goto end_of_calc;
        }
    end_of_calc:
        return multiplicadorNucleo;
    }
    private float fatorArma(int armaAtivo, int armaPassivo)
    {
        //0 - Cortante
        //1 - Impacto
        //2 - Escudo
        //3 - BolhaDeEnergia
        //4 - Cajado
        //5 - Canhao
        //fator 1
        float multiplicadorArma = 1f;
        //fator 0.75
        if (armaAtivo == 0 && armaPassivo == 0 || armaAtivo == 0 && armaPassivo == 3)
        {
            multiplicadorArma = 0.75f;
            goto end_of_calc;
        }
        if (armaAtivo == 1 && armaPassivo == 1 || armaAtivo == 1 && armaPassivo == 3)
        {
            multiplicadorArma = 0.75f;
            goto end_of_calc;
        }
        if (armaAtivo == 2 && armaPassivo == 2 || armaAtivo == 2 && armaPassivo == 5)
        {
            multiplicadorArma = 0.75f;
            goto end_of_calc;
        }
        if (armaAtivo == 3 && armaPassivo == 3 || armaAtivo == 3 && armaPassivo == 0 || armaAtivo == 3 && armaPassivo == 1)
        {
            multiplicadorArma = 0.75f;
            goto end_of_calc;
        }
        if (armaAtivo == 4 && armaPassivo == 4 || armaAtivo == 4 && armaPassivo == 5)
        {
            multiplicadorArma = 0.75f;
            goto end_of_calc;
        }
        if (armaAtivo == 5 && armaPassivo == 2 || armaAtivo == 5 && armaPassivo == 4)
        {
            multiplicadorArma = 0.75f;
            goto end_of_calc;
        }
        //fator 0.5
        if (armaAtivo == 0 && armaPassivo == 1 || armaAtivo == 0 && armaPassivo == 2)
        {
            multiplicadorArma = 0.5f;
            goto end_of_calc;
        }
        if (armaAtivo == 1 && armaPassivo == 2 || armaAtivo == 1 && armaPassivo == 4)
        {
            multiplicadorArma = 0.5f;
            goto end_of_calc;
        }
        if (armaAtivo == 2 && armaPassivo == 3 || armaAtivo == 2 && armaPassivo == 4)
        {
            multiplicadorArma = 0.5f;
            goto end_of_calc;
        }
        if (armaAtivo == 3 && armaPassivo == 5)
        {
            multiplicadorArma = 0.5f;
            goto end_of_calc;
        }
        if (armaAtivo == 4 && armaPassivo == 0 || armaAtivo == 4 && armaPassivo == 3)
        {
            multiplicadorArma = 0.5f;
            goto end_of_calc;
        }
        if (armaAtivo == 5 && armaPassivo == 0 || armaAtivo == 5 && armaPassivo == 1)
        {
            multiplicadorArma = 0.5f;
            goto end_of_calc;
        }
    end_of_calc:
        return multiplicadorArma;
    }
    void pegouInfeccao(int efeito, RobotManager manager, float fator)
    {
        switch (efeito)
        {
            case 1:
                if (!manager.Spy)
                {
                    manager.InfectadoSpy(fator);
                }
                else
                {
                    manager.JaInfectado();
                }

                break;
            case 2:
                manager.InfectadoKeylogger();
                break;
            case 3:
                if (!manager.Trojan)
                {
                    manager.InfectarTrojan(fator);
                }
                else
                {
                    manager.JaInfectado();
                }

                break;
            case 4:
                if (!manager.Ranson)
                {
                    manager.InfectarRanson(fator);

                }
                else
                {
                    manager.JaInfectado();
                }

                break;
            case 5:
                if (!manager.Worm)
                {
                    manager.InfectadoWorm(fator);
                }
                else
                {
                    manager.JaInfectado();
                }

                break;
            case 6:
                if (!manager.Virus)
                {
                    manager.InfectadoVirus(fator);
                }
                else
                {
                    manager.JaInfectado();
                }

                break;
        }
    }
    public void RivTomaDano()
    {
        roboQueTomaDano = enemyManager;
    }
    public void PLTomaDao()
    {
        roboQueTomaDano = playerManager;
    }
    public int RetornaRelacaoFisico(int armaAtivo)
    {
        //0 - Cortante
        //1 - Impacto
        //2 - Escudo
        //3 - BolhaDeEnergia
        //4 - Cajado
        //5 - Canhao
        //relacao
        //0 - desvatagem
        //1-equilibrado
        //2-desvantagem
        int relacao = 1;
        int armaPassivo = enemyStatus.NucleoFisico.Model;
        float multiplicadorArma = 1f;
        //fator 0.75
        if (armaAtivo == 0 && armaPassivo == 0 || armaAtivo == 0 && armaPassivo == 3)
        {
            multiplicadorArma = 0.75f;
            goto end_of_calc;
        }
        if (armaAtivo == 1 && armaPassivo == 1 || armaAtivo == 1 && armaPassivo == 3)
        {
            multiplicadorArma = 0.75f;
            goto end_of_calc;
        }
        if (armaAtivo == 2 && armaPassivo == 2 || armaAtivo == 2 && armaPassivo == 5)
        {
            multiplicadorArma = 0.75f;
            goto end_of_calc;
        }
        if (armaAtivo == 3 && armaPassivo == 3 || armaAtivo == 3 && armaPassivo == 0 || armaAtivo == 3 && armaPassivo == 1)
        {
            multiplicadorArma = 0.75f;
            goto end_of_calc;
        }
        if (armaAtivo == 4 && armaPassivo == 4 || armaAtivo == 4 && armaPassivo == 5)
        {
            multiplicadorArma = 0.75f;
            goto end_of_calc;
        }
        if (armaAtivo == 5 && armaPassivo == 2 || armaAtivo == 5 && armaPassivo == 4)
        {
            multiplicadorArma = 0.75f;
            goto end_of_calc;
        }
        //fator 0.5
        if (armaAtivo == 0 && armaPassivo == 1 || armaAtivo == 0 && armaPassivo == 2)
        {
            multiplicadorArma = 0.5f;
            goto end_of_calc;
        }
        if (armaAtivo == 1 && armaPassivo == 2 || armaAtivo == 1 && armaPassivo == 4)
        {
            multiplicadorArma = 0.5f;
            goto end_of_calc;
        }
        if (armaAtivo == 2 && armaPassivo == 3 || armaAtivo == 2 && armaPassivo == 4)
        {
            multiplicadorArma = 0.5f;
            goto end_of_calc;
        }
        if (armaAtivo == 3 && armaPassivo == 5)
        {
            multiplicadorArma = 0.5f;
            goto end_of_calc;
        }
        if (armaAtivo == 4 && armaPassivo == 0 || armaAtivo == 4 && armaPassivo == 3)
        {
            multiplicadorArma = 0.5f;
            goto end_of_calc;
        }
        if (armaAtivo == 5 && armaPassivo == 0 || armaAtivo == 5 && armaPassivo == 1)
        {
            multiplicadorArma = 0.5f;
            goto end_of_calc;
        }
    end_of_calc:
        if (multiplicadorArma < 1f) { relacao = 0; }
    else if (multiplicadorArma > 1f){ relacao = 2; }
        return relacao;
    }
    public int RetornaRelacaoElement(int elementalAtivo)
    {
        //0 - desvatagem
        //1-equilibrado
        //2-desvantagem
        int relacao = 1;  
        
        int elementalPassivo = enemyStatus.NucleoElemental;
        //0 - Vermelho
        //1 - Azul
        //2 - Amarelo
        //3 - Verde
        //4 - Laranja
        //5 - Roxo
        //fator 1
        float multiplicadorNucleo = 1f;
        //fator 0.5
        if (elementalAtivo == elementalPassivo)
        {
            multiplicadorNucleo = 0.5f;
            goto end_of_calc;
        }
        //fator 2
        if (elementalAtivo == 0 && elementalPassivo == 3 || elementalAtivo == 0 && elementalPassivo == 5)
        {
            multiplicadorNucleo = 2f;
            goto end_of_calc;
        }
        if (elementalAtivo == 1 && elementalPassivo == 0 || elementalAtivo == 1 && elementalPassivo == 4)
        {
            multiplicadorNucleo = 2f;
            goto end_of_calc;
        }
        if (elementalAtivo == 2 && elementalPassivo == 1 || elementalAtivo == 2 && elementalPassivo == 3)
        {
            multiplicadorNucleo = 2f;
            goto end_of_calc;
        }
        if (elementalAtivo == 3 && elementalPassivo == 4 || elementalAtivo == 3 && elementalPassivo == 5)
        {
            multiplicadorNucleo = 2f;
            goto end_of_calc;
        }
        if (elementalAtivo == 4 && elementalPassivo == 0 || elementalAtivo == 4 && elementalPassivo == 2)
        {
            multiplicadorNucleo = 2f;
            goto end_of_calc;
        }
        if (elementalAtivo == 5 && elementalPassivo == 1 || elementalAtivo == 5 && elementalPassivo == 2)
        {
            multiplicadorNucleo = 2f;
            goto end_of_calc;
        }
    end_of_calc:
        if (multiplicadorNucleo < 1f) { relacao = 0; }
        else if (multiplicadorNucleo > 1f) { relacao = 2; }
        return relacao;
    }
}
