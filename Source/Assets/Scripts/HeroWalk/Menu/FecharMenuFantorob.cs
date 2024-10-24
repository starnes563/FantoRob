using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FecharMenuFantorob : MonoBehaviour
{
    public PartyMenu PartyMenu;
    public PlayerMenu PlayerMenu;
    public FantorobMenu FantorobMenu;

    public void Clicou()
    {
        SonsMenu.Desistir();
        if(PartyMenu.ItemUsar !=null)
        {
            PartyMenu.Fechar();
            FantorobMenu.Fechar();
            PlayerMenu.AbrirMenuItens();
        }
        else
        {
            PartyMenu.Fechar();
            FantorobMenu.Fechar();
        }
    }

}
