using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeRobot : MonoBehaviour
{
    public Button BotaoRobo;
    public Image PartySpacer;
    public Image FantoroboSpacer;
   // public GameObject Aviso;
    public PlayerObjects playerObjects;
    public GameObject SubMenu;
    public bool SubAberto = false;
    public GameObject CoreMenu;
    public GameObject NotSpaceBillBoard;
    private bool billOpen = false;
    public GameObject PartyObject;
    public GameObject FantoRobObject;
    private bool partyActive = false;
    private bool fantoActive = false;
    // Start is called before the first frame update
    void Start()
    {
        playerObjects = GameObject.FindWithTag("Gerenciador").GetComponent<PlayerObjects>();
    }

    // Update is called once per frame
    void Update()
    {

        if (partyActive && !SubAberto && !billOpen)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                FecharParty();
            }
        }
        if (fantoActive && !SubAberto && !billOpen)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                FecharFantorob();
            }
        }
    }
    public void AbrirParty()
    {
        PartyObject.SetActive(true);
        partyActive = true;
    }
    public void AbrirFantorob()
    {
        FantoRobObject.SetActive(true);
        fantoActive = true;
    }
    void FecharParty()
    {
        PartyObject.SetActive(false);
        partyActive = false;
    }
    void FecharFantorob()
    {
        FantoRobObject.SetActive(false);
        fantoActive = false;
    }
    void criarMenuParty()
    {
        foreach(FantoRob thing in PlayerObjects.RobotsInUse)
        {
            criarBotao(thing, PartySpacer, true);
        }
    }
    void criarMenuFantorob()
    {
        foreach (FantoRob thing in PlayerObjects.RobotsNotInUse)
        {
            criarBotao(thing, FantoroboSpacer, false);
        }
    }
    void criarBotao(FantoRob thing, Image spacer, bool party)
    {
 
    }
    public void ShowSubMenuParty(FantoRob thing)
    {
        SubMenu.GetComponent<ChangeSubMenu>().MyRobot = thing;
        SubMenu.GetComponent<ChangeSubMenu>().Menu = this;
        SubMenu.GetComponent<ChangeSubMenu>().party = true;
        SubMenu.GetComponent<ChangeSubMenu>().Iniciar();
        SubAberto = true;
    }
    public void ShowSubMenuFanto(FantoRob thing)
    {
        SubMenu.GetComponent<ChangeSubMenu>().MyRobot = thing;
        SubMenu.GetComponent<ChangeSubMenu>().Menu = this;
        SubMenu.GetComponent<ChangeSubMenu>().party = false;
        SubMenu.GetComponent<ChangeSubMenu>().Iniciar();
        SubAberto = true;
    }
    public void SeeRobot(FantoRob Robot)
    {
        CoreMenu.GetComponent<ShowRobot>().CriarMenu(Robot, null);
        CoreMenu.SetActive(true);
    }
     public void RetirarParty(FantoRob Robot)
    {
        PlayerObjects.RobotsNotInUse.Add(Robot);
        FantoRob[] novoArray = new FantoRob[3];
        int instancia = 0;
        foreach(FantoRob robot in PlayerObjects.RobotsInUse)
        {
            if (robot != null && robot.IndividualCode != Robot.IndividualCode)
            {
                novoArray[instancia] = robot;
                instancia++;
            }
        }
        for(int i = 0; i<3; i++)
        {
            PlayerObjects.RobotsInUse[i] = novoArray[i];
        }
    }
    public void PorParty(FantoRob Robot)
    {
        bool emptySpace = false;
        foreach(FantoRob robot in PlayerObjects.RobotsInUse)
        {
            if(robot == null)
            {
                emptySpace = true;
                break;
            }
        }               
        if(emptySpace)
        {
            int isnt = 0;
            foreach (FantoRob robot in PlayerObjects.RobotsInUse)
            {
                if (robot == null)
                {
                    PlayerObjects.RobotsInUse[isnt] = Robot;
                    break;
                }
                isnt++;
            }           
            foreach(FantoRob robot in PlayerObjects.RobotsNotInUse)
            {
                if (robot != null && robot.IndividualCode != Robot.IndividualCode)
                {
                    PlayerObjects.RobotsNotInUse.Add(robot);
                }
            }          
        }
        else
        {
            NotSpaceBillBoard.GetComponent<NotSpace>().Ativo = true;
            NotSpaceBillBoard.SetActive(true);
        }
    }
}
