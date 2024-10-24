using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerShop : MonoBehaviour
{
    public List<ShopStock> EstoqueFantodin = new List<ShopStock>();   
    public List<ShopStock> Aleatorio = new List<ShopStock>();
    public List<ShopStock> EstoqueCriado = new List<ShopStock>();
    public List<int> ContagemPente = new List<int>();
    public List<ShopStock> EstoqueCreditos = new List<ShopStock>();
  
    public PreShopMenu Preshop;
    public GameObject QuadroPropriedades;
    public GameObject QuadroEscolherNucleoFisico;
    public int GerarAleatorio;
    public Image Spacer;
    public Button BuyButton;
    public List<GameObject> botao = new List<GameObject>();
    //para funconar a musica
    public static AudioSource Source;
    public static AudioClip SomConfirmar;
    public static AudioClip SomDesistir;
    public static AudioClip SomNaoPode;
    public static AudioClip SomMoedas;
    public AudioClip SomParaNao;
    public AudioClip SomParaCom;
    public AudioClip SomParaDes;
    public AudioClip SomComprar;
    public List<string> FalaComprar;
    public Fala Dialogo;
    public Animator AnimatorArnaldo;
    public string PosicaoComprando;
    public string PosicaoOciosa;
    public Text FantoDinPlayer;
    public Text CreditoPlayer;
    public bool DarMissao = false;
    public bool Maquina = false;
    public Text Inventario;
    public static ManagerShop Instance;
    private void Awake()
    {
        SomConfirmar = SomParaCom;
        SomDesistir = SomParaNao;
        SomNaoPode = SomParaNao;
        SomMoedas = SomComprar;
        Source = GetComponent<AudioSource>();       
    }
    void OnEnable()
    {
        this.transform.position = new Vector3(this.transform.position.x, -7.23f);
        LeanTween.moveLocalY(this.gameObject, -0.12f, 0.3f);
        FantoDinPlayer.text = PlayerObjects.Fantodin.ToString();
        CreditoPlayer.text = PlayerObjects.Creditos.ToString();
        if(!Maquina)
        {
            AnimatorArnaldo.Play(PosicaoComprando);
        }        
        bool facopente = false;        
        if(Random.Range(0,101)>GerarAleatorio && !DarMissao)
        {
            facopente = true;
        }
        if(StoryEvents.RegeraEstoque)
        {
            if (Random.Range(0, 101) > GerarAleatorio && !DarMissao)
            {
                facopente = true;
            }
            CriarMenu(facopente);
        }
        else
        {
            RegerarMenu();
        }       
        if(Dialogo != null && !Maquina)
        {
            Dialogo.DigitarNaTela(FalaComprar[ManagerGame.Instance.Idm], "Arnaldo");
        }      
        GameObject.FindWithTag("Player").GetComponent<Walk>().CanIWalk = false;
        Instance = this;
        AtualizarInvent();
    }
    public void CriarMenu(bool facopente)
    {
        StoryEvents.RegeraEstoque = false;
        if (botao.Count > 1)
        {
            foreach (GameObject bt in botao)
            {
                Destroy(bt);
            }
            botao.Clear();
        }
        //estoquedinheiro
        if (EstoqueFantodin != null)
        {
            for (int i = 0; i < EstoqueFantodin.Count; i++)
            {
                if (EstoqueFantodin[i].Estrela <= PlayerStatus.Estrelas)
                {
                    if (EstoqueFantodin[i].Bag[0] && !StoryEvents.ExpasoesInventario[0])
                    {
                        CriarBotao(EstoqueFantodin[i]);
                    }
                    else if (EstoqueFantodin[i].Bag[1] && !StoryEvents.ExpasoesInventario[1])
                    {
                        CriarBotao(EstoqueFantodin[i]);
                    }
                    else if (!EstoqueFantodin[i].Bag[0] && !EstoqueFantodin[i].Bag[1])
                    {
                        CriarBotao(EstoqueFantodin[i]);
                    }
                }
            }
        }
        EstoqueCriado.Clear();
        // Aleatorio
        if (facopente)
        {               
            //pentes
            int quantospentes = Random.Range(1, 5);
            for (int j = 0; j < quantospentes; j++)
            {                
                //aletoariza o pente
                int nivel = 1;
                int preco = 10;
                switch (PlayerStatus.Estrelas)
                {
                    case 2:
                        nivel = 2;
                        preco = 16;
                        break;
                    case 3:
                        nivel = 2;
                        preco = 16;
                        break;
                    case 4:
                        nivel = 3;
                        preco = 22;
                        break;
                    case 5:
                        nivel = 3;
                        preco = 22;
                        break;
                    case 6:
                        nivel = 4;
                        preco = 30;
                        break;
                    case 7:
                        nivel = 4;
                        preco = 30;
                        break;
                    case 8:
                        nivel = 4;
                        preco = 30;
                        break;
                }
                Aleatorio[0].PenteVazio.CriarPente(nivel, Aleatorio[0].PenteVazio.MeuTipo, Constructor.MoveConstructor(nivel));
                Aleatorio[0].Preco = preco;
                EstoqueCriado.Add(Instantiate(Aleatorio[0]));
            }           
            //partes
            int quantaspartes = Random.Range(1, 5);
            for (int j = 0; j < quantaspartes; j++)
            {
                int partid = 1;
                int prop = 1;
                int preco = 25;
                if (PlayerStatus.Estrelas < 2)
                {
                    partid = Random.Range(1, 3);
                    preco = 25;
                }
                else if (PlayerStatus.Estrelas < 4)
                {
                    partid = Random.Range(1, 5);
                    preco = 45;
                }
                else if (PlayerStatus.Estrelas >= 4)
                {
                    partid = Random.Range(0, 5);
                    prop = 2;
                    preco = 70;
                }
                Aleatorio[1].ParteRobo = Constructor.Instance.PartConstructor(partid, Random.Range(0, 5), prop);
                Aleatorio[1].Preco = preco;
                EstoqueCriado.Add(Instantiate(Aleatorio[1]));                
            }
            if (EstoqueCriado.Count>0)
            {
                for (int i = 0; i < EstoqueCriado.Count; i++)
                {                    
                        CriarBotao(EstoqueCriado[i]);                    
                }
            }

            //Microtransações
            if (EstoqueCreditos != null && EstoqueCreditos.Count > 0)
                {
                for (int i = 0; i < EstoqueCreditos.Count; i++)
                {
                    if (EstoqueCreditos[i].Estrela <= PlayerStatus.Estrelas)
                    {
                        CriarBotao(EstoqueCreditos[i]);
                    }
                }
            }

        }

       
    }
    private void CriarBotao(ShopStock stock)
    {
        Button Bot = Instantiate(BuyButton, Spacer.transform) as Button;
        botao.Add(Bot.gameObject);
        Bot.GetComponent<BuyButton>().Criar(stock, Preshop);
    }
    
    public static void TocarSomConfimar()
    {
        Source.PlayOneShot(SomConfirmar);
    }
    public static void TocarSomDesiste()
    {
        Source.PlayOneShot(SomDesistir);
    }
    public static void TocarSomNaoPode()
    {
        Source.PlayOneShot(SomNaoPode);
    }
    public static void TocarSomComprar()
    {
        Source.PlayOneShot(SomMoedas);
    }
    public void Salvar()
    {
       //SaveSystem.Save();
    }
    public void AtualizarValores()
    {
        FantoDinPlayer.text = PlayerObjects.Fantodin.ToString();
        CreditoPlayer.text = PlayerObjects.Creditos.ToString();
    }
    public void AtualizarInvent()
    {
        int invent = 0;
        foreach (GameObject item in PlayerObjects.PlayerObjectsStatic.Itens)
        {
            invent += item.GetComponent<Item>().Quantidade;
        }

        int inventatual = PlayerObjects.InventarioMax - invent;
        Inventario.text = inventatual.ToString();
    }
    public void RegerarMenu()
    {
        Preshop.gameObject.SetActive(false);
        QuadroPropriedades.SetActive(false);
        QuadroEscolherNucleoFisico.SetActive(false);
        if (botao.Count > 1)
        {
            foreach (GameObject bt in botao)
            {
                Destroy(bt);
            }
            botao.Clear();
        }
        //estoquedinheiro
        if (EstoqueFantodin != null)
        {
            for (int i = 0; i < EstoqueFantodin.Count; i++)
            {
                if (EstoqueFantodin[i].Estrela <= PlayerStatus.Estrelas)
                {
                    if(EstoqueFantodin[i].Compravel)
                    {
                        if (EstoqueFantodin[i].Bag[0] && !StoryEvents.ExpasoesInventario[0])
                        {
                            CriarBotao(EstoqueFantodin[i]);
                        }
                        else if (EstoqueFantodin[i].Bag[1] && !StoryEvents.ExpasoesInventario[1])
                        {
                            CriarBotao(EstoqueFantodin[i]);
                        }
                        else if (!EstoqueFantodin[i].Bag[0] && !EstoqueFantodin[i].Bag[0])
                        {
                            CriarBotao(EstoqueFantodin[i]);
                        }
                    }                              
                }
            }
        }
        if (EstoqueCriado.Count > 0)
        {
            for (int i = 0; i < EstoqueCriado.Count; i++)
            {
               
                if (EstoqueCriado[i].Compravel)
                {
                    CriarBotao(EstoqueCriado[i]);
                }
            }
        }
    }
}