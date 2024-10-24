using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotButton : MonoBehaviour
{
    public Merger MenuSoldar;
    public int Slot;
    public GameObject[] Simbolos = new GameObject[6];
    [HideInInspector]
    public enum estado
    {
        SEMCHIP,
        COMCHIP,     
    }
    [HideInInspector]
    public estado St = estado.SEMCHIP;

    public void Clicar()
    {
        switch (St)
        {
            case estado.SEMCHIP:
                MenuSoldar.PorSlot(Slot, this);               
                break;
            case estado.COMCHIP:
                MenuSoldar.RetirarSlot(Slot, this);
                foreach (GameObject sim in Simbolos)
                {
                    if(sim.activeSelf == true)
                    {
                        sim.gameObject.SetActive(false);
                    }                    
                }                
                break;
        }
    }
   public void Ativarimagem()
    {
        int tp = 0;
        switch (MenuSoldar.Slots[Slot])
        {
            case 0:
                tp = 0;
                break;
            case 1:
                tp = 0;
                break;
            case 2:
                tp = 1;
                break;
            case 3:
                tp = 1;
                break;
            case 4:
                tp = 2;
                break;
            case 5:
                tp = 2;
                break;
            case 6:
                tp = 2;
                break;
            case 7:
                tp = 3;
                break;
            case 8:
                tp = 3;
                break;
            case 9:
                tp = 4;
                break;
            case 10:
                tp = 4;
                break;
            case 11:
                tp = 4;
                break;
            case 12:
                tp = 4;
                break;
            case 13:
                tp = 5;
                break;
            case 14:
                tp = 5;
                break;
        }
        Simbolos[tp].SetActive(true);       
    
    }
    public void DesativarImagem()
    {
        foreach (GameObject sim in Simbolos)
        {
            if (sim.activeSelf)
            {
                sim.gameObject.SetActive(false);
            }
        }
    }

}
