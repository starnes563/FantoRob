using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyButton : MonoBehaviour
{
    public PlayerMenu Menu;   
    public void Clicou()
    {
        SonsMenu.Confimar();
        Menu.AbrirMenuParty();
    }
}
