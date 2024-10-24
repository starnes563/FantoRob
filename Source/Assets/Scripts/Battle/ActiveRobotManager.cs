using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveRobotManager : MonoBehaviour
{
    //variaveis para ativar e desativar robos
    public Shake MecherACamera;
    public GerenciadorDialogo GerenciadorDialogo;
    public TextoDeBatalha TextoDeBatalha;
    // declarar os tres robos do player
    private ObjectsDatabase Objects;
    [HideInInspector]
    public List<GameObject> PlayerRobots = new List<GameObject>();
    [HideInInspector]
    public List<GameObject> EnemyRobots = new List<GameObject>();
    [HideInInspector]
    public BattleManager battleManager;
    private TransitionManager screemManager;
    // criar um sistema de tags para a batalha no editor
    [HideInInspector]
    public GameObject ActivePlayerRobot;
    // declarar qual robo esta ativo enemy
    [HideInInspector]
    public GameObject ActiveEnemyRobot;
    //animacoes
    public GameObject AnimacaoTrocarRoboPlayer;
    public GameObject AnimacaoTrocarRoboEnemy;
    //variaveis de troca
    private GameObject proximoRoboPlayer;
    private GameObject proximoRoboenemy;
    public bool Abertura = false;
    [HideInInspector]
    public bool CanIStart = false;
    private bool acabouBatalha = false;
    public GameObject PosicaoAbertura;
    public bool Tesourado = false;
    // Start is called before the first frame update
    private void Awake()
    {
        screemManager = GetComponent<TransitionManager>();
        Objects = GetComponent<ObjectsDatabase>();
        battleManager = GetComponent<BattleManager>();
    }
    void OnEnable()
    {
        StartBat();
        CanIStart = false;
        acabouBatalha = false;
    }
    public void StartBat()
    {
        //fill player robots
        if (!Abertura) { PlayerRobots.Clear(); }      
        foreach (GameObject robot in screemManager.PlayerRobots)
        {
            PlayerRobots.Add(robot);
        }        
        //fill enemy robots
        if (!Abertura)
        {
            EnemyRobots.Clear();
        }       
        foreach (GameObject robot in screemManager.EnemyRobots)
        {
            EnemyRobots.Add(robot);
        }        
        //preencher o robo ativo do player
        ActivePlayerRobot = PlayerRobots[0];        
        screemManager.MontarStatusPlayer(ActivePlayerRobot.GetComponent<Status>(), ActivePlayerRobot.GetComponent<RobotManager>());       
        ativarRobo(ActivePlayerRobot, false);
        //preencher o robo ativo do enemy
        ActiveEnemyRobot = EnemyRobots[0];       
        screemManager.MontarStatusEnemy(ActiveEnemyRobot.GetComponent<Status>(), ActiveEnemyRobot.GetComponent<RobotManager>());
        ativarRobo(ActiveEnemyRobot, true);
        //screemManager.AtualizarMenuAtaques(ActivePlayerRobot.GetComponent<Status>().NucleoFisico, ActivePlayerRobot.GetComponent<WeaponMethods>());
        screemManager.CreateSwitchMenu();
        //battleManager.GetRobots();      
       
       
    }
    private void Update()
    {
        if(CanIStart && !Abertura)
        {
            IniciarBatalha();
            CanIStart = false;
        }
    }
    public void IniciarBatalha()
    {
        AnimacaoTrocaPlayer tr = Instantiate(AnimacaoTrocarRoboPlayer, ManagerGame.Instance.Regiao.Posicao.transform).GetComponent<AnimacaoTrocaPlayer>();
        tr.manager = this;
        tr.tmanager = screemManager;
        tr.ComecoBatalha = true;

        AnimacaoTrocaEnemy te = Instantiate(AnimacaoTrocarRoboEnemy, ManagerGame.Instance.Regiao.Posicao.transform).GetComponent<AnimacaoTrocaEnemy>();
        te.manager = this;
        te.tmanager = screemManager;
        te.ComecoBatalha = true;
    }
    public void IniciarTrocaPlayer(GameObject robo, bool fimdebatalha)
    {
        proximoRoboPlayer = robo;
        screemManager.EsconderCaixaDeAcao();
        screemManager.EsconderMenuAtaques();
        screemManager.EsconderMenuDeTroca();
        screemManager.EsconderMenuItem();
        ActivePlayerRobot.GetComponent<WeaponMethods>().LimparArma();
        AnimacaoTrocaPlayer tr;
        if (!Abertura)
        {
            tr = Instantiate(AnimacaoTrocarRoboPlayer, ManagerGame.Instance.Regiao.Posicao.transform).GetComponent<AnimacaoTrocaPlayer>();
        }
        else
        {
            tr = Instantiate(AnimacaoTrocarRoboPlayer, PosicaoAbertura.transform).GetComponent<AnimacaoTrocaPlayer>();
        }
       
        tr.manager = this;
        tr.FimdeBatalha = fimdebatalha;
    }
    public void TrocarDeRoboPlayer()
    {
        //desativa atual
       desativarRobo(ActivePlayerRobot, false);
        screemManager.EsconderCaixaDeAcao();
        screemManager.EsconderMenuAtaques();
        screemManager.EsconderMenuDeTroca();
        screemManager.EsconderMenuItem();
       screemManager.ZerarBarraDeAcao();
        //ativa o proximo
        if (proximoRoboPlayer !=null)
        {
            ActivePlayerRobot = proximoRoboPlayer;
                
            battleManager.GetRobots();            
            screemManager.AtualizarMenuAtaques(ActivePlayerRobot.GetComponent<RobotManager>().Weapon, ActivePlayerRobot.GetComponent<WeaponMethods>());            
            screemManager.MontarStatusPlayer(ActivePlayerRobot.GetComponent<Status>(), ActivePlayerRobot.GetComponent<RobotManager>());           
            ativarRobo(ActivePlayerRobot, false);
            RoboPlayerAparecer();            
        }       
    }
    public void IniciarTrocaEnemy(GameObject robo, bool fimdebatalha)
    {
        screemManager.EsconderCaixaDeAcao();
        screemManager.EsconderMenuAtaques();
        screemManager.EsconderMenuDeTroca();
        screemManager.EsconderMenuItem();
        ActiveEnemyRobot.GetComponent<WeaponMethods>().LimparArma();
        if (!fimdebatalha)
        {
            proximoRoboenemy = robo;
            proximoRoboenemy.GetComponent<RobotManager>().acoesAtual = 2;
        }
        else
        {
            proximoRoboenemy = null;
        }
        AnimacaoTrocaEnemy tr;
        if (!Abertura)
        {
            tr = Instantiate(AnimacaoTrocarRoboEnemy, ManagerGame.Instance.Regiao.Posicao.transform).GetComponent<AnimacaoTrocaEnemy>();
        }
        else
        {
            tr = Instantiate(AnimacaoTrocarRoboEnemy, PosicaoAbertura.transform).GetComponent<AnimacaoTrocaEnemy>();
        }
        
        tr.manager = this;
        tr.FimdeBatalha = fimdebatalha;
    }
    public void TrocarDeRoboEnemy()
    {
        //desativa atual
        if (!Tesourado)
        {
            desativarRobo(ActiveEnemyRobot, true);
        }
        
        //ativa o proximo        
        if(proximoRoboenemy != null)
        {
            ActiveEnemyRobot = proximoRoboenemy;
            ativarRobo(ActiveEnemyRobot, true);            
            battleManager.GetRobots();                     
            screemManager.MontarStatusEnemy(ActiveEnemyRobot.GetComponent<Status>(), ActiveEnemyRobot.GetComponent<RobotManager>());
            screemManager.AtualizarMenuAtaques(ActivePlayerRobot.GetComponent<RobotManager>().Weapon, ActivePlayerRobot.GetComponent<WeaponMethods>());           
            RoboEnemyAparecer();
            
        }        
    }
    private void ativarRobo(GameObject robo, bool Ia)
    {
        //robotmanager
        RobotManager manager = robo.GetComponent<RobotManager>();
        manager.Ativo = true;      
        manager.shaker = MecherACamera;
        manager.battleManager = battleManager;
        manager.screemManager = screemManager;
        manager.GerenciadorDialogo = GerenciadorDialogo;
        manager.TextoDeBatalha = TextoDeBatalha;
        manager.PreencheBarras();
        //WeaponMethods
        WeaponMethods wp = robo.GetComponent<WeaponMethods>();
        wp.battleManager = battleManager;
        wp.Diferenciado = GetComponent<ManDiferenciado>();
        wp.gerenciadorDialogo = GerenciadorDialogo;
        wp.textoDeBatalha = TextoDeBatalha;

        //Ia
        if(Ia)
        {
            IA ia = robo.GetComponent<IA>();
            ia.battleManager = battleManager;
            ia.robotManager = this;
            ia.Ia = true;
            ia.SelecionaOProximoRobo();
        }       
    }
    public void RoboPlayerAparecer()
    {        
        ActivePlayerRobot.SetActive(true);
       
    }
    public void RoboEnemyAparecer()
    {        
        ActiveEnemyRobot.SetActive(true);
        
    }
    private void desativarRobo(GameObject robo, bool Ia)
    {
        //gameobject
        robo.SetActive(false);

        //robotmanager
        RobotManager manager = robo.GetComponent<RobotManager>();
        manager.Ativo = false;                
        manager.shaker = null;
        manager.battleManager = null;
        manager.screemManager = null;
        manager.GerenciadorDialogo = null;
        manager.TextoDeBatalha = null;
        screemManager.LimparMenu(robo.GetComponent<RobotManager>());
        //WeaponMethods
        WeaponMethods wp = robo.GetComponent<WeaponMethods>();
        wp.battleManager = null;
        wp.gerenciadorDialogo = null;
        wp.textoDeBatalha = null;

        //Ia
        if (Ia)
        {
            IA ia = robo.GetComponent<IA>();
            ia.battleManager = null;
            ia.robotManager = null;
            ia.Ia = false;
            ia.nextRobot = null;
        }
        
    }
    public void RobotVerification(GameObject Fantorob)
    {        
        //verifica se o player tem robos ativos
        if (!acabouBatalha)
        {           
            if (Fantorob.GetComponent<RobotManager>().PlayerRobot == true)
            {
                int contadorKOPlayer = 0;
                foreach (GameObject robot in PlayerRobots)
                {
                    if (robot.GetComponent<RobotManager>().KO)
                    {
                        contadorKOPlayer++;
                    }
                }
                if (contadorKOPlayer == PlayerRobots.Count)
                {
                    IniciarTrocaPlayer(null, true);
                    acabouBatalha = true;
                }
                else
                {
                    battleManager.BattleState = BattleManager.BattleStateMachine.PLAYERTRADE;
                    screemManager.EsconderCaixaDeAcao();
                    screemManager.MostrarMenuDeTroca(false);
                }
            }
            else
            {
                //verifica se o rival ainda tem fantorob
                int contadorKOEnemy = 0;
                foreach (GameObject robot in EnemyRobots)
                {
                    if (robot.GetComponent<RobotManager>().KO)
                    {
                        contadorKOEnemy++;
                    }
                }
                if (contadorKOEnemy == EnemyRobots.Count)
                {
                    IniciarTrocaEnemy(null, true);
                    acabouBatalha = true;
                }
                else
                {                    
                    battleManager.BattleState = BattleManager.BattleStateMachine.ENEMYTRADE;
                    int id = EnemyRobots.IndexOf(ActiveEnemyRobot) + 1;
                    IniciarTrocaEnemy(EnemyRobots[id], false);
                }
            }
        }
    }
    public void PlayerWins()
    {
        screemManager.PlayerWins();
    }
    public void PlayerLoses()
    {
        screemManager.PlayerLoses();
    }
}
