using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotManager : MonoBehaviour
{
    public GameObject ParticulasDano;
    public GameObject ParticulasCura;
    public GameObject AntiVirus;
    public GameObject FloatText;
    public GameObject OverlockAnimation;    
    
   [HideInInspector]
    public Slider SliderIntegridade;
   [HideInInspector]
    public Slider SliderResistencia;
    [HideInInspector]
    public Slider SliderBateria;
    [HideInInspector]
    public List<Slider> SliderAcoes = new List<Slider>();
    [HideInInspector]
    public Text TextoIntegridadeAtual;
    [HideInInspector]
    public Text TextoResistenciaAtual;
    [HideInInspector]
    public Status MyStatus;
    public Weapon Weapon;
    public WeaponMethods MyWeapon;
    // controla os status atuais
    [HideInInspector]
    public float integridadeAtual;
   [HideInInspector]
    public float bateriaAtual;
    [HideInInspector]
    public float ResistenciaAtual;
    [HideInInspector]
    public float VelocidadeAtual;
    [HideInInspector]
    public float AtaqueAtual;
    [HideInInspector]
    public float AtaqueEnergeticoAtual;
    [HideInInspector]
    public int acoesAtual;
    private float contadorDeAcoes;
    //o metodo keylogger vai retornar um valor sempre que o robo for infectado com este virus;
    [HideInInspector]
    public int keyloggerVariant;
    //essas booleanas vão ativar e desativar de acordo com o robo estar ou não infectado com virus;
    [HideInInspector]
    public bool Spy = false;
    [HideInInspector]
    public bool Keylogger = false;
    [HideInInspector]
    public bool Trojan = false;
    [HideInInspector]
    public bool Ranson = false;
    [HideInInspector]
    public bool Worm = false;
    [HideInInspector]
    public bool Virus = false;
    // verifica se o robo esta com a guarda quebrada
    [HideInInspector]
    public bool broke = false;
    // verifica se ja usou overlock
   [HideInInspector]
    public bool Overlock = false;
    [HideInInspector]
    public bool infectado = false;
    [HideInInspector]
    public AnimationController RobotAnimator;
    [HideInInspector]
    public BattleManager battleManager;
    [HideInInspector]
    public Color MinhaCor;
    private float porcentagemBK = 1f;
    private float porcentagemOV = 1f;
    private float porcentagemIN = 1f;
    [HideInInspector]
    public bool Ativo = false;
    [HideInInspector]
    public TransitionManager screemManager;
    [HideInInspector]
    public GerenciadorDialogo GerenciadorDialogo;
    [HideInInspector]
    public TextoDeBatalha TextoDeBatalha;
   [HideInInspector]
    public bool KO = false;
    public Shake shaker;
    [HideInInspector]
    public bool MeuTurno = false;
    [HideInInspector]
    public SwitchRobot MeuBotao;
    public GameObject FumacaDireita;
    public Transform VaoDireito;
    public GameObject FumacaEsquerda;
    public Transform VaoEsquerdo;
    public bool PlayerRobot;
    private IEnumerator diminuirVida;
    public SpriteRenderer CaixaDeStatus;
    [HideInInspector]
    bool animacaoArma = false;
    [HideInInspector]
    public float AumentoSpy;
    [HideInInspector]
    public float WormPercentual;
    [HideInInspector]
    public float VirusPercentual;
    public ActiveRobotManager ActiveRobotManagerAbertura;
    [HideInInspector]
    public float fatorTrojan;
    [HideInInspector]
    public float fatorRanson;
    // Start is called before the first frame update
    void OnEnable()
    {
        this.GetComponent<Animator>().speed = 1;
        if(ActiveRobotManagerAbertura != null && ActiveRobotManagerAbertura.Abertura)
        {
            PegarValoresStatus();        
        }
        RobotAnimator = GetComponent<AnimationController>();
        if(PlayerRobot && battleManager !=null &&battleManager.EnemyRobot !=null)
        {
            battleManager.EnemyRobot.GetComponent<IA>().iaState = IA.IaState.CARREGANDO;
        }
       
    }
    void Update()
    {
        
        if (Ativo && integridadeAtual > 0 && bateriaAtual >0)
        {
            calculaAcoes();
            VerificarBroke();
            restaurarResistencia();
            if (broke) { animacaoBroke(); }
           // if (Overlock) { animacaoOverlock(); }
            if (infectado) { animacaoInfeccao(); }  
            if(animacaoArma) { animacaoOverlock(); }
        }
    }   
    public void PegarValoresStatus()
    {
        MyStatus = GetComponent<Status>();       
        definirCorDoNucleo();
        integridadeAtual = MyStatus.IntegridadeAtual;        
        bateriaAtual = MyStatus.BateriaAtual;
        ResistenciaAtual = MyStatus.Resistencia;
        VelocidadeAtual = MyStatus.Velocidade;
        AtaqueAtual = MyStatus.Ataque;
        AtaqueEnergeticoAtual = MyStatus.AtaqueEnergetico;
        acoesAtual = MyStatus.Acoes;        
        Weapon = MyStatus.NucleoFisico;
        //Verifica A Situação
        if (MyStatus.Spy) { InfectadoSpy(MyStatus.AumentoSpy);}
        if (MyStatus.Keylogger)
        {
            Keylogger = true;
            infectado = true;
            keyloggerVariant = MyStatus.KeyloggerAtual;
            if (keyloggerVariant > MyStatus.keyloggerVariant) { keyloggerVariant = MyStatus.keyloggerVariant; }
        }
        if (MyStatus.Trojan) { InfectarTrojan(MyStatus.FatorTrojan); };
        if (MyStatus.Ranson) { InfectarRanson(MyStatus.FatorRanson); }
        if (MyStatus.Worm) { InfectadoWorm(MyStatus.WormPercentual); };
        if (MyStatus.Virus) { InfectadoVirus(MyStatus.VirusPercentual); }
        //preenche as barras para iniciar
        PreencheBarras();
    }
    private void definirCorDoNucleo()
    {
        if (MyStatus.NucleoElemental == 0) { MinhaCor = Color.red; }
        if (MyStatus.NucleoElemental == 1) { MinhaCor = Color.blue; }
        if (MyStatus.NucleoElemental == 2) { MinhaCor = Color.yellow; }
        if (MyStatus.NucleoElemental == 3) { MinhaCor = Color.green; }
        if (MyStatus.NucleoElemental == 4) { MinhaCor = new Color (254, 161, 0, 1 ); }
        if (MyStatus.NucleoElemental == 5) { MinhaCor = new Color(143, 0, 254, 1); }
    }
    public void PreencheBarras()
    {
        if (Ativo)
        {           
            //integridade
            if (SliderIntegridade != null)
            {
                SliderIntegridade.value = integridadeAtual;
            }
            if (TextoIntegridadeAtual != null)
            {
                int inte = Mathf.RoundToInt(integridadeAtual);
                TextoIntegridadeAtual.text = inte.ToString();
            }
            //resistencia
            if (SliderResistencia != null)
            {
                SliderResistencia.value = ResistenciaAtual;
            }
            if (TextoResistenciaAtual != null)
            {
                int res = Mathf.RoundToInt(ResistenciaAtual);
                TextoResistenciaAtual.text = res.ToString();
            }
            //bateria
            if (SliderBateria != null)
            {
                SliderBateria.value = bateriaAtual;
            }
            //acoes
            
            if (SliderAcoes.Count > 0)
            {
                
                foreach (Slider acao in SliderAcoes)
                {                    
                        acao.value = 0;
                        acao.maxValue = 100;                    
                }
                for (int i = 0; i < acoesAtual; i++)
                {
                   
                    SliderAcoes[i].value = 100;
                    
                }
            }
        }
             
    }
    public  IEnumerator atualizaBarraIntegridade(float valorinicial, int velocidade)
    {
        float diferenca = 0;        
        float i = 1;  
        if (integridadeAtual>valorinicial) { i = 1; diferenca = integridadeAtual-valorinicial; }
        if( valorinicial > integridadeAtual) { i=-1; diferenca = valorinicial - integridadeAtual; }        
        if (diferenca>0)
        {
            while (diferenca > 0)
            {               
                if (SliderIntegridade != null)
                {
                    SliderIntegridade.value += i * velocidade * Time.deltaTime;
                }
                if (TextoIntegridadeAtual != null)
                {
                    TextoIntegridadeAtual.text = SliderIntegridade.value.ToString();
                }
                diferenca -= 1 * velocidade * Time.deltaTime;
                yield return null;
            }
        }       
        //ajuste final
        if (SliderIntegridade != null)
        {
            SliderIntegridade.value = integridadeAtual;
        }
        if (TextoIntegridadeAtual != null)
        {
            int valor = (int)SliderIntegridade.value;
            TextoIntegridadeAtual.text = valor.ToString();
        }        
        if (SliderIntegridade.value == 0)
        {           
            IniciarTroca();
        }

        if (MeuBotao !=null)
        {
            MeuBotao.Atualizar();
        }        
    }
    void IniciarTroca()
    {        
        SomFantoRob.TocarSomPerdeu();
        
        if (PlayerRobot)
        {
            battleManager.BattleState = BattleManager.BattleStateMachine.PLAYERTRADE;
        }
        else
        {
            battleManager.BattleState = BattleManager.BattleStateMachine.ENEMYTRADE;
        }
        screemManager.EsconderCaixaDeAcao();
        screemManager.EsconderMenuAtaques();
        screemManager.EsconderMenuDeTroca();
        screemManager.EsconderMenuItem();
        this.GetComponent<Animator>().SetTrigger("perdeu");
    }
    public void atualizaBarraResistencia()
    {
        if (SliderResistencia != null)
        {
            SliderResistencia.value = ResistenciaAtual;
        }
        if (TextoResistenciaAtual != null)
        {            
            int valor = (int)ResistenciaAtual;
            TextoResistenciaAtual.text = valor.ToString();
        }
        if (MeuBotao != null)
        {
            MeuBotao.Atualizar();       
        }
    }
    public void atualizaBarraBateria()
    {
      
        if (SliderBateria != null)
        {
            SliderBateria.value = bateriaAtual;
        }


        if(bateriaAtual == 0) 
        {
            KO = true;
            IniciarTroca(); 
        }
        if (MeuBotao != null)
        {
            MeuBotao.Atualizar();
        }
    }
    private void calculaAcoes()
    {
        if (Ativo)
        {
            if (acoesAtual < MyStatus.Acoes - keyloggerVariant && battleManager.BattleState == BattleManager.BattleStateMachine.START
                 || acoesAtual < MyStatus.Acoes - keyloggerVariant && battleManager.BattleState == BattleManager.BattleStateMachine.IDLE)
            {
                contadorDeAcoes += Time.deltaTime * VelocidadeAtual;
                if (SliderAcoes.Count > 0)
                {
                    atualizaBarraAcoes(contadorDeAcoes);
                }
                if (contadorDeAcoes >= 100)
                {
                    acoesAtual++;
                    contadorDeAcoes = 0;
                }
            }
        }
    }
    private void atualizaBarraAcoes(float valor)
    {
        for (int i = 0; i <= acoesAtual; i++)
        {
            if (i == 6) { break; }
            if (SliderAcoes[i].value < 99.95)
            {
                SliderAcoes[i].value = valor;
            }
        }
    }
    public void DiminuiAcoes(int acao)
    {
        if (SliderAcoes.Count > 0)
        {
            for (int i = acoesAtual - 1; i > acoesAtual - 1 - acao; i -= 1)
            {
                SliderAcoes[i].value = 0;
                if (i != 5)
                {
                    if (screemManager.SliderAcoes[i + 1].value < 99.95)
                    {
                        screemManager.SliderAcoes[i + 1].value = 0;
                    }
                }
            }
        }          
        acoesAtual -= acao;
    }    
    public void GastoBateria(float gasto)
    {
        float valor = bateriaAtual;
        bateriaAtual -= gasto;
        if (bateriaAtual < 0) { bateriaAtual = 0; }
        
            atualizaBarraBateria();
               
    }
    private void VerificarBroke()
    {
        if (ResistenciaAtual <= 0)
        {
            broke = true;
            MyWeapon.DesativarCortanteQuebrado();
        }
    }
    private void animacaoBroke()
    {
        porcentagemBK -= 0.75f * Time.deltaTime;
        Color corAtual = Color.Lerp(MyStatus.Broke, Color.white, porcentagemBK);
        CaixaDeStatus.color = corAtual;
        if (porcentagemBK <= 0)
        {
            porcentagemBK = 1f;
        }
    }
    private void restaurarResistencia()
    {
        if (broke)
        {
            if(battleManager.BattleState == BattleManager.BattleStateMachine.IDLE || battleManager.BattleState == BattleManager.BattleStateMachine.START)
            {
                ResistenciaAtual += Time.deltaTime * VelocidadeAtual / MyStatus.FatordeRecuperação;
                atualizaBarraResistencia();
            }            
        }
        if (ResistenciaAtual > MyStatus.Resistencia)
        {
            ResistenciaAtual = MyStatus.Resistencia;
            broke = false;
            CaixaDeStatus.color = Color.white;
            atualizaBarraResistencia();
            //desliga animacao de broke
        }
    }
    public void TomarDano(float danoIntegridade, float danoResistencia, int tipodedano)
    {
        
        if(diminuirVida != null)
        {
            StopCoroutine(diminuirVida);
        }
        float integridadenoinicio = SliderIntegridade.value;       
        Instantiate(ParticulasDano, transform.position, Quaternion.identity);        
        
        switch(tipodedano)
        {
            case 0:
              
                SomFantoRob.NElementalNEfetivo();
                GerenciadorDialogo.DigitarNaTela(TextoDeBatalha.textos[2]);
                TextoDeBatalha.TextoPadrao = false;
                FloatTextON("1/2x");
                break;
            case 1:
                shaker.MechidaForte();
                SomFantoRob.NElementalEfetivo();
                GerenciadorDialogo.DigitarNaTela(TextoDeBatalha.textos[3]);
                TextoDeBatalha.TextoPadrao = false;
                if (!PlayerRobot) { ManagerGame.Instance.SuperEFetivo(); }
                FloatTextON("1x");
                break;
            case 2:                
                SomFantoRob.ElementalNEfetivo();
                GerenciadorDialogo.DigitarNaTela(TextoDeBatalha.textos[3]);
                TextoDeBatalha.TextoPadrao = false;
                FloatTextON("1x");
                break;
            case 3:
                shaker.MechidaForte();
                SomFantoRob.ElementEfetivo();
                GerenciadorDialogo.DigitarNaTela(TextoDeBatalha.textos[4]);
                TextoDeBatalha.TextoPadrao = false;
                FloatTextON("2x");
                if (!PlayerRobot) { ManagerGame.Instance.SuperEFetivo(); }
                break;
        }
        //nfEscudo
        MyWeapon.MeuEscudo.GuardarEnergia(danoResistencia, MyStatus.Integridade);
        
        //nfimpacto
        if(MyWeapon.weapon.Model==1)
        {
            danoIntegridade = MyWeapon.MeuImapcto.CalculaDano(danoIntegridade);            
            danoResistencia = MyWeapon.MeuImapcto.CalculaDano(danoResistencia);           
        }
        if (danoIntegridade > integridadeAtual)
        {            
            danoIntegridade = integridadeAtual;
            //KO = true;
        }
        if (danoResistencia > ResistenciaAtual)
        { danoResistencia = ResistenciaAtual + 10; }

        if (broke)
        {
            if (integridadeAtual < 1) { danoIntegridade = 1; }
            integridadeAtual -= danoIntegridade;
        }
        else
        {
            if (integridadeAtual < 1) { danoIntegridade = 1; }
            ResistenciaAtual -= danoResistencia;
            integridadeAtual -= danoIntegridade * 0.4f;
        }
        float dano = danoIntegridade + danoResistencia;
        int danofloat = (int)dano;
        atualizaBarraResistencia();
        diminuirVida = atualizaBarraIntegridade(integridadenoinicio, MyStatus.Integridade / 2);
        StartCoroutine(diminuirVida);
        if (battleManager.BattleState == BattleManager.BattleStateMachine.PLAYERANIMATION)
        {
            battleManager.BattleState = BattleManager.BattleStateMachine.PLAYERTURN;
        }
        if (battleManager.BattleState == BattleManager.BattleStateMachine.ENEMYANIMATION)
        {
            battleManager.BattleState = BattleManager.BattleStateMachine.ENEMYTURN;
        }
    }
    public void OverlockOn()
    {
        if (Overlock == false)
        {
            battleManager.BattleState = BattleManager.BattleStateMachine.OTHERANIMATION;
            Instantiate(OverlockAnimation, transform.position, Quaternion.identity).GetComponent<Overlock>().battleManager = battleManager;
            Overlock = true;
            ResistenciaAtual *= 1 + MyStatus.ResistenciaOV;
            VelocidadeAtual *= 1 + MyStatus.VelocidadeOV;
            AtaqueAtual *= 1 + MyStatus.AtaqueOV;
            AtaqueEnergeticoAtual *= 1 + MyStatus.AtaqueEnergeticoOV;
            MyWeapon.AumentarGasto(MyStatus.EnergiaOV);
        }
    }
    public void OverlockOFF()
    {
        ResistenciaAtual = (float)MyStatus.Resistencia;
        VelocidadeAtual = (float)MyStatus.Velocidade;
        AtaqueAtual = (float)MyStatus.Ataque;
        AtaqueEnergeticoAtual = (float)MyStatus.AtaqueEnergetico;
        CaixaDeStatus.color = Color.white;
        MyWeapon.DiminuirGasto();
    }
    private void animacaoOverlock()
    {      
        porcentagemOV -= 0.75f * Time.deltaTime;
        Color corAtual = Color.Lerp(MyStatus.Overlock, Color.white, porcentagemOV);
        CaixaDeStatus.color = corAtual;  
        if (porcentagemOV <= 0)
        {
            porcentagemOV = 1f;
        }
    }
    //essa parte cuida do efeito de infecção
    public void InfectadoSpy(float aumento)
    {
        Spy = true;
        infectado = true;
        MyWeapon.AumentarGasto(aumento);
        MyStatus.AumentoSpy = aumento;
    }
    public void InfectadoKeylogger()
    {
        Keylogger = true;
        infectado = true;
        if (keyloggerVariant <= MyStatus.keyloggerVariant)
        {
            keyloggerVariant++;
        }
        else
        {
            JaInfectado();
        }
    }
    private void infectadoTrojan()
    {      
       if(integridadeAtual>1 && Trojan)
        {
            integridadeAtual -= fatorTrojan;            
            StartCoroutine(atualizaBarraIntegridade(integridadeAtual, 1));
        }  
       else
        {
            RetiraTrojan();
        }
    }
    public void InfectarTrojan(float fator)
    {
        fatorTrojan = fator;
        infectado = true;
        Trojan = true;
        if(PlayerRobot)
        {
            battleManager.JogadorFimTurno += infectadoTrojan;
        }
        else
        {
            battleManager.RivalFimTurno += infectadoTrojan;
        }
    }
    private void infectadoRanson()
    {
        if(ResistenciaAtual>0)
        {
            ResistenciaAtual -= fatorRanson;
            atualizaBarraResistencia();
        }
        else
        {
            RetiraRanson();
        }
           
    }
    public void InfectarRanson(float fator)
    {
        fatorRanson = fator;
        infectado = true;
        Ranson = true;
        if (PlayerRobot)
        {
            battleManager.JogadorFimTurno += infectadoRanson;
        }
        else
        {
            battleManager.RivalFimTurno += infectadoRanson;
        }
    }
    public void InfectadoWorm(float percentualReducao)
    {
        Worm = true;       
        if (VelocidadeAtual > (float)MyStatus.Velocidade / 2)
        {
            VelocidadeAtual *= 1 - percentualReducao;
            infectado = true;
            WormPercentual += percentualReducao;
        }        
    }    
    public void InfectadoVirus(float percentualReducao)
    {
        Virus = true;
        VirusPercentual += percentualReducao;
        if ((float)AtaqueAtual > (float)MyStatus.Ataque / 2)
        {
            AtaqueAtual *= 1 - percentualReducao/100;
            infectado = true;
        }
        if ((float)AtaqueEnergeticoAtual > (float)MyStatus.AtaqueEnergetico / 2)
        {
            AtaqueEnergeticoAtual *= 1 - percentualReducao/100;
            infectado = true;
        }
    }
    private void animacaoInfeccao()
    {
        porcentagemIN -= 0.75f * Time.deltaTime;
        Color corAtual = Color.Lerp(MyStatus.Infeccao, Color.white, porcentagemIN);
        CaixaDeStatus.color = corAtual;
        if (porcentagemIN <= 0)
        {
            porcentagemIN = 1f;
        }
    }
    //essa parte cuida do efeito de uso de itens
    public void RetiraSpy()
    {
        antivirus();
        Spy = false;
        MyWeapon.DiminuirGasto();
        infectado = false;
        MyStatus.AumentoSpy = 0;
    }
    public void RetiraKeylogger()
    {
        Keylogger = false;
        antivirus();
        keyloggerVariant = 0;
        infectado = false;
    }
    public void RetiraTrojan()
    {
        antivirus();
        Trojan = false;
        infectado = false;
        if (PlayerRobot)
        {
            battleManager.JogadorFimTurno -= infectadoTrojan;
        }
        else
        {
            battleManager.RivalFimTurno -= infectadoTrojan;
        }
    }
    public void RetiraRanson()
    {
        antivirus();
        Ranson = false;
        infectado = false;
        if (PlayerRobot)
        {
            battleManager.JogadorFimTurno -= infectadoRanson;
        }
        else
        {
            battleManager.RivalFimTurno -= infectadoRanson;
        }
    }
    public void RetiraWorm()
    {
        antivirus();
        Worm = false;
        VelocidadeAtual = (float)MyStatus.Velocidade;
        infectado = false;
        WormPercentual = 0;
    }
    public void RetiraVirus()
    {
        antivirus();
        Virus = false;
        AtaqueAtual = (float)MyStatus.Ataque;
        AtaqueEnergeticoAtual = (float)MyStatus.AtaqueEnergetico;
        infectado = false;
        VirusPercentual = 0;
    }
    public void RecuperaIntegridade(float recurepacao)
    {
        SomFantoRob.AumentarBarra();
        float valor = integridadeAtual;
        particulasCura();
        integridadeAtual += recurepacao;
        if (integridadeAtual > (float)MyStatus.Integridade)
        {
            integridadeAtual = (float)MyStatus.Integridade;
        }
        StartCoroutine(atualizaBarraIntegridade(valor, MyStatus.Integridade / 5));
    }
    public void RecuperaBateria(float recurepacao)
    {
        SomFantoRob.AumentarBarra();
        float valor = bateriaAtual;
        particulasCura();
        bateriaAtual += recurepacao;
        if (bateriaAtual > (float)MyStatus.Bateria)
        {
            bateriaAtual = (float)MyStatus.Bateria;
        }
        atualizaBarraBateria();
    }    
    public void FloatTextON(string text)
    {
        GameObject floattext = Instantiate(FloatText, ManagerGame.Instance.Regiao.Posicao.transform) as GameObject;
        floattext.transform.position = new Vector3(transform.position.x, transform.position.y + 2, 0);
        floattext.transform.GetChild(0).GetComponent<Text>().text = text;
    }
    public void LowBatterieVerify()
    {
        if(bateriaAtual ==0)
        {
            KO = true;
            RobotAnimator.AnimacaoOn("TurnOff");
        }
    }
    private void KoHandler()
    {
        this.GetComponent<Animator>().speed = 0;
        KO = true;      
        battleManager.ActiveRobotManager.RobotVerification(this.gameObject);
        if(MyWeapon.Diferenciado!=null)
        {
            MyWeapon.Diferenciado.KoFantoRob(PlayerRobot);
        }
        if(!PlayerRobot)
        {
            ManagerGame.Instance.FantorobKO(MyStatus.Modelo, MyStatus.NucleoFisico.Model,MyStatus.NucleoElemental) ;
            RobotManager r = battleManager.playerManager;
            ManagerGame.Instance.FantorobVence(r.MyStatus.Modelo, r.MyStatus.NucleoFisico.Model, r.MyStatus.NucleoElemental);
        }
        
    }
    private void particulasCura()
    {        
        Instantiate(ParticulasCura, transform.position, Quaternion.identity);
    }
    private void antivirus()
    {
        if (!GameObject.Find("Scan"))
        {
            Instantiate(AntiVirus, transform).GetComponent<Scan>().MyWeapon = MyWeapon;
            CaixaDeStatus.color = Color.white;
        }
    }
    public void SoltarFumaca()
    {        
        Instantiate(FumacaDireita,VaoDireito,false).GetComponent<Fumaca>().bm = battleManager;
        Instantiate(FumacaEsquerda, VaoEsquerdo,false);

    }
    public void SomNegado()
    {
        SomFantoRob.SomNegado();
    }
    public void ParticulaAcao()
    {
        Instantiate(OverlockAnimation, transform.position, Quaternion.identity).GetComponent<Overlock>().battleManager = battleManager;
    }
    public void AnimacaoArmaOn()
    {
        animacaoArma = true;
    }
    public void  AnimacaoArmaOff()
    {
        animacaoArma = false;
        CaixaDeStatus.color = Color.white;
    }
    
    public void JaInfectado()
    {
        GerenciadorDialogo.DigitarNaTela(TextoDeBatalha.textos[9]);
        TextoDeBatalha.TextoPadrao = false;
    }
}


