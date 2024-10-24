using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoreMenu : MonoBehaviour
{
    [HideInInspector]
    public GameObject Bateria;
    [HideInInspector]
    public string Nome;
    [HideInInspector]
    public Weapon Arma;
    [HideInInspector]
    RobotGeneralMenu Menu;
    int IDAtual;

    //Espaços
    public Text Integridade;
    public Text Ataque;
    public Text AtaqueEspecial;
    public Text Velocidade;
    public Text Resistência;
    public Text Nucleo;
    public Text Modelo;
    public Text Name;

    //botoes
    public Button BotaoBateria;
    public Button BotaoArmaAtual;
    public Button BotaoArmas;

    //Spacers
    public Image SpacerWeapon;
    public Image SpacerAllWeapons;
    public Image SpacerAtackList;
    

    //objetos
    public GameObject AttackNote;
    public List<ComboNote> ListaCombo = new List<ComboNote>();
    private PlayerObjects playerObjects;

    // Start is called bfore the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CriarMenu(GameObject core, int id, RobotGeneralMenu menu, Status status)
    {
        Menu = menu;
        IDAtual = id;
        playerObjects = GameObject.FindWithTag("Gerenciador").GetComponent<PlayerObjects>();
        //coloca os valores no hexagono
        
        switch (core.GetComponent<RobotCore>().Nucleo)
        {
            case 0:
                Nucleo.text = "Red";
                break;
            case 1:
                Nucleo.text = "Blue";
                break;
            case 2:
                Nucleo.text = "Yellow";
                break;
            case 3:
                Nucleo.text = "Green";
                break;
            case 4:
                Nucleo.text = "Orange";
                break;
            case 5:
                Nucleo.text = "Blue";
                break;
        }
        Integridade.text = (string)status.Integridade.ToString();
        Ataque.text = (string)status.Ataque.ToString();
        AtaqueEspecial.text = (string)status.AtaqueEnergetico.ToString();
        Velocidade.text = (string)status.Velocidade.ToString();
        Resistência.text = (string)status.Resistencia.ToString();

        //nome e modelo
        Nome = core.GetComponent<RobotCore>().Nome;
        Name.text = core.GetComponent<RobotCore>().Nome;
        Modelo.text = core.GetComponent<RobotCore>().Modelo;

        //montar o botao da bateria
        Bateria = core.GetComponent<RobotCore>().Bateria;
        TrocarBateria(Bateria.GetComponent<Battery>());

        //MontarMenuArma;
        MontarMenuArma(menu.MeuRobo.Fisico, menu.MeuRobo);
        //f
    }
    public void TrocarBateria(Battery bateria)
    {
        BotaoBateria.GetComponent<BatteryButton>().Coremenu = this;
        BotaoBateria.transform.GetChild(0).GetComponent<Text>().text = bateria.GetComponent<Battery>().Charge.ToString();
        Bateria = bateria.gameObject;
    }
    public void MontarMenuArma(Weapon arma,FantoRob robo)
    {
        Arma = arma;
        //arma atualmente equipada
        Button botao = Instantiate(BotaoArmaAtual, SpacerWeapon.transform) as Button;
        botao.GetComponent<WeaponButton>().MyWeapon = arma;
        botao.GetComponent<WeaponButton>().Menu = this;
        botao.transform.GetChild(0).GetComponent<Image>().sprite = arma.MySprite;
        botao.transform.GetChild(1).GetComponent<Text>().text = arma.Nome[ManagerGame.Instance.Idm];
        botao.transform.GetChild(2).GetComponent<Text>().text = arma.Forca.ToString();

        //gera os menus de ataques
        int instancia = 1;
        foreach(Transform child in SpacerAtackList.transform)
        {
            GameObject.Destroy(child);
        }
        foreach(Attack ataque in arma.Ataque)
        {
            if(ataque !=null)
            {
                Instantiate(AttackNote, SpacerAtackList.transform).GetComponent<AttackNote>().Gerar(ataque, instancia,robo);
                instancia++;
            }
        }

        //gera os demonstrativos de combo;
        foreach (ComboNote c in ListaCombo)
        {
            c.gameObject.SetActive(true);
        }
        for (int i = 0; i < arma.Combo.Count; i++)
        {
            ListaCombo[i].gameObject.SetActive(true);
            ListaCombo[i].Gerar(arma.Combo[i]);
        }

    }
    public void GerarMenuTodasArmas()
    {
        foreach(GameObject arma in playerObjects.Weapons)
        {
            Button botao = Instantiate(BotaoArmaAtual, SpacerWeapon.transform) as Button;
            botao.GetComponent<WeaponSelectionButton>().MyWeapon = arma.GetComponent<Weapon>();
            botao.GetComponent<WeaponSelectionButton>().Menu = this;
            botao.transform.GetChild(0).GetComponent<Image>().sprite = arma.GetComponent<Weapon>().MySprite;
            botao.transform.GetChild(1).GetComponent<Text>().text = arma.GetComponent<Weapon>().Nome[ManagerGame.Instance.Idm];
            botao.transform.GetChild(2).GetComponent<Text>().text = arma.GetComponent<Weapon>().Forca.ToString();
        }
    }
    public void TrocarArma(Weapon armanova, GameObject botaoarma)
    {
        Weapon armaantiga = Arma;
        MontarMenuArma(armanova, Menu.MeuRobo);

        GameObject.Destroy(botaoarma.gameObject);
        //criar botao novo para a arma antiga
        Button botao = Instantiate(BotaoArmaAtual, SpacerWeapon.transform) as Button;
        botao.GetComponent<WeaponButton>().MyWeapon = armaantiga;
        botao.GetComponent<WeaponButton>().Menu = this;
        botao.transform.GetChild(0).GetComponent<Image>().sprite = armaantiga.MySprite;
        botao.transform.GetChild(1).GetComponent<Text>().text = armaantiga.Nome[ManagerGame.Instance.Idm];
        botao.transform.GetChild(2).GetComponent<Text>().text = armaantiga.Forca.ToString();
    }

    public void Concluir()
    {
        Menu.ReceberNucleo(this.gameObject, Arma);
        foreach(Transform child in SpacerWeapon.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (Transform child in SpacerAllWeapons.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (Transform child in SpacerAtackList.transform)
        {
            GameObject.Destroy(child.gameObject);
        }        
        gameObject.SetActive(false);
    }

}
