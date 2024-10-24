using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetaDeTroca : MonoBehaviour
{
    public ChooseRobotMenu MeuMenu;
    public int ValorBase;
    [HideInInspector]
    public int valoratual;
   
    public void Reiniciar()
    {
        valoratual = ValorBase;
    }
    public void Clicou()
    {
        MeuMenu.TrocarFantorob(valoratual);
        
    }
}
