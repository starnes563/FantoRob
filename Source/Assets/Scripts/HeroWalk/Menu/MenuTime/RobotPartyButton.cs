using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotPartyButton : MonoBehaviour
{
    [HideInInspector]
    public FantoRob MyRobot;
    [HideInInspector]
    public PartyMenu partyMenu;
    [HideInInspector]
    public ItensMenu itensMenu;
    [HideInInspector]
    public PlayerMenu PlayerMenu;
    [HideInInspector]
    public int index;
    public GameObject MenuSecundario;
    public QuadroRobo QuadroRobo;
    public GameObject BotaoTirar;
    // Start is called before the first frame update
    void Start()
    {
        itensMenu = PlayerMenu.ItenMenu.GetComponent<ItensMenu>();
        MenuSecundario.GetComponent<RobotPartyButtonSec>().Botao = this;
    }
      public void Clicou()
    {
        SonsMenu.Confimar();
        partyMenu.EsconderTodos();
       if(partyMenu.ItemUsar == null)
        {
            MenuSecundario.SetActive(true);
            if(ManagerGame.Instance.Regiao.Desafio)
            {
                BotaoTirar.gameObject.SetActive(false);
            }
        }
        else
        {
            itensMenu.MyRobot = MyRobot;
            itensMenu.UsarItem();
            SonsMenu.Recuperar();
        }
        if (MyRobot != null)
        {
            QuadroRobo.Mostrar(MyRobot);
        }
    }
    public void Retirar()
    {
        SonsMenu.Confimar();
        partyMenu.Tirar(index, MyRobot);
    }
    public void TornarLider()
    {
        SonsMenu.Confimar();
        partyMenu.Robot = MyRobot;
        partyMenu.TornarLider(MyRobot, index);
    }
    public void Ver()
    {
        SonsMenu.Confimar();
        partyMenu.Robot = MyRobot;
        partyMenu.VerSumario();
    }
    public void UsarItem()
    {
        SonsMenu.Confimar();
        partyMenu.Robot = MyRobot;
        partyMenu.UsarItem();
    }
    public void EsconderSecond()
    {
        MenuSecundario.SetActive(false);
    }
    public void Destruir()
    {
        Destroy(gameObject);
    }
    private void OnMouseEnter()
    {
        if(MyRobot!=null)
        {
            QuadroRobo.Mostrar(MyRobot);
        }       
    }
    private void OnMouseExit()
    {
        if (MyRobot != null)
        {
            QuadroRobo.Esconder();
        }
    }

}
