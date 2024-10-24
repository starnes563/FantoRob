using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboNote : MonoBehaviour
{    
    public GameObject[] ComboBillboard = new GameObject[6];
    public Text[] Texto = new Text[6];
   
    public void Gerar(Combo combo)
    {
        for(int i = 0; i<6;i++)
        {
            if(combo.ComboID[i]!=0)
            {
                ComboBillboard[i].SetActive(true);
                Texto[i].text = combo.ComboID[i].ToString();
            }
        }
    }
}
