using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatorobButton : MonoBehaviour
{
    [HideInInspector]
    public FantoRob MyRobot;
    [HideInInspector]
    public FantorobMenu menu;
    [HideInInspector]
    public int Index;
    public GameObject MenuSecundario;
    [HideInInspector]
    public QuadroRobo QuadroRobo;
    [HideInInspector]
    public PlayerMenu PMenu;
    
    public void Clicou()
    {
        SonsMenu.Confimar();
        menu.EsconderTodos();
        MenuSecundario.SetActive(true);
    }
    public void Ver()
    {
        SonsMenu.Confimar();
        menu.Robot = MyRobot;
        menu.Mostrar();
    }
    public void Trocar()
    {       
        menu.Robot = MyRobot;
        menu.AdicionarAParty(Index);
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
        QuadroRobo.Mostrar(MyRobot);
    }
    private void OnMouseExit()
    {
        QuadroRobo.Esconder();
    }
}
