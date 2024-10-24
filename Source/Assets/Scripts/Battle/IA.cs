using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA : MonoBehaviour
{
    // vai de 1 a 10 do mais facil para o mais dificil
    public enum IaState
    {
        CARREGANDO,              
        ESPERANDO,
        AGINDO,
    }
    public IaState iaState = IaState.ESPERANDO;
    public int Dificulty;
    [HideInInspector]
    public int iaLevel;
    private WeaponMethods myWeapon;
    private RobotManager myRobot;
    [HideInInspector]
    public BattleManager battleManager;
    [HideInInspector]
    public ActiveRobotManager robotManager;
    [HideInInspector]
    public string nextRobot = null;
    [HideInInspector]
    public int instancia = 0;
    //private int move = 1;
    private int totalMove;
    private float hp;
    private float batterie;
    //wax
    private int softWax;
    private int hardWax;
    private int ironWax;
    //batteries
    private int AA;
    private int C;
    private int D;
    private int NineV;
    //cleaners
    private int SpyCleaner;
    private int KeyLoggerCleaner;
    private int TrojanCleaner;
    private int RansonCleaner;
    private int WormCleaner;
    private int VirusCleaner;
    // ligar e desligar a ia
   // [HideInInspector]
    public bool Ia = false;
    //controla se ja fez overlock
    private bool overlock = false;
    [HideInInspector]
    public bool StartAttacking = false;
    [HideInInspector]
    public bool newCombo = true;
    public int combo;
    [HideInInspector]
    public int comboID;
    float contador;
    float contadorseguranca;
    float esperarAtacar = 1.5f;
    int contadorAleatorio = 0;
    bool ataqueEsperto = false;
    // Start is called before the first frame update
    void Start()
    {
        myWeapon = GetComponent<WeaponMethods>();
        myRobot = GetComponent<RobotManager>();    
        totalMove = myRobot.MyStatus.Acoes;       
        switch(Dificulty)
        {
            case 0:
        
            iaLevel = 1;
            softWax = 0;
            hardWax = 0;
            ironWax = 0;
            AA = 0;
            D = 0;
            C = 0;
            NineV = 0;
            SpyCleaner = 0;
            KeyLoggerCleaner = 0;
            TrojanCleaner = 0;
            RansonCleaner = 0;
            WormCleaner = 0;
            VirusCleaner = 0;
                esperarAtacar = 1.5f;
                ataqueEsperto = false;
                break;
            case 1:
        
            iaLevel = 2;
            softWax = 0;
            hardWax = 0;
            ironWax = 0;
            AA = 0;
            D = 0;
            C = 0;
            NineV = 0;
            SpyCleaner = 0;
            KeyLoggerCleaner = 0;
            TrojanCleaner = 0;
            RansonCleaner = 0;
            WormCleaner = 0;
            VirusCleaner = 0;
                esperarAtacar = 1.5f;
                ataqueEsperto = false;
                break;
            case 2:
        
            iaLevel = 3;
            softWax = 0;
            hardWax = 0;
            ironWax = 0;
            AA = 0;
            D = 0;
            C = 0;
            NineV = 0;
            SpyCleaner = 0;
            KeyLoggerCleaner = 0;
            TrojanCleaner = 0;
            RansonCleaner = 0;
            WormCleaner = 0;
            VirusCleaner = 0;
                esperarAtacar = 1.5f;
                ataqueEsperto = false;
                break;
            case 3:
        
            iaLevel = 4;
            softWax = 0;
            hardWax = 0;
            ironWax = 0;
            AA = 0;
            D = 0;
            C = 0;
            NineV = 0;
            SpyCleaner = 1;
            KeyLoggerCleaner = 1;
            TrojanCleaner = 1;
            RansonCleaner = 1;
            WormCleaner = 1;
            VirusCleaner = 1;
                esperarAtacar = 1f;
                ataqueEsperto = false;
                break;
            case 4:
        
            iaLevel = 4;
            softWax = 1;
            hardWax = 0;
            ironWax = 0;
            AA = 1;
            D = 0;
            C = 0;
            NineV = 0;
            SpyCleaner = 2;
            KeyLoggerCleaner = 2;
            TrojanCleaner = 2;
            RansonCleaner = 2;
            WormCleaner = 2;
            VirusCleaner = 2;
                esperarAtacar = 1f;
                ataqueEsperto = true;
                break;
            case 5:
        
            iaLevel = 4;
            softWax = 2;
            hardWax = 0;
            ironWax = 0;
            AA = 1;
            D = 2;
            C = 0;
            NineV = 0;
            SpyCleaner = 2;
            KeyLoggerCleaner = 2;
            TrojanCleaner = 2;
            RansonCleaner = 2;
            WormCleaner = 2;
            VirusCleaner = 2;
                esperarAtacar = 1f;
                ataqueEsperto = true;
                break;
            case 6:
        
            iaLevel = 4;
            softWax = 0;
            hardWax = 1;
            ironWax = 0;
            AA = 0;
            D = 1;
            C = 1;
            NineV = 0;
            SpyCleaner = Random.Range(1, 4);
            KeyLoggerCleaner = Random.Range(1, 4);
            TrojanCleaner = Random.Range(1, 4);
            RansonCleaner = Random.Range(1, 4);
            WormCleaner = Random.Range(1, 4);
            VirusCleaner = Random.Range(1, 4);
                esperarAtacar = 0.3f;
                ataqueEsperto = true;
                break;
            case 7:
        
            iaLevel = 5;
            softWax = 0;
            hardWax = 2;
            ironWax = 0;
            AA = 0;
            D = 1;
            C = 2;
            NineV = 1;
            SpyCleaner = 2;
            KeyLoggerCleaner = 2;
            TrojanCleaner = 2;
            RansonCleaner = 2;
            WormCleaner = 2;
            VirusCleaner = 2;
                esperarAtacar = 0.3f;
                ataqueEsperto = true;
                break;
            case 8:
        
            iaLevel = 5;
            softWax =0;
            hardWax = 2;
            ironWax = 1;
            AA = 0;
            D = 0;
            C = 1;
            NineV = 1;
            SpyCleaner = Random.Range(1, 4);
            KeyLoggerCleaner = Random.Range(1, 4);
            TrojanCleaner = Random.Range(1, 4);
                RansonCleaner = Random.Range(1, 4);
            WormCleaner = Random.Range(1, 4);
            VirusCleaner = Random.Range(1, 4);
                esperarAtacar = 0.3f;
                ataqueEsperto = true;
                break;
            case 9:
        
            iaLevel = 5;
            softWax = 0;
            hardWax = 1;
            ironWax = 2;
            AA = 0;
            D = 0;
            C = 2;
            NineV = 1;
            SpyCleaner = Random.Range(2, 6);
                KeyLoggerCleaner = Random.Range(2, 6);
                TrojanCleaner = Random.Range(2, 6);
                RansonCleaner = Random.Range(2, 6);
                WormCleaner = Random.Range(2, 6);
                VirusCleaner = Random.Range(2, 6);
                esperarAtacar = 0.3f;
                ataqueEsperto = true;
                break;
    }
        
    }
    void OnEnable()
    {
        iaState = IaState.CARREGANDO;
    }
    // Update is called once per frame
    void Update()
    {
        if (Ia)
        {
            //IaTroca();
            if (!myRobot.KO)
            {
                switch (iaState)
                {
                    case IaState.CARREGANDO:
                        if (myRobot.acoesAtual == myRobot.MyStatus.Acoes - myRobot.keyloggerVariant)
                        {
                            iaState = IaState.ESPERANDO;
                        }
                        break;
                    case IaState.ESPERANDO:
                        if (battleManager.BattleState == BattleManager.BattleStateMachine.START ||
                    battleManager.BattleState == BattleManager.BattleStateMachine.IDLE)
                        {
                            if (myRobot.integridadeAtual > 0 && myRobot.bateriaAtual > 0)
                            {
                                battleManager.EnemyStartTurn();
                                iaState = IaState.AGINDO;
                                contador = 1.5f;
                                contadorseguranca = 0f;
                            }

                        }
                        break;

                    case IaState.AGINDO:
                        contadorseguranca += Time.deltaTime;
                        if (myRobot.acoesAtual == 0)
                        {
                            iaState = IaState.CARREGANDO;                           
                        }
                        if (battleManager.BattleState == BattleManager.BattleStateMachine.ENEMYTURN)
                        {
                            contador += Time.deltaTime;
                            contadorseguranca = 0f;
                            contadorAleatorio = 0;
                            if (contador >= esperarAtacar)
                            {
                                contador = 0f;                                
                                usarArma();
                                if (iaLevel >= 2) { IaITem(); }
                                //if (iaLevel == 5) { IaOverlock(); }
                                if (newCombo) 
                                {                                    
                                    combo = Random.Range(1, 4);
                                    if (combo > 2) { comboID = Random.Range(2, myWeapon.weapon.Combo.Count); }
                                    newCombo = false;
                                    instancia = 0;
                                }
                                if (iaLevel <= 3) { iA1(); }
                                if (iaLevel == 4) { iA2(combo); }
                                if (iaLevel == 5) { iA3(combo); }
                            }

                        }       
                        if(contadorseguranca >=2.5f && battleManager.BattleState != BattleManager.BattleStateMachine.PLAYERTURN && battleManager.BattleState != BattleManager.BattleStateMachine.PLAYERANIMATION 
                            && battleManager.BattleState != BattleManager.BattleStateMachine.ENEMYTURN && battleManager.BattleState != BattleManager.BattleStateMachine.ENEMYANIMATION)
                        {
                            iaState = IaState.CARREGANDO;
                            battleManager.BattleState = BattleManager.BattleStateMachine.START;
                        }
                        break;
                }
            }
        }
    }
    //IA
    public void IaTroca()
    {
        if (myRobot.KO)
        {
            if (nextRobot != null && robotManager.EnemyRobots.Count>1)
            {
                switchRobot(nextRobot);
            }
        }
    }
    private void IaITem()
    {              
        hp = myRobot.integridadeAtual / myRobot.MyStatus.Integridade;
        batterie = myRobot.bateriaAtual / myRobot.MyStatus.Bateria;

        // verifica integridade
        if (hp < 0.7f && softWax > 0) { integridadeRestore(200, 1);softWax--; }
        if (hp < 0.5f && hardWax > 0) { integridadeRestore(600, 1); hardWax--; }
        if (hp < 0.2f && ironWax > 0) { integridadeRestore(10000, 3); ironWax--; }

         //verifica bateria
         if (batterie < 0.8f && AA > 0) { bateriaRecharge(100, 1); AA--; }
         if (batterie < 0.5f && C > 0) { bateriaRecharge(500, 1);C--; }
         if (batterie < 0.3f && D > 0) { bateriaRecharge(1000, 2);D--; }
         if (batterie < 0.1f && NineV > 0) { bateriaRecharge(10000, 3);NineV--; }

         //verifica se esta infectado
         if (myRobot.Spy == true && SpyCleaner > 0) { spyCleaner(1); SpyCleaner--; }
         if (myRobot.Keylogger == true && KeyLoggerCleaner > 0) { keyloggerCleaner(1); KeyLoggerCleaner--; }
         if (myRobot.Trojan == true && TrojanCleaner > 0) { trojanCleaner(1); TrojanCleaner--; }
         if (myRobot.Ranson == true && RansonCleaner > 0) { ransonCleaner(1); RansonCleaner--; }
         if (myRobot.Worm == true && WormCleaner > 0) { wormCleaner(1); WormCleaner--; }
         if (myRobot.Virus == true && VirusCleaner > 0) { virusCleaner(1); VirusCleaner--; }
        
    }
    private void IaOverlock()
    {
        if (StartAttacking && battleManager.EnemyCanAttack && myWeapon.animacaoexecutando == false 
            && battleManager.playerManager.MyWeapon.animacaoexecutando == false 
            && !battleManager.playerManager.MeuTurno)
        {
            hp = myRobot.integridadeAtual / myRobot.MyStatus.Integridade;
            if (!overlock && hp < 0.6f)
            {
                Overlock();
                overlock = true;
            }
        }
    }
    private void iA1()
    {
        if (battleManager.BattleState == BattleManager.BattleStateMachine.ENEMYTURN)
        {
           randomAttack();
        }
    }
    private void iA2(int combo)
    {
        if (battleManager.BattleState == BattleManager.BattleStateMachine.ENEMYTURN)
        {
            if (combo > 2) { combo = 2; }
            if (combo == 1) { smallCombo(); }
            if (combo == 2) { mediumCombo(); }
        }
    }
    private void iA3(int combo)
    {
        if (battleManager.BattleState == BattleManager.BattleStateMachine.ENEMYTURN)
        { 
            //atacar
            if (battleManager.playerManager.broke == true) { combo = 3; }
            if (combo == 1) { smallCombo(); }
            if (combo == 2) { mediumCombo(); }
            if (combo == 3) { largeCombo(); }
        }
    }
    //attack
    private void randomAttack()
    {
        if(contadorAleatorio<10)
        {
            contadorAleatorio++;           
            int attack = Random.Range(0, myWeapon.weapon.Ataque.Length);
            Attack(attack);            
        }
        else
        {
            myRobot.acoesAtual = 0;
        }
      
    }
    private void smallCombo()
    {
        if (instancia == 0) { Attack(myWeapon.weapon.Combo[comboID].ComboID[0] - 1); instancia++; goto exit; }
        if (instancia == 1) { Attack(myWeapon.weapon.Combo[comboID].ComboID[1] - 1); instancia++; goto exit; }
        if (instancia == 2) { Attack(myWeapon.weapon.Combo[comboID].ComboID[2] - 1); instancia = 0; newCombo = true; goto exit; }
    exit:
        ;
    }
    private void mediumCombo()
    {
        if (instancia == 0) { Attack(myWeapon.weapon.Combo[comboID].ComboID[0] - 1); instancia++; goto exit; }
        if (instancia == 1) { Attack(myWeapon.weapon.Combo[comboID].ComboID[1] - 1); instancia++; goto exit; }
        if (instancia == 2) { Attack(myWeapon.weapon.Combo[comboID].ComboID[2] - 1); instancia++; goto exit; }
        if (instancia == 3) { Attack(myWeapon.weapon.Combo[comboID].ComboID[3] - 1); instancia = 0; newCombo = true; goto exit; }
    exit:
        ;
    }
    private void largeCombo()
    {
        if (instancia == 0) { Attack(myWeapon.weapon.Combo[comboID].ComboID[0]-1); instancia++; goto exit; }
        if (instancia == 1) { Attack(myWeapon.weapon.Combo[comboID].ComboID[1] - 1); instancia++; goto exit; }
        if (instancia == 2) { Attack(myWeapon.weapon.Combo[comboID].ComboID[2] - 1); instancia++; goto exit; }
        if (instancia == 3) { Attack(myWeapon.weapon.Combo[comboID].ComboID[3] - 1); instancia++; goto exit; }
        if (instancia == 4) { Attack(myWeapon.weapon.Combo[comboID].ComboID[4] - 1); instancia++; goto exit; }
        if (instancia == 5) { Attack(myWeapon.weapon.Combo[comboID].ComboID[5] - 1); instancia = 0; newCombo = true; goto exit; }
    exit:
        ;

    }
    private void Attack(int attack)
    {
        if (attack >= 0)
        {
            if (myWeapon.weapon.Ataque[attack] != null)
            {
                if(!ataqueEsperto || ataqueEsperto && !battleManager.playerManager.broke && !myWeapon.weapon.Ataque[attack].Elemental || ataqueEsperto && battleManager.playerManager.broke)
                {
                    if (myRobot.acoesAtual == 0)
                    {
                        return;
                    }
                    if (myRobot.acoesAtual - myWeapon.weapon.Ataque[attack].UsoDeAcoes < 0)
                    {
                        randomAttack();
                    }
                    else
                    {
                        myWeapon.UsarAtaque(attack);
                        contadorAleatorio = 0;
                    }
                }
                else
                {
                    randomAttack();
                }               
            }
            else
            {
                randomAttack();
            }
        }
        else
        {
            randomAttack();
        }
    }
    //overlock
    private void Overlock()
    {
       // myRobot.OverlockOn();
    }
    //item using
    private void integridadeRestore(float recover, int gasto)
    {
        myRobot.RecuperaIntegridade(recover);
        myRobot.DiminuiAcoes(gasto);
    }
    private void bateriaRecharge(float recharge, int gasto)
    {
        myRobot.RecuperaBateria(recharge);
        myRobot.DiminuiAcoes(gasto);
    }
    private void spyCleaner(int gasto)
    {
        myRobot.RetiraSpy();
        myRobot.DiminuiAcoes(gasto);
    }
    private void keyloggerCleaner(int gasto)
    {
        myRobot.RetiraKeylogger();
        myRobot.DiminuiAcoes(gasto);
    }
    private void trojanCleaner(int gasto)
    {
        myRobot.RetiraTrojan();
        myRobot.DiminuiAcoes(gasto);
    }
    private void ransonCleaner(int gasto)
    {
        myRobot.RetiraRanson();
        myRobot.DiminuiAcoes(gasto);
    }
    private void wormCleaner(int gasto)
    {
        myRobot.RetiraWorm();
        myRobot.DiminuiAcoes(gasto);
    }
    private void virusCleaner(int gasto)
    {
        myRobot.RetiraVirus();
        myRobot.DiminuiAcoes(gasto);
    }
    //switch robot
    private void switchRobot(string robotTag)
    {
        battleManager.BattleState = BattleManager.BattleStateMachine.ENEMYTRADE;
        //robotManager.IniciarTrocaEnemy(robotTag, false);
        // o active robot manager vai desligar essa IA e ligar a proxima
    }
    public void SelecionaOProximoRobo()
    {
        for (int i = 1; i < robotManager.EnemyRobots.Count; i++)
        {
            if (robotManager.EnemyRobots[i] != null && robotManager.EnemyRobots[i].GetComponent<RobotManager>().Ativo == false
                && robotManager.EnemyRobots[i].GetComponent<RobotManager>().KO == false)
            {
                nextRobot = robotManager.EnemyRobots[i].gameObject.tag;
                break;
            }
        }
    }
    private void usarArma()
    {
        //cortante
        if (myWeapon.MeuCortante.Ativado)
        {
            //LIGAR FRENESI
            if (myWeapon.MeuCortante.ModoAtual == Cortante.Modo.CALMO && myRobot.ResistenciaAtual > myRobot.MyStatus.Resistencia * 0.15f)
            {
                if (iaLevel <= 2 && Random.Range(0,101)>75) { myWeapon.MeuCortante.Clicar(); }
                if (iaLevel>2 && iaLevel<=5)
                {
                    if (myRobot.integridadeAtual >= myRobot.MyStatus.Integridade/2 && Random.Range(0, 101) > 50 || battleManager.playerManager.broke)
                    {
                        myWeapon.MeuCortante.Clicar();
                    }
                }                
            }
            //DESLIGAR FRENESI
            if (myWeapon.MeuCortante.ModoAtual == Cortante.Modo.FRENESI && myRobot.ResistenciaAtual < myRobot.MyStatus.Resistencia * 0.25f)
            {
                if (iaLevel <= 3) { myWeapon.MeuCortante.Clicar(); }
                if (iaLevel > 3 && iaLevel <= 5)
                {
                    if (myRobot.integridadeAtual < myRobot.MyStatus.Integridade / 2 && battleManager.playerManager.broke)
                    {
                        myWeapon.MeuCortante.Clicar();
                    }
                }
            }
        }
        //impacto
        if(myWeapon.MeuImapcto.Ativado)
        {
            //trancar
            if(!myWeapon.MeuImapcto.Trancado)
            {
                if (iaLevel <= 2 && Random.Range(0, 101) > 75) { myWeapon.MeuImapcto.Trancar(); }
                if (iaLevel > 2 && iaLevel <= 5)
                {
                    if (myRobot.broke && myWeapon.MeuImapcto.ModoAtual == Impacto.Modo.DEFESA)
                    {
                        myWeapon.MeuImapcto.Trancar();
                    }
                    else if(battleManager.playerManager.broke && myWeapon.MeuImapcto.ModoAtual == Impacto.Modo.ATAQUE)
                    {
                        myWeapon.MeuImapcto.Trancar();
                    }
                }
            }
        }
        //canhao
        if(myWeapon.MeuCanhao.Ativado)
        {
            if(iaLevel<=3 && myWeapon.MeuCanhao.PodeGuardado>0 && Random.Range(0,101)>75)
            {
                myWeapon.MeuCanhao.Atirar();
            }
            else if(iaLevel>3 && iaLevel<=5 && myWeapon.MeuCanhao.PodeGuardado == 3)
            {
                myWeapon.MeuCanhao.Atirar();
            }
        }
        //cajado
        if (myWeapon.MeuCajado.ativado)
        {
            //LIGARDESCARREGA
            if (myWeapon.MeuCajado.ModoAtual == Cajado.Modo.MODOCARREGA)
            {
                if (iaLevel <= 2 && myWeapon.MeuCajado.PoderCarreagado > 30 && Random.Range(0, 101) > 75)
                {
                    myWeapon.MeuCajado.Trocar();
                }
                else if (iaLevel > 2 && iaLevel <= 5 && myWeapon.MeuCajado.PoderCarreagado > 50)
                {
                    if (Random.Range(0, 101) > 50 || battleManager.playerManager.broke)
                    {
                        myWeapon.MeuCajado.Trocar();
                    }
                }
            }
        }
        //bolha
        if(myWeapon.MinhaBolha.Ativado)
        {
            if(iaLevel<=3 && Random.Range(0, 101) > 75) { myWeapon.MinhaBolha.TocarNotas(); }
            else if(iaLevel>3 && iaLevel<=5)
            {
                if(myRobot.broke || battleManager.playerManager.broke || myRobot.integridadeAtual<myRobot.MyStatus.Integridade/2)
                {
                    myWeapon.MinhaBolha.TocarNotas();
                }
                else
                {
                    int i = 0;
                    foreach (Notas n in myWeapon.MinhaBolha.MinhasNotas)
                    {
                        if (n.meuBuff != 9) { i++; }
                    }
                    if (i >= 3) { myWeapon.MinhaBolha.TocarNotas(); }
                }               
            }

        }
        //escudo
        if(myWeapon.MeuEscudo.Ativado)
        {
            if (iaLevel <= 2) { myWeapon.MeuEscudo.BuffarStatus(); }
            else if (iaLevel > 3 && iaLevel <= 5)
            {
                if (myRobot.broke || battleManager.playerManager.broke || myRobot.integridadeAtual < myRobot.MyStatus.Integridade / 2 || myWeapon.MeuEscudo.EnergiaGuardada==2)
                {
                    myWeapon.MeuEscudo.BuffarStatus();
                }
            }
        }
    }
}

