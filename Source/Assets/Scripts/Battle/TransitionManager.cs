using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    public GameObject Darken;
    public GameObject CaixaDeAcao;
    public GameObject CaixaDeTroca;
    public BtCancelarTroca CancelarTroca;
    public GameObject CaixaDeItem;
    public GameObject CaixaDeAtaques;
    //Status Player
    public Text NomeDoRobo;
    public List<GameObject> NucleoDoRobo;
    public Slider SliderBateria;
    public Slider SliderIntegridadeJogador;
    public Slider SliderResistenciaJogador;
    public Text IntegridadeAtualJogador;
    public Text IntegridadeTotalJogador;
    public Text ResistenciaAtualJogador;
    public Text ResistenciaTotalJogador;
    public Button BotaoOverlock;
    public Slider[] SliderAcoes = new Slider[6];
    //StatusEnemy
    public Slider SliderBateriaInimigo;
    public Slider SliderIntegridadeInimigo;
    public Slider SliderResistenciaInimigo;
    public Text NomeDoRoboEnemy;
    private ActiveRobotManager ActiveRobotManager;
    private BattleManager battleManager;
    private ObjectsDatabase objectsDatabase;
    [HideInInspector]
    public bool possoexecutar = false;
    [HideInInspector]
    public bool possoexecutaritem = false;
    //[HideInInspector]
    public List<GameObject> PlayerRobots = new List<GameObject>();
    //[HideInInspector]
    public List<GameObject> EnemyRobots = new List<GameObject>();
    public float VelocidadeDosMenus;
    public bool Abertura = true;
    public List<GameObject> NucleosElementaisPlayer;
    public List<GameObject> NucleosFisicoPlayer;
    public List<GameObject> NucleosElementaisRival;
    public List<GameObject> NucloesFisicosRival;
    public GameObject OvalBase;
    private LootManager loot;
    public GameObject TelaFim;
    private GameObject tela;
    public SpriteRenderer CaixaStatusJogador;
    public SpriteRenderer CaixaStatusRival;
    public UIFisico UIFisico;
    //0 - desvantagem
    //1- equilibrado
    //2 - vantagem
    public List<TextoRelacao> TextoRelacaos = new List<TextoRelacao>(3);
    // Start is called before the first frame update
    void Awake()
    {
        objectsDatabase = GetComponent<ObjectsDatabase>();
        ActiveRobotManager = GetComponent<ActiveRobotManager>();
        battleManager = GetComponent<BattleManager>();
    }
    public void IniciarFantorobs()
    {
        objectsDatabase = GetComponent<ObjectsDatabase>();
        //Instancia Robos do Player
        PlayerRobots.Clear();
        foreach (FantoRob rob in PlayerObjects.RobotsInUse)
        {
            GameObject fanto = Instantiate(objectsDatabase.PlayerRobots[rob.Modelo], ManagerGame.Instance.Regiao.Posicao.transform);
            PlayerRobots.Add(fanto);
            AdicionarStatusFantorob(rob, fanto);
        }
        //Instancia Robos do Enemy
        EnemyRobots.Clear();
        foreach (FantoRob rob in ManagerGame.Instance.RobotsToBattle)
        {
            GameObject fanto = Instantiate(objectsDatabase.EnemyRobots[rob.Modelo],ManagerGame.Instance.Regiao.Posicao.transform);
            EnemyRobots.Add(fanto);
            AdicionarStatusFantorob(rob, fanto);
            fanto.GetComponent<IA>().Dificulty = ManagerGame.Instance.IAProximaBatalha;
        }
    }
    void AdicionarStatusFantorob(FantoRob rob, GameObject FantorobBatalha)
    {
        FantorobBatalha.SetActive(false);
        Status status = FantorobBatalha.GetComponent<Status>();
        status.SNucleo = rob.SpriteElemento;
        status.SArma = rob.SpriteFisico;
        status.Integridade = rob.Integridade;
        status.Bateria = rob.Bateria;
        status.Resistencia = rob.Resistencia;
        status.Velocidade = rob.Velocidade;
        status.Ataque = rob.Ataque;
        status.AtaqueEnergetico = rob.AtaqueElemental;
        status.IntegridadeAtual = rob.IntegridadeAtual;
        status.BateriaAtual = rob.BateriaAtual;
        status.NucleoFisico = rob.Fisico;
        status.Acoes = rob.Acoes;
        status.NucleoElemental = rob.Elemento;
        status.SpriteNucleo = rob.SpriteElemento;
        status.keyloggerVariant = rob.keyloggerVariant;
        status.EnergiaOV = rob.EnergiaOV;
        status.ResistenciaOV = rob.ResistenciaOV;
        status.VelocidadeOV = rob.VelocidadeOV;
        status.AtaqueOV = rob.AtaqueOV;
        status.AtaqueEnergeticoOV = rob.AtaqueEnergeticoOV;
        status.FatordeRecuperação = rob.FatordeRecuperação;
        status.Broke = rob.Broke;
        status.Overlock = rob.Overlock;
        status.Infeccao = rob.Infeccao;
        status.Spy = rob.Spy;
        status.Keylogger = rob.Keylogger;
        status.KeyloggerAtual = rob.KeyloggerAtual;
        status.Trojan = rob.Trojan;
        status.FatorTrojan = rob.FatorTrojan;
        status.Ranson = rob.Ranson;
        status.Virus = rob.Virus;
        status.Worm = rob.Worm;
        status.AumentoSpy = rob.AumentoSpy;
        status.WormPercentual = rob.WormPercentual;
        status.VirusPercentual = rob.VirusPercentual;
        status.Modelo = rob.Modelo;
        FantorobBatalha.GetComponent<RobotManager>().battleManager = battleManager;
        FantorobBatalha.GetComponent<RobotManager>().Ativo = false;
        FantorobBatalha.GetComponent<RobotManager>().PegarValoresStatus();
        if (FantorobBatalha.GetComponent<IA>())
        {
            FantorobBatalha.GetComponent<IA>().Ia = false;
            FantorobBatalha.GetComponent<IA>().Dificulty = ManagerGame.Instance.IAProximaBatalha;
        }
        FantorobBatalha.SetActive(false);
    }
    void OnEnable()
    {        
        CreateItemMenu();
        CreateSwitchMenu();
        if(!Abertura)
        {
            if (ManagerGame.Instance.Regiao.Loot)
            {
                loot = GetComponent<LootManager>();
                StartCoroutine(loot.GerarLoot());
            }
        }
        possoexecutar = false;
        possoexecutaritem = false;
    }
    // Update is called once per frame
    public void MontarStatusPlayer(Status status, RobotManager manager)
    {        
        if(!Abertura)
        {
            foreach (GameObject bg in NucleosElementaisPlayer)
            {
                bg.SetActive(false);
            }
            NucleosElementaisPlayer[status.NucleoElemental].SetActive(true);
            foreach (GameObject bg in NucleosFisicoPlayer)
            {
                bg.SetActive(false);
            }
            NucleosFisicoPlayer[status.NucleoFisico.Model].SetActive(true);
        }
       
        //associa as variaveis do status;
        NomeDoRobo.text = status.Nome;
        SliderIntegridadeJogador.maxValue = status.Integridade;
        SliderIntegridadeJogador.value = manager.integridadeAtual;
        SliderResistenciaJogador.maxValue = status.Resistencia;
        SliderResistenciaJogador.value = manager.ResistenciaAtual;
        IntegridadeTotalJogador.text = status.Integridade.ToString();
        IntegridadeAtualJogador.text = manager.integridadeAtual.ToString();
        ResistenciaTotalJogador.text = status.Resistencia.ToString();
        ResistenciaAtualJogador.text = manager.ResistenciaAtual.ToString();
        SliderBateria.maxValue = status.Bateria;
        SliderBateria.value = manager.bateriaAtual;
        //associa os gameobjects do robotmanager
        manager.SliderIntegridade = SliderIntegridadeJogador;
        manager.SliderResistencia = SliderResistenciaJogador;
        manager.TextoIntegridadeAtual = IntegridadeAtualJogador;
        manager.TextoResistenciaAtual = ResistenciaAtualJogador;
        manager.SliderBateria = SliderBateria;
        BotaoOverlock.GetComponent<BtOverlock>().robo = manager;
        for (int i = 0; i < 6; i++)
        {
            manager.SliderAcoes.Add(SliderAcoes[i]);
        }
        foreach (Slider sld in SliderAcoes)
        {
            sld.gameObject.SetActive(false);
        }
        for (int i = 0; i < status.Acoes; i++)
        {
            SliderAcoes[i].gameObject.SetActive(true);
        }
        CaixaStatusJogador.color = Color.white;
        manager.CaixaDeStatus = CaixaStatusJogador;        
    }
    public void MontarStatusEnemy(Status status, RobotManager manager)
    {
        if(!Abertura)
        {
            foreach (GameObject bg in NucleosElementaisRival)
            {
                bg.SetActive(false);
            }
            NucleosElementaisRival[status.NucleoElemental].SetActive(true);
            foreach (GameObject bg in NucloesFisicosRival)
            {
                bg.SetActive(false);
            }
            NucloesFisicosRival[status.NucleoFisico.Model].SetActive(true);
           // OvalBase.transform.position = new Vector3(status.transform.position.x, OvalBase.transform.position.y, OvalBase.transform.position.z);
        }        
        NomeDoRoboEnemy.text = status.Nome;
        SliderIntegridadeInimigo.maxValue = status.Integridade;
        SliderIntegridadeInimigo.value = manager.integridadeAtual;
        SliderResistenciaInimigo.maxValue = status.Resistencia;
        SliderResistenciaInimigo.value = manager.ResistenciaAtual;       
        SliderBateriaInimigo.maxValue = status.Bateria;
        SliderBateriaInimigo.value = manager.bateriaAtual;
        manager.SliderBateria = SliderBateriaInimigo;        
        manager.SliderIntegridade = SliderIntegridadeInimigo;
        manager.SliderResistencia = SliderResistenciaInimigo;
        CaixaStatusRival.color = Color.white;
        manager.CaixaDeStatus = CaixaStatusRival;        
    }
    public void LimparMenu(RobotManager manager)
    {
        manager.SliderIntegridade = null;
        manager.SliderResistencia = null;
        manager.TextoIntegridadeAtual = null;
        manager.TextoResistenciaAtual = null;
        manager.SliderBateria = null;
        BotaoOverlock.GetComponent<BtOverlock>().robo = null;
        for (int i = 0; i < 6; i++)
        {
            manager.SliderAcoes.Clear();
        }
    }
    public void ZerarBarraDeAcao()
    {
        foreach (Slider acao in SliderAcoes)
        {
            acao.gameObject.SetActive(false);
        }
    }
    public void MostrarCaixaDeAcao()
    {
        if (!CaixaDeAcao.activeSelf)
        {
            CaixaDeAcao.SetActive(true);
            LeanTween.moveLocalY(CaixaDeAcao, -36.2213f, VelocidadeDosMenus);
        }
    }
    public void EsconderCaixaDeAcao()
    {
        if (CaixaDeAcao.activeSelf)
        {
            CaixaDeAcao.SetActive(false);
            LeanTween.moveLocalY(CaixaDeAcao, -85.1f, VelocidadeDosMenus);
        }
    }
    public void CreateSwitchMenu()
    {
        CaixaDeTroca.GetComponent<SwitchManager>().GerarMenuDeTroca(ActiveRobotManager);
    }
    public void CreateItemMenu()
    {
        CaixaDeItem.GetComponent<ItemManager>().GerarMenuItem();
    }
    public void MostrarMenuDeTroca(bool fechar)
    {
        if (!CaixaDeTroca.activeSelf)
        {
            CancelarTroca.fechar = fechar;
            CaixaDeTroca.SetActive(true);
            LeanTween.moveLocalY(CaixaDeTroca, -36.2213f, VelocidadeDosMenus);
            possoexecutar = true;
        }
    }
    public void EsconderMenuDeTroca()
    {
        if (CaixaDeTroca.activeSelf)
        {
            CaixaDeTroca.SetActive(false);
            possoexecutar = true;
            LeanTween.moveLocalY(CaixaDeTroca, -83.4f, VelocidadeDosMenus);
            battleManager.playerManager.MeuTurno = false;
            CaixaDeTroca.SetActive(false);
        }
    }
    public void MostrarMenuItem()
    {
        if (!CaixaDeItem.activeSelf)
        {
            CaixaDeItem.SetActive(true);
            LeanTween.moveLocalY(CaixaDeItem, -36.2213f, VelocidadeDosMenus);
            possoexecutaritem = true;
        }
    }
    public void EsconderMenuItem()
    {
        if (CaixaDeItem.activeSelf)
        {
            possoexecutaritem = true;
            CaixaDeItem.SetActive(false);
            LeanTween.moveLocalY(CaixaDeItem, -83.4f, VelocidadeDosMenus);
            battleManager.playerManager.MeuTurno = false;
        }
    }
    public void AtualizarMenuAtaques(Weapon weapon, WeaponMethods wp)
    {
        CaixaDeAtaques.GetComponent<AttackManager>().GerarMenu(weapon, wp, battleManager);
    }
    public void MostrarMenuAtaques()
    {
        if (!CaixaDeAtaques.activeSelf)
        {
            CaixaDeAtaques.SetActive(true);
            CaixaDeAtaques.GetComponent<AttackManager>().ativo = true;
            LeanTween.moveLocalY(CaixaDeAtaques, -32.1f, VelocidadeDosMenus);
        }
    }
    public void EsconderMenuAtaques()
    {
        if (CaixaDeAtaques.activeSelf)
        {
            CaixaDeAtaques.SetActive(false);
            CaixaDeAtaques.GetComponent<AttackManager>().ativo = false;
            LeanTween.moveLocalY(CaixaDeAtaques, -86.3f, VelocidadeDosMenus);
        }
    }
    public void PlayerWins()
    {          
        if (Abertura)
        {
            GameObject.FindWithTag("Dialogo").GetComponent<GerenciadorDialogo>().DisplayNextSetence();
            new WaitForSeconds(1f);
            Time.timeScale = 1f;
            ManagerGame.Instance.SceneToLoad = 2;
            ManagerGame.Instance.LoadNextScene();
        }
        else
        {
            tela = Instantiate(TelaFim, ManagerGame.Instance.Regiao.Posicao.transform);
            tela.GetComponent<TelaFimBatalha>().CriarTelaFim(GetComponent<LootManager>());
            tela.GetComponent<TelaFimBatalha>().MostrarTelaGanhou();
            GameObject.Find("ControlaMusicaDeFundo").GetComponent<AudioSource>().Stop();
            ManagerGame.Instance.Lost = false;
            PlayerObjects.Fantodin += ManagerGame.Instance.Money; ;
            PlayerStatus.Trending += ManagerGame.Instance.Trend;
            if (loot != null)
            {
                foreach (Loot lt in loot.LootBatalha)
                {
                    lt.MeADicionaAoInventario();
                }
            }
            for (int i = 0; i < ActiveRobotManager.PlayerRobots.Count; i++)
            {
                ActiveRobotManager.PlayerRobots[i].GetComponent<WeaponMethods>().LimparArma();
                passarStatus(ActiveRobotManager.PlayerRobots[i].GetComponent<RobotManager>(), PlayerObjects.RobotsInUse[i]);
            }
        }
    }
    void passarStatus(RobotManager st, FantoRob rob)
    {
        rob.IntegridadeAtual = (int)st.integridadeAtual;
        rob.BateriaAtual = (int)st.bateriaAtual;
        rob.Spy = st.Spy;
        rob.Keylogger = st.Keylogger;
        rob.KeyloggerAtual = st.keyloggerVariant;
        rob.Trojan = st.Trojan;
        rob.FatorTrojan = st.fatorTrojan;
        rob.Ranson = st.Ranson;
        rob.FantoRanson = st.fatorRanson;
        rob.Virus = st.Virus;
        rob.Worm = st.Worm;
        rob.AumentoSpy = st.AumentoSpy;
        rob.WormPercentual = st.WormPercentual;
        rob.VirusPercentual = st.VirusPercentual;
    }
    public void PlayerLoses()
    {       
        if (Abertura)
        {
            GameObject.FindWithTag("Dialogo").GetComponent<GerenciadorDialogo>().DisplayNextSetence();
            new WaitForSeconds(1f);
            Time.timeScale = 1f;
            ManagerGame.Instance.SceneToLoad = 2;
            ManagerGame.Instance.LoadNextScene();
        }
        else
        {
            tela = Instantiate(TelaFim, ManagerGame.Instance.Regiao.Posicao.transform);
            tela.GetComponent<TelaFimBatalha>().MostrarTelaPerdeu();
            GameObject.Find("ControlaMusicaDeFundo").GetComponent<AudioSource>().Stop();
            ManagerGame.Instance.Lost = true;
            if (PlayerObjects.Fantodin >= ManagerGame.Instance.Money / 2)
            {
                PlayerObjects.Fantodin -= ManagerGame.Instance.Money / 2;
            }
            else
            {
                PlayerObjects.Fantodin = 0;
            }
            
                PlayerStatus.Trending = 0;
                        
            for (int i = 0; i < ActiveRobotManager.PlayerRobots.Count; i++)
            {
                ActiveRobotManager.PlayerRobots[i].GetComponent<WeaponMethods>().LimparArma();
                statusIgualUm(ActiveRobotManager.PlayerRobots[i].GetComponent<RobotManager>(), PlayerObjects.RobotsInUse[i]);
            }           
        }
    }
    void statusIgualUm(RobotManager st, FantoRob rob)
    {
        rob.IntegridadeAtual = 1;
        rob.BateriaAtual = 1;
        rob.Spy = st.Spy;
        rob.Keylogger = st.Keylogger;
        rob.KeyloggerAtual = st.keyloggerVariant;
        rob.Trojan = st.Trojan;
        rob.Ranson = st.Ranson;
        rob.Virus = st.Virus;
        rob.Worm = st.Worm;
        rob.AumentoSpy = st.AumentoSpy;
        rob.WormPercentual = st.WormPercentual;
        rob.VirusPercentual = st.VirusPercentual;
    }
    public void ZerarCena()
    {
        Destroy(tela);
        foreach(GameObject gb in PlayerRobots)
        {
            Destroy(gb);
        }
        PlayerRobots.Clear();
        ActiveRobotManager.PlayerRobots.Clear();
        foreach (GameObject gb in EnemyRobots)
        {
            Destroy(gb);
        }
        EnemyRobots.Clear();
        ActiveRobotManager.EnemyRobots.Clear();
    }
}
   [System.Serializable]
   public class TextoRelacao
{
    public List<string> MinhaRelacao; 
}
