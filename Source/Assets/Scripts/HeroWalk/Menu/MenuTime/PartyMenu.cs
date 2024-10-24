using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyMenu : MonoBehaviour
{
    [HideInInspector]
    public Item ItemUsar;
    public GameObject SecondMenu;
    public Button RobotButton;
    public Image Spacer;   
    [HideInInspector]
    public FantoRob Robot;
    [HideInInspector]
    public ItensMenu itensMenu;
    [HideInInspector]
    public PlayerMenu PlayerMenu;
    [HideInInspector]
    public GameObject NovoRoboParty;
    [HideInInspector]
    public GameObject RobotPartyRetirar;
    public QuadroRobo QuadroRobo;
    List<GameObject> botoes = new List<GameObject>();
    public FantorobMenu MenuFantorob;
    // Start is called before the first frame update      
    public void Criar(PlayerMenu menu)
    {
        this.transform.position = new Vector3(this.transform.position.x, -7.23f,30f);
        LeanTween.moveLocalY(this.gameObject, 2.36f, 0.3f);
        if (botoes.Count > 0)
        {
            foreach (GameObject g in botoes)
            {
                Destroy(g);
            }
            botoes.Clear();
        }
        PlayerMenu = menu;
        itensMenu = PlayerMenu.ItenMenu.GetComponent<ItensMenu>();
        int inde = 0;
        if(PlayerObjects.RobotsInUse.Count>0)
        {
            foreach (FantoRob robo in PlayerObjects.RobotsInUse)
            {
                Button botao = Instantiate(RobotButton, Spacer.transform) as Button;
                botoes.Add(botao.gameObject);
                botao.GetComponent<RobotPartyButton>().MyRobot = robo;
                botao.GetComponent<RobotPartyButton>().partyMenu = this;
                botao.GetComponent<RobotPartyButton>().PlayerMenu = menu;
                botao.GetComponent<RobotPartyButton>().QuadroRobo = QuadroRobo;
                botao.GetComponent<RobotPartyButton>().index = inde;
                inde++;
                botao.transform.GetChild(0).GetComponent<Image>().sprite = robo.MenuIconeFantorob;
                botao.transform.GetChild(1).GetComponent<Text>().text = robo.Nome;
            }
        }        
    }
    public void RefazerMenu()
    {
        if (botoes.Count > 0)
        {
            foreach (GameObject g in botoes)
            {
                Destroy(g);
            }
            botoes.Clear();
        }
        int inde = 0;
        foreach (FantoRob robo in PlayerObjects.RobotsInUse)
        {
            Button botao = Instantiate(RobotButton, Spacer.transform) as Button;
            botoes.Add(botao.gameObject);
            botao.GetComponent<RobotPartyButton>().MyRobot = robo;
            botao.GetComponent<RobotPartyButton>().partyMenu = this;
            botao.GetComponent<RobotPartyButton>().QuadroRobo = QuadroRobo;
            botao.GetComponent<RobotPartyButton>().PlayerMenu = PlayerMenu;
            botao.GetComponent<RobotPartyButton>().index = inde;
            inde++;
            botao.transform.GetChild(0).GetComponent<Image>().sprite = robo.MenuIconeFantorob;
            botao.transform.GetChild(1).GetComponent<Text>().text = robo.Nome;
        }
    }
    public void ShowSecondMenu(FantoRob robo)
    {
        Robot = robo;
    }

    public void UsarItem()
    {
        itensMenu.MyRobot = Robot;
        PlayerMenu.AbrirMenuItens();
    }
    public void VerSumario()
    {
        PlayerMenu.AbrirMenuCore(Robot);
    }
    public void TornarLider(FantoRob robot, int index)
    {
        if(PlayerObjects.RobotsInUse.Count>1)
        {
            PlayerObjects.RobotsInUse[index] = PlayerObjects.RobotsInUse[0];
            PlayerObjects.RobotsInUse[0] = robot;
            RefazerMenu();
        }
        else
        {
            SonsMenu.Negado();
        }

        //List<GameObject> novalista = new List<GameObject>(3);
        //novalista[0] = Robot;
        //int i = 1;
        //foreach(GameObject fanto in playerObjects.RobotsInUse)
        //{
           // if(fanto.GetComponent<Squeleton>().ID!=Robot.GetComponent<Squeleton>().ID)
           // {
               // novalista[i] = fanto;
               // i++;
            //}
       // }
       // for(int t=0; t<3;t++)
       // {
            //playerObjects.RobotsInUse[t] = novalista[t];
        //}
    }
   public void Fechar()
    {
        PlayerMenu.MostrarQuadro();
        Robot = null;
        ItemUsar = null;
        this.gameObject.SetActive(false);        
    }
    public void Tirar(int index, FantoRob robot)
    {
        if(PlayerObjects.RobotsInUse.Count == 1)
        {
            SonsMenu.Negado();
        }
        else
        {
            PlayerObjects.RobotsNotInUse.Add(robot);
            PlayerObjects.RobotsInUse.Remove(PlayerObjects.RobotsInUse[index]);
            RefazerMenu();
            MenuFantorob.RefazerMenu();
        }       
    }
    public void EsconderTodos()
    {
        if (botoes.Count > 0)
        {
            foreach (GameObject g in botoes)
            {
                g.GetComponent<RobotPartyButton>().MenuSecundario.SetActive(false);
            }
        }
    }
    public void LimparItem()
    {
        ItemUsar = null;
    }
}
