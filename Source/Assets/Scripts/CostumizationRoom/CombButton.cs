using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombButton : MonoBehaviour
{
    public Pente MyThing;
    public UnMerger unmerger;
    public Text[] Valores = new Text[6];
    public Text Gasto;
    public Text Move;
    public GameObject Confima;
    // Update is called once per frame

    public void Clicar()
    {
        unmerger.escolherPente(MyThing);
    }
    public void PassarValores(Pente p)
    {
        for( int i = 0; i<6; i++)
        {
            Valores[i].text = p.Valor[i].ToString();
        }
        Gasto.text = (string)p.GastoAtual.ToString();
        if (p.Move.Aleatório)
        {
            Move.text = (string)p.Move.Nome;
        }
        else
        {
            Move.text = (string)p.Move.NamesLang[ManagerGame.Instance.Idm];
        }
       // Move.text = (string)p.Move.Nome;
    }


}
