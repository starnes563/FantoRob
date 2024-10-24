using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMenu : MonoBehaviour
{
    //botões
    public Button ItemButton;
    public Button PartyButton;    
    public Button InventoryButton;
    public Button BotaoMissao;
 
    //menu
    public GameObject ItenMenu;
    public GameObject PartyMenu;
    public GameObject FantoRobMenu;
    public GameObject InventoryMenu;
    public GameObject MenuMissao;
    public GameObject CoreMenu;
    public Text Money;
    [HideInInspector]
    public PlayerObjects playerObjects;
    [HideInInspector]
    public Walk player;
    //
    [HideInInspector]
    public bool Active = false;

    public QuadroJogador QuadroJogador;
    public GameObject SetaGPS;
    // Start is called before the first frame update
    void Start()
    {        
        ItemButton.GetComponent<ItensButton>().Menu = this;
        PartyButton.GetComponent<PartyButton>().Menu = this;   
        playerObjects = PlayerObjects.PlayerObjectsStatic;
    }
    // Update is called once per frame
    void Update()
    {
        if(Active)
        {
            if (Input.GetButtonDown("Jump"))
            {
                if (SetaGPS != null) { SetaGPS.SetActive(true); }
                Fechar();
            }
        }
    }
    public void UpdateMoney()
    {
        Money.text = PlayerObjects.Fantodin.ToString();
    }
    public void AbrirMenuItens()
    {
        FecharTudo();
        ItenMenu.GetComponent<ItensMenu>().Criar(this);
        EsconderQuadro();
        ItenMenu.SetActive(true);
    }
    public void AbrirMenuParty()
    {
        FecharTudo();
        PartyMenu.GetComponent<PartyMenu>().Criar(this);
        EsconderQuadro();
        if(!ManagerGame.Instance.Regiao.Desafio || ManagerGame.Instance.Regiao.Cutscene)
        {
            FantoRobMenu.GetComponent<FantorobMenu>().Criar(this);
            FantoRobMenu.SetActive(true);
        }
        PartyMenu.SetActive(true);
    }    
    public void AbrirMenuInventario()
    {
        FecharTudo();
        EsconderQuadro();
        InventoryMenu.GetComponent<IntentoryMenu>().Criar();
        InventoryMenu.GetComponent<IntentoryMenu>().PlMenu = this;
        InventoryMenu.SetActive(true);
    }
    public void AbrirMenuMissoes()
    {
        FecharTudo();
        EsconderQuadro();
        SonsMenu.Confimar();
        MenuMissao.GetComponent<MenuMissoes>().plmen = this;        
        MenuMissao.SetActive(true);
    }
    public void AbrirMenuCore(FantoRob robo)
    {
        CoreMenu.GetComponent<ShowRobot>().CriarMenu(robo, this);
        FecharTudo();
        EsconderQuadro();
        CoreMenu.SetActive(true);
        this.gameObject.SetActive(false);
    }
    public void Salvar()
    {

    }
    public void Fechar()
    {
        SonsMenu.Desistir();
        player.FecharMenu();
    }
    public void FecharTudo()
    {
        ItenMenu.SetActive(false);
        PartyMenu.SetActive(false);
        FantoRobMenu.SetActive(false);
        InventoryMenu.SetActive(false);
        MenuMissao.SetActive(false);
    }
    public void EsconderQuadro()
    {
        QuadroJogador.gameObject.SetActive(false);
    }
    public void MostrarQuadro()
    {
        QuadroJogador.gameObject.SetActive(true);
    }
    public void MontarQuadroJogador()
    {
        QuadroJogador.Mostrar();
    }
    public void OnEnable()
    {
        if (SetaGPS != null) { SetaGPS.SetActive(false); }
    }
}
