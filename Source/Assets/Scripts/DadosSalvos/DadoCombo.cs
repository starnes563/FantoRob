using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class DadoCombo
{
    public int[] ComboID = new int[6];

    public DadoCombo(Combo c)
    {
        for( int i = 0; i<6; i++)
        {
            if(c.ComboID !=null)
            {
                ComboID[i] = c.ComboID[i];
            }
        }
    }
}
