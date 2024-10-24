using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtVerAtaque : MonoBehaviour
{
    [HideInInspector]
    public Attack MeuAtaque;
    public int ID;
    public QuadroAtaque quadro;
    public MenuTrocarAtaque MenuTrocarAt;
    public Text Nome;
    private void OnMouseEnter()
    {
        if(MeuAtaque !=null)
        {
            quadro.Mostrar(MeuAtaque);
        }
        
       
    }
    private void OnMouseExit()
    {
        if (MeuAtaque != null)
        {
            quadro.Esconder();
        }        
    }
    public void Clicar()
    {
        MenuTrocarAt.Criar(this);
    }

    public void Trocar(Move mv)
    {
        Nome.text = mv.Nome;
        MeuAtaque = MenuTrocarAt.RobotMenu.MeuFantorob.Fisico.Ataque[ID];
    }

}
