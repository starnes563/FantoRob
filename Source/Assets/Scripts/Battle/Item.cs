using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    // Start is called before the first frame update
  //  [HideInInspector]
    public int Itemid;
    public List<string> Nome;
    public int Quantidade;
    public List<string> Descricao;
    public Sprite Sprite;
    public int Tipo;
    public int GastoAcoes;
    public float Fator;
    [HideInInspector]    
    public void CriarItem(int quantidade)
    {
        Quantidade += quantidade;
    }
    public void GastarItem(int quantidade)
    {
        
        Quantidade -= quantidade;

        if(Quantidade < 0)
        {
            Quantidade = 0;
        }
    }
}


