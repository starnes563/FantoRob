using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    //0 a 5
   [HideInInspector]
    public RobotPart Parte;
    public Plug[] Plugs = new Plug[5];
    public ChooseRobotMenu RobotMenu;    
    // Start is called before the first frame update
    public void Abrir(RobotPart prt)
    {
        Parte = prt;
        //desapla todos os pentes
        foreach (Plug p in Plugs)
        {
            p.LimparPlug();
        }
        //verifica se ja ha pentes
        foreach (Pente pt in Parte.Pente)
        {            
            if (pt!=null)
            {               
                Plugs[System.Array.IndexOf(Parte.Pente, pt)].Acoplar(pt,false);
            }
        }

    }
     public int Compilar(int i)
    {
        int value = 0;
         foreach(Plug p in Plugs)
        {
            value += p.RetornarMeuValor(i);
        }
        return value;
    }
    public int CalcularGasto()
    {
        int value = 0;
         foreach(Plug p in Plugs)
        {
            value += p.RetornarMeuGasto();
        }
        return value;
    }
    public void Compilador()
    {
        for(int i = 0; i<6;i++)
        {
            Parte.Value[i] = Compilar(i);
        }        
        Parte.Energyspent = CalcularGasto();
        RobotMenu.RemontarRobo();
    }
    public void Concluir()
    {
        this.gameObject.SetActive(false);
        Plugs[0].MenuPentes.gameObject.SetActive(false);
    }

}
