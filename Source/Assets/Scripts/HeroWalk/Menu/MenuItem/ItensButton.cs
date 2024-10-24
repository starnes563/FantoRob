using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItensButton : MonoBehaviour
{
    public PlayerMenu Menu;
    // Start is called before the first frame update   
    public void Clicou()
    {
        SonsMenu.Confimar();
        Menu.AbrirMenuItens();
    }
}
