using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Combo
{
    public int[] ComboID = new int[6];
    public void GerarCombo(int id, int i)
    {
        
        ComboID[i]=id;        
    }
    public void CarregarSalvo(DadoCombo c)
    {
        for (int i = 0; i < 6; i++)
        {
            if (c.ComboID != null)
            {
                ComboID[i] = c.ComboID[i];
            }
        }
    }
}
