using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class ManagerGame : MonoBehaviour
{
    public static ManagerGame Instance;
    //tempo de jogo
    public float Tempo;
    //Hero
    public List<GameObject> NeftariDireita;
   
   // [HideInInspector]
    public GameObject HeroAtual;
    //0-pijama, 1 - sem mochila, 2 - com mochila
    public bool instanciahero = true;
    
    //Scenes
    [HideInInspector]
    public int SceneToLoad;
    //gotattacked]
    //[HideInInspector]
    public bool EmBatalha = false;    
    //portugues = 0
    //english = 1
    public int Idm = 0;
    //ENUM 
    //sempre termina com rob;
    public List<string> SavePath;
    public int ActualSavePath;
    public List<string> MensagemDeErro;
    //eventos
    public event Action GanhouBatalha;
    public event Action PerdeuBatalha;
    public delegate void ID(int modelo);
    public event ID VenceuFantorob;
    public event ID VenceuElemento;
    public event ID VenceuFisico;
    public event ID UsaFantoRob;
    public event ID UsaElemento;
    public event ID UsaFisico;
    public event Action SuperEfeitov;
    public event ID Combo;
    public event ID UsaArma;
    [HideInInspector]
    Sprite Spriteatual;
    [HideInInspector]
    string Nome;
    public enum GameState
    {
        CANBATTLE_STATE,
        CANTBATTLE_STATE,
        ONBATTLE_STATE,
        IDLE,
    }
    [HideInInspector]
    public GameState gameState;
    //Battle Variables
    [HideInInspector]
    public List<int> Robots = new List<int>();
    [HideInInspector]
    public List<FantoRob> PlayerRobots = new List<FantoRob>();
    [HideInInspector]
    public List<FantoRob> RobotsToBattle = new List<FantoRob>();
    [HideInInspector]
    public List<GameObject> Item = new List<GameObject>();
    //Loot
    [HideInInspector]
    public int Exp;
    [HideInInspector]
    public int Trend;
    [HideInInspector]
    public int Money;       
    
    [HideInInspector]    
    public int ProximaDificuldade;  
    [HideInInspector]
    public RegionData Regiao;   
    [HideInInspector]
    public int IAProximaBatalha;
    [HideInInspector]
    public bool Lost = false;
   // [HideInInspector]
    public bool Transitando = false;
    [HideInInspector]
    public bool Rival = false;
    [HideInInspector]
    public bool Campeao = false;
    [HideInInspector]
    public bool FantoMascara = false;
    private Posicionador posic;
    public GameObject TelaDeMissao;
    [HideInInspector]
    public List<NPCBattle> NPCAtacou;
    [HideInInspector]
    public int Vidas;
    float timer;
    public bool MenuInicial = false;
    float timePlaying;
    void Awake()
    {   
            //check if the instance already exists
        if (Instance == null)
        {
            Instance = this;
            if(!this.MenuInicial)
            {              
                ManagerGame.Instance.Idm = PlayerPrefs.GetInt("Idm");                
            }
        }
        // if exists but is not this one
        else if(Instance!=this)
        {
            if(this.MenuInicial)
            {
               
                this.Idm = Instance.Idm;
                Destroy(Instance.gameObject);                
                Instance = this;                
            }
            else
            {
                
                Destroy(gameObject);
            }           
        }
        //if(MenuInicial == true) { MenuInicial = false; }
        //dont destroy between scenes
        DontDestroyOnLoad(gameObject);
        //pega Regiao;
        if (GameObject.FindWithTag("Regiao"))
        {
            Instance.Regiao = GameObject.FindWithTag("Regiao").GetComponent<RegionData>();
            Instance.instanciahero = Instance.Regiao.InstanciaHero;
            //pega posicionador
            Instance.posic = GameObject.FindWithTag("Regiao").GetComponent<Posicionador>();

            //inicia jogador
            if (!GameObject.Find("Neftari") && Instance.instanciahero)
            {
                GameObject newHero = Instantiate(Instance.NeftariDireita[PlayerStatus.PersonagemAtual], PlayerStatus.NextHeroPosition, Quaternion.identity) as GameObject;
                newHero.name = "Neftari";
                Instance.HeroAtual = newHero;
                Instance.HeroAtual.gameObject.SetActive(true);
                if (PlayerStatus.ProximaAnimacao != null)
                {
                    newHero.GetComponent<Animator>().Play(PlayerStatus.ProximaAnimacao);
                }
            }
            else
            {
                if(!Instance.Regiao.Abertura)
                {
                    Instance.HeroAtual = Instance.posic.Neftari[PlayerStatus.PersonagemAtual];
                }                
            }            
            if (Instance.posic.Marcus != null)
            {
                PlayerStatus.MarcusV = Instance.posic.Marcus;
            }
            if (Instance.posic.Luiza != null)
            {
                PlayerStatus.Luiza = Instance.posic.Luiza;
            }
        }
       
    }
    // Update is called once per frame
    void Update()
    {
        Tempo += Time.deltaTime;
        switch(gameState)
        {
            case (GameState.CANBATTLE_STATE):
                if(EmBatalha)
                {
                    gameState = GameState.ONBATTLE_STATE;
                }
                break;
            case (GameState.CANTBATTLE_STATE):

                break;
            case (GameState.ONBATTLE_STATE):
                //LoadBattleScene
                
                //Go to Idle
                gameState = GameState.IDLE;
                break;
            case (GameState.IDLE):
                break;
        }       
    }
    public void LoadNextScene()
    {        
      SceneManager.LoadSceneAsync(Instance.SceneToLoad);
    }
    public void StartBattle(int tipo, NPCBattle npc)
    {
         CaixaDeSom.Instancia.GetComponent<AudioSource>().Pause();
        Instance.Regiao.GetComponent<Posicionador>().Neftari[PlayerStatus.PersonagemAtual].GetComponent<Walk>().PararDeAndar();
        int animacao = tipo;
        switch(tipo)
        {
            case 0:
                Rival = false;
                Campeao = false;
                FantoMascara = false;
                break;
            case 1:
                Rival = true;
                Campeao = false;
                FantoMascara = false;
                break;
            case 2:
                Rival = false;
                Campeao = true;
                FantoMascara = false;
                break;
            case 3:
                Rival = false;
                Campeao = false;
                FantoMascara = true;
                animacao = 4;
                break;
        }
        Instance.Money = npc.Money;
        Instance.Trend = npc.Trend;
        Instance.Exp = npc.Exp;
        Instance.IAProximaBatalha = npc.Dificuldade;
        Instance.EmBatalha = true;
        Instance.Transitando = true;
        Transform p= GameObject.FindWithTag("MainCamera").transform;
        Instance.Regiao.Posicao.transform.position = new Vector3(p.position.x,p.position.y,0);
        //pausar a musica atual
        CaixaDeSom.Instancia.GetComponent<AudioSource>().Pause();        
        //iniciar a musica de batalha
        Instance.Regiao.AudioBatalha.SetActive(true);
        //instanciar a animação de troca
        Instantiate(Instance.Regiao.AnimacoesDeTroca[animacao], Instance.Regiao.Posicao.transform);
        //receber os robos do rival 
        Instance.RobotsToBattle = new List<FantoRob>();
        foreach (FantoRob rob in npc.Robots)
        {
            Instance.RobotsToBattle.Add(rob);
        }
        //fazer o gerente de transicao montar a cena e deixar pronto para a animação iniciar
        Instance.Regiao.GerenteTransicao.IniciarFantorobs();
        NPCAtacou.Add(npc);
    }
    public void TrocarCenaBatalha()
    {
        Instance.Regiao.GlobalLight.SetActive(false);
        CaixaDeSom.Instancia.GetComponent<AudioSource>().Pause();
        Instance.Regiao.CenaBatalha.SetActive(true);
        Instance.Regiao.CameraCenario.SetActive(false);
        Instance.Regiao.CameraBatalha.SetActive(true);        
    }
    public void AnimacaoFimDaBatalha()
    {
        Instance.Transitando = true;
        Instantiate(Instance.Regiao.AnimacoesDeTroca[3], Regiao.Posicao.transform);
    }
    public void FinalizarBatalha()
    {
        Regiao.GerenteTransicao.ZerarCena();
        if (!Instance.Lost)
        {
            WinTheBattle();
        }
        else
        {
            LostTheBattle();
        }        
    }
    public void WinTheBattle()
    {       
        GanhouBatalha?.Invoke();
        PlayerStatus.ReceiveExp(Exp);
        Exp = 0;
        PlayerStatus.ReceiveReputation(Trend);
        Trend = 0;
        Money = 0;
        if (Instance.Regiao.Desafio && !Campeao)
        { 
            foreach(NPCBattle n in NPCAtacou) 
            {
                if(!n.Cutscene)
                {
                    Destroy(n.gameObject);
                }
               
            }            
        }
        NPCAtacou.Clear();
        TrocarCenaExploracao();
    }
    public void LostTheBattle()
    {
        PerdeuBatalha?.Invoke();
        Exp = 0;
        Trend = 0;
        Money = 0;
        if (Instance.Regiao.Desafio && !Campeao)
        {
            foreach (NPCBattle n in NPCAtacou) { Destroy(n.gameObject); }
        }
        NPCAtacou.Clear();
        if (Instance.Regiao.LoadSaveOnLose)
        {            
            TrocarCenaExploracao();
            Instance.SceneToLoad = 133;
            GameObject.FindWithTag("MainCamera").GetComponent<Diretor>().TrocarACena();
        }
        else
        {
            TrocarCenaExploracao();
        }      
        Instance.Regiao.GetComponent<Posicionador>().Neftari[PlayerStatus.PersonagemAtual].GetComponent<Walk>().LiberarAndar();
        if(Instance.Regiao.Desafio)
        {
            Instance.Regiao.IrParaQuarto();
            PlayerObjects.PerderLoot();
            PlayerStatus.Trending = 0;
            Instance.Transitando = false;
            if(PlayerStatus.DaysLeft>0)
            {
                PlayerStatus.DaysLeft -= 1;
                foreach (FantoRob fanto in PlayerObjects.RobotsInUse)
                {
                    if (fanto != null)
                    {
                        fanto.Curartudo();
                    }
                }
                StoryEvents.RegeraEstoque = true;
            }
            else
            {
                TrocarCenaExploracao();
                Instance.SceneToLoad = 133;
                GameObject.FindWithTag("MainCamera").GetComponent<Diretor>().TrocarACena();
            }
        }
        if (Instance.Regiao.Cutscene)
        {            
            PlayerObjects.PerderLoot();
            PlayerStatus.Trending = 0;
            Instance.Transitando = false;
            if (PlayerStatus.DaysLeft > 0)
            {
                PlayerStatus.DaysLeft -= 1;
                foreach (FantoRob fanto in PlayerObjects.RobotsInUse)
                {
                    if (fanto != null)
                    {
                        fanto.Curartudo();
                    }
                }
                StoryEvents.RegeraEstoque = true;
            }
            else
            {
                TrocarCenaExploracao();
                Instance.SceneToLoad = 133;
                GameObject.FindWithTag("MainCamera").GetComponent<Diretor>().TrocarACena();
            }
        }
    }
    public void TrocarCenaExploracao()
    {
        Instance.Regiao.GlobalLight.SetActive(true);
        CaixaDeSom.Instancia.GetComponent<AudioSource>().UnPause();
        CaixaDeSom.Instancia.GetComponent<AudioSource>().volume = 0;
        StartCoroutine(CaixaDeSom.Instancia.AumentaVolume());
        Instance.Regiao.CenaBatalha.SetActive(false);
        Instance.Regiao.CameraCenario.SetActive(true);
        Instance.Regiao.CameraBatalha.SetActive(false);
        Instance.Regiao.AudioBatalha.SetActive(false);
        Instance.EmBatalha = false;
        Instance.Regiao.GetComponent<Posicionador>().Neftari[PlayerStatus.PersonagemAtual].GetComponent<Walk>().CanIWalk = true;
    }
    public void FinalizarCutsceneAtual(string posicaoanimacao, Vector3 posicao)
    {        
        Instance.HeroAtual.transform.position = posicao;        
        Instance.HeroAtual.SetActive(true);
        Instance.HeroAtual.GetComponent<Animator>().Play(posicaoanimacao);
    }
    public void IniciaCustcene()
    {
        Instance.HeroAtual.gameObject.SetActive(false);
    }
    public void TrocarCenaCutscene(int cena)
    {
        ManagerGame.Instance.SceneToLoad = cena;        
    }    
    public void IniciarCarregarDados()
    {

    }
    public IEnumerator Load()
    {       
        DadosJogador dados = SaveSystem.Load();
        if (dados != null)
        {
            Tempo = dados.Tempo;
            PlayerStatus.PersonagemAtual = dados.PersonagemAtual;
            Instance.IniciarCarregarDados();
            Instance.SceneToLoad = dados.Cena;
            PlayerStatus.NextHeroPosition = new Vector3(dados.transfom[0], dados.transfom[1], dados.transfom[2]);
            PlayerStatus.ProximaAnimacao = "Base Layer.IdleFrente";           
            PlayerStatus.MarcusAtivo = dados.MarcusAtivo;
            PlayerStatus.ProximaPosicaoMarcus = new Vector3(dados.MarcusTransform[0], dados.MarcusTransform[1], dados.MarcusTransform[2]);
            PlayerStatus.LuizaAtiva = dados.LuizaAtiva;
            PlayerStatus.ProximaPosicaoLuiza = new Vector3(dados.LuizaTransform[0], dados.LuizaTransform[1], dados.LuizaTransform[2]);
            //playerstatus
            PlayerStatus.Level = dados.Level;
            PlayerStatus.Exp = dados.Exp;
            PlayerStatus.nextLevel = dados.nextLevel;
            PlayerStatus.Reputation = dados.Reputation;
            PlayerStatus.Trending = dados.Trending;
            PlayerStatus.nextReputation = dados.nextReputation;
            PlayerStatus.Estrelas = dados.Estrelas;
            PlayerStatus.nextstar = dados.nextstar;
            PlayerStatus.ControleDeCena = dados.ControleDeCena;
            PlayerStatus.CartaEndosso = dados.CartaEndosso;
            PlayerStatus.DaysLeft = dados.DaysLeft;
            PlayerStatus.Posicao = dados.Posicao;
            PlayerStatus.Pontos = dados.Pontos;
            PlayerStatus.DaysLeft = dados.DaysLeft;
           // PlayerStatus.Nome = dados.Nome;
            //PlayerStatus.MeuSprite = Constructor.Instance.SpritesPersoangem[dados.Sprite];          
            //PlayerObjects
            PlayerObjects.RobotsInUse.Clear();
            foreach (DadosFantorob i in dados.RobotsInUse)
            {
                FantoRob rob = Instantiate(Constructor.Instance.Fantorobs[i.Modelo]);
                rob.CarregarSalvo(i);
                PlayerObjects.RobotsInUse.Add(rob);
            }
            
            PlayerObjects.RobotsNotInUse.Clear();
            foreach (DadosFantorob i in dados.RobotsNotInUse)
            {
                FantoRob rob = Instantiate(Constructor.Instance.Fantorobs[i.Modelo]);
                rob.CarregarSalvo(i);
                PlayerObjects.RobotsNotInUse.Add(rob);
            }
            PlayerObjects.PentesVazios.Clear();
            foreach (DadoPente i in dados.Combs)
            {
                Pente c = ScriptableObject.CreateInstance<Pente>();
                c.CarregarSalvo(i);
                PlayerObjects.PentesVazios.Add(c);
            }
            for (int i = 0; i < 16; i++)
            {
                PlayerObjects.Circuits[i] = dados.Circuits[i];
            }
            PlayerObjects.PentesCheios.Clear();
            foreach (DadoPente i in dados.PentesCheios)
            {
                Pente c = ScriptableObject.CreateInstance<Pente>();
                c.CarregarSalvo(i);
                PlayerObjects.PentesCheios.Add(c);
            }           
            PlayerObjects.Silicon = dados.Silicon;
            PlayerObjects.RobotParts.Clear();
            foreach (DadoParte i in dados.RobotParts)
            {
                RobotPart p = ScriptableObject.CreateInstance<RobotPart>();
                p.CarregarSalvo(i);
                PlayerObjects.RobotParts.Add(p);
            }
            for (int i = 0; i < dados.Itens.Count; i++)
            {
                PlayerObjects.PlayerObjectsStatic.Itens[i].GetComponent<Item>().Quantidade = dados.Itens[i];
            }
            PlayerObjects.PlayerObjectsStatic.Batteries.Clear();
            foreach (int i in dados.Batteries)
            {
                PlayerObjects.PlayerObjectsStatic.Batteries.Add(i);
            }
            PlayerObjects.ItensConstruir.Clear();
            foreach (int i in dados.ItensConstruir)
            {
                PlayerObjects.ItensConstruir.Add(i);
            }
            PlayerObjects.NucleosFisicos.Clear();
            foreach (DadoNucleoFisico i in dados.NucleosFisicos)
            {
                Weapon nf = Instantiate(Constructor.Instance.NucleosFisicos[i.Array]);
                nf.CarregarSavlo(i);
                PlayerObjects.NucleosFisicos.Add(nf);
            }
            PlayerObjects.Missões.Clear();
            foreach (DadosMissao m in dados.Missões)
            {
                Quest q = new Quest(Quest.TipoDeQuest.BATALHA, m.Requerido, m.Id, m.Nome, m.Descriçao, null);
                q.CarregarSalvo(m);
                q.AdicionarMissao();
            }            
            PlayerObjects.Fantodin = dados.Fantodin;
            PlayerObjects.Creditos = dados.Creditos;
            PlayerObjects.EsqueletoEspecial = dados.EsqueletoEspecial;
            PlayerObjects.InventarioMax = dados.InventarioMax;
            Instance.ActualSavePath = dados.ActualSavePath;
            StoryEvents.Carregar(dados.DadosStory);
            GameObject.FindWithTag("MainCamera").GetComponent<Diretor>().TrocarACena();
            Time.timeScale = 1f;
            // Nome = dados.Nome;
            // Spriteatual = Constructor.Instance.SpritesPersoangem[dados.Sprite];
            // PlayerStatus.Nome = Nome;
            // PlayerStatus.MeuSprite = Spriteatual;            
        }
       // ManagerGame.Instance.Idm = dados.Linguaatual;
        //if (PlayerPrefs.HasKey("Escolhido"))
       // {
            ManagerGame.Instance.Idm = PlayerPrefs.GetInt("Idm");
        //}
            Time.timeScale = 1;
        yield return null;
    }
    public void AnalisarGatilho()
    {
        foreach(GatilhoCutscene gat in Regiao.GatilhosCut)
        {            
            gat.Ativar();
        }
    }
    public void MostrarQuadroMissao(Quest quest)
    {
        Transform cam = GameObject.FindWithTag("MainCamera").GetComponent<Transform>();
        GameObject qm = Instantiate(TelaDeMissao, cam) as GameObject;
        qm.GetComponent<QuestBoard>().MontarQuadro(quest);
        Instance.Regiao.GetComponent<Posicionador>().Neftari[PlayerStatus.PersonagemAtual].GetComponent<Walk>().PararDeAndar();
    }
    void CalculaVida()
    {
        if (Vidas < 5)
        {
            timer += Time.deltaTime;
            if (timer > 30f)
            {
                Vidas++;
                timer = 0;
            }
        }
        //mostrar o timer na tela

    }
    public void ReceberInfoJogador()
    {             
        //criarosavepath
        string save = "nef" + SavePath.Count.ToString() + ".rob";
        SavePath.Add(save);
        ActualSavePath = SavePath.IndexOf(save);
        PlayerStatus.Nome = Nome;
        PlayerStatus.MeuSprite = Spriteatual;
    }
    public void CarregarNovaBatalha()
    {

    }
    public void FantorobKO(int modelo, int fisico, int elemental)
    {
        VenceuFantorob?.Invoke(modelo);
        VenceuElemento?.Invoke(elemental);
        VenceuFisico?.Invoke(fisico);        
    }
    public void FantorobVence(int modelo, int fisico, int elemental)
    {
        UsaFantoRob?.Invoke(modelo);
        UsaElemento?.Invoke(elemental);
        UsaFisico?.Invoke(fisico);
    }
    public void SuperEFetivo()
    {
        SuperEfeitov?.Invoke();
    }
    public void Fezcombo(int id)
    {
        Combo?.Invoke(id);
    }
    public void UsarNF(int nf)
    {
        UsaArma?.Invoke(nf);
    }
}
