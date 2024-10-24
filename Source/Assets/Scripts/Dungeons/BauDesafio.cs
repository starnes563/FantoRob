using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BauDesafio : MonoBehaviour
{  
    public int MeuID;
    public Sprite SpriteDestrancado;
    public Sprite SpriteAberto;
    public SpriteRenderer ItemBau;
    //Itens
    public Sprite ChavePequenaSp;
    public Sprite ChaveGrandeSp;
    public Sprite ItemDesafioSp;
    public Sprite MapaIcone;
    public Sprite IconeExpasao;
    public Sprite EsqueletoEspecial;
    //Sons
    public AudioClip SomBauAberto;
    public AudioClip SomBauDestrancado;
    public AudioClip SomChaveouLoot;
    public AudioClip SomChaveGrandeouItem;
    //Objetos
    public enum TIPO
    {
        DESTRANCADO,
        INIMIGOS,
        BOTAO,
    }
    public TIPO MeuTipo;
    public enum Conteudo
    {
        CHAVEPEQUENA,
        CHAVEGRANDE,
        ITEMDESAFIO,
        LOOT,
        MAPA,
        EXPANSAO,
        ESQUELETOESPECIAL,
    }
    public int Desafio;
    public Conteudo MeuConteudo;
    bool destrancado = false;
    bool aberto = false;
    [HideInInspector]
    public bool PodeAbrir = false;
    [HideInInspector]
    public CaixaDialogo CaixaDeDialogo;
    public Dialogo InicioFrase;
    private Dialogo MeuDialogo;
    public List<string> ChavePequena;
    public List<string> ChaveGrande;
    public List<string> ItemDesafio;
    public List<string> MapaDesafio;
    public List<string> ExpansaoString;
    public List<string> EsqueletoString;
    Loot MeuLoot;
    //bautrancado por inimigos
    [HideInInspector]
    public List<GameObject> InimigosADerrotar = new List<GameObject>();
    float contador = 0;
    Walk Player;
    // Start is called before the first frame update
    void Start()
    {
        CaixaDeDialogo = GameObject.FindWithTag("MainCamera").transform.GetChild(0).GetComponent<CaixaDialogo>();
        if(MeuConteudo == Conteudo.MAPA)
        {
            if(StoryEvents.MapaDesafio[Desafio])
            {
                PodeAbrir = false;
                destrancado = true;
                aberto = true;
                GetComponent<SpriteRenderer>().sprite = SpriteAberto;                
            }
            IniciarBau();
        }
        else
        {
            BauAberto();
            IniciarBau();
        }               
    }

    // Update is called once per frame
    void Update()
    {
        if(MeuTipo == TIPO.INIMIGOS && !destrancado && !aberto)
        {
            contador += Time.deltaTime;
            if(contador>=0.5f)
            {
                contador = 0f;
                bool devoabrir = true;
                foreach (GameObject g in InimigosADerrotar)
                {
                    if(g != null)
                    {
                        devoabrir = false;
                    }
                }
                if (devoabrir) { DestrancarBau(); }
            }
        }
        if (Input.GetButtonDown("Fire1") && PodeAbrir && !aberto && destrancado)
        {
            Clicou();
        }
    }
    public void IniciarBau()
    {
        if (!aberto)
        {
            if (MeuTipo == TIPO.DESTRANCADO)
            {
                GetComponent<SpriteRenderer>().sprite = SpriteDestrancado;
                destrancado = true;
            }
            MeuDialogo = Instantiate(InicioFrase);
            MeuDialogo.LerOTexto(ManagerGame.Instance.Idm);
            switch (MeuConteudo)
            {
                case Conteudo.CHAVEPEQUENA:
                    MeuDialogo.Sentencas[0] = MeuDialogo.Sentencas[0] + ChavePequena[ManagerGame.Instance.Idm];
                    ItemBau.sprite = ChavePequenaSp;
                    break;
                case Conteudo.CHAVEGRANDE:
                    MeuDialogo.Sentencas[0] = MeuDialogo.Sentencas[0] + ChaveGrande[ManagerGame.Instance.Idm];
                    ItemBau.sprite = ChaveGrandeSp;
                    break;
                case Conteudo.ITEMDESAFIO:
                    MeuDialogo.Sentencas[0] = MeuDialogo.Sentencas[0] + ItemDesafio[ManagerGame.Instance.Idm];
                    ItemBau.sprite = ItemDesafioSp;
                    break;
                case Conteudo.LOOT:
                   
                    //gerar loot
                    MeuLoot = GameObject.FindWithTag("Regiao").GetComponent<RegionData>().AllLoot[Random.Range(0, ManagerGame.Instance.Regiao.AllLoot.Count)];
                    //gerarnome
                    string nome = "";
                    switch (MeuLoot.MeuTipo)
                    {
                        case Loot.TipodeLoot.ITEMCONSTRUIR:
                            nome = Constructor.RetornarNome(6, 0, 0, 0, MeuLoot.Propriedade, 0);
                            ItemBau.sprite = Constructor.RetornarSprite(6, 0, 0, MeuLoot.Propriedade, 0);
                            break;
                        case Loot.TipodeLoot.PENTEVAZIO:
                            nome = Constructor.RetornarNome(1, 0, 0, 0, 0, 0);
                            ItemBau.sprite = Constructor.RetornarSprite(1, 0, 0, 0, 0);
                            break;
                        case Loot.TipodeLoot.PENTECHEIO:
                            nome = Constructor.RetornarNome(1, 0, 0, 0, 0, 0);
                            ItemBau.sprite = Constructor.RetornarSprite(1, 0, 0, 0, 0);
                            break;
                        case Loot.TipodeLoot.CIRCUITO:
                            nome = Constructor.RetornarNome(5, 0, 0, MeuLoot.Propriedade, 0, 0);
                            ItemBau.sprite = Constructor.RetornarSprite(5,0, MeuLoot.Propriedade,0,0);
                            break;
                        case Loot.TipodeLoot.SILICIO:
                            nome = Constructor.RetornarNome(0, 0, 0, 0, 0, 0);
                            ItemBau.sprite = Constructor.RetornarSprite(0, 0, 0, 0, 0);
                            break;
                        case Loot.TipodeLoot.PARTEROBO:
                            nome = Constructor.RetornarNome(7, 0, 0, 0, 0, MeuLoot.Propriedade);
                            ItemBau.sprite = Constructor.RetornarSprite(7,0,0,0, MeuLoot.Propriedade);
                            break;
                    }
                    MeuDialogo.Sentencas[0] = MeuDialogo.Sentencas[0] + " " + nome;
                    break;
                case Conteudo.MAPA:
                    MeuDialogo.Sentencas[0] = MeuDialogo.Sentencas[0] + MapaDesafio[ManagerGame.Instance.Idm];
                    ItemBau.sprite = MapaIcone;                    
                    break;
                case Conteudo.EXPANSAO:
                    MeuDialogo.Sentencas[0] = MeuDialogo.Sentencas[0] + ExpansaoString[ManagerGame.Instance.Idm];
                    ItemBau.sprite = IconeExpasao;
                    break;
                case Conteudo.ESQUELETOESPECIAL:
                    MeuDialogo.Sentencas[0] = MeuDialogo.Sentencas[0] + EsqueletoString[ManagerGame.Instance.Idm];
                    ItemBau.sprite = EsqueletoEspecial;
                    break;
            }
        }
    }
    public void DestrancarBau()
    {
        //trocar imagem e abrir
        GetComponent<SpriteRenderer>().sprite = SpriteDestrancado;
        destrancado = true;
        //tocar o som;
        GetComponent<AudioSource>().PlayOneShot(SomBauDestrancado);
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            PodeAbrir = true;
            Player = collider.GetComponent<Walk>();
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            PodeAbrir = false;
            Player = null;
        }
    }
    public void Clicou()
    {
        if (!CaixaDeDialogo.gameObject.activeSelf && !ManagerGame.Instance.Transitando && !ManagerGame.Instance.EmBatalha)
        {
            Player.CanIWalk = false;
            Diretor.DesativarMenuPlayer();
            PodeAbrir = false;
            aberto = true;
            StoryEvents.DesafiosCamp[Desafio].Interagiveis[MeuID] = true;
            GetComponent<SpriteRenderer>().sprite = SpriteAberto;
            GetComponent<AudioSource>().PlayOneShot(SomBauAberto);
            CaixaDeDialogo.ReceberDialogo(InicioFrase);
            ItemBau.gameObject.SetActive(true);
            CaixaDeDialogo.ReceberDialogo(MeuDialogo);
            switch (MeuConteudo)
            {
                case Conteudo.CHAVEPEQUENA:
                    StoryEvents.DesafiosCamp[Desafio].Chavepequena++;                
                    GetComponent<AudioSource>().PlayOneShot(SomChaveouLoot);
                    break;
                case Conteudo.CHAVEGRANDE:
                    StoryEvents.DesafiosCamp[Desafio].Chavegrande = true;  
                    GetComponent<AudioSource>().PlayOneShot(SomChaveGrandeouItem);
                    break;
                case Conteudo.ITEMDESAFIO:
                    StoryEvents.DesafiosCamp[Desafio].Itemdesafio = true;                   
                    GetComponent<AudioSource>().PlayOneShot(SomChaveGrandeouItem);
                    break;
                case Conteudo.LOOT:
                    MeuLoot.MeADicionaAoInventario();                   
                    GetComponent<AudioSource>().PlayOneShot(SomChaveouLoot);
                    break;
                case Conteudo.MAPA:
                    StoryEvents.MapaDesafio[Desafio] = true;
                    GetComponent<AudioSource>().PlayOneShot(SomChaveGrandeouItem);
                    break;
                case Conteudo.EXPANSAO:
                    PlayerObjects.InventarioMax += 1;
                    GetComponent<AudioSource>().PlayOneShot(SomChaveouLoot);                    
                    break;
                case Conteudo.ESQUELETOESPECIAL:
                    PlayerObjects.EsqueletoEspecial++;
                    GetComponent<AudioSource>().PlayOneShot(SomChaveouLoot);
                    break;
            }
        
            UIDesafio.Atualizar();
        }
    }
    public void BauAberto()
    {
        if (StoryEvents.DesafiosCamp[Desafio].Interagiveis[MeuID] == true)
        {
            PodeAbrir = false;
            destrancado = true;
            aberto = true;
            GetComponent<SpriteRenderer>().sprite = SpriteAberto;
        }
    }
}
