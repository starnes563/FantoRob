using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombSelectionButton : MonoBehaviour
{
    [HideInInspector]
    public Plug PlugAtual;
    [HideInInspector]
    public Pente MeuPente;
    [HideInInspector]
    public CombSelectionMenu Menu;
    public Text Nome;
    public Text[] Valor = new Text[6];
    public Text Gasto;
    public Text NomedoMove;

    public void Criar(Pente pente, Plug pl, CombSelectionMenu m)
    {
        PlugAtual = pl;
        MeuPente = pente;
       // Nome.text = pente.Nome;
        for (int i = 0; i < 6; i++)
        {
            Valor[i].text = pente.Valor[i].ToString();
        }
        Gasto.text = pente.GastoAtual.ToString();
        if (pente.Move.Aleatório)
        {
            NomedoMove.text = pente.Move.Nome;
        }
        else
        {
            NomedoMove.text = pente.Move.NamesLang[ManagerGame.Instance.Idm];
        }
        //NomedoMove.text = pente.Move.Nome;
        Menu = m;
    }
    public void Clicar()
    {
        PlugAtual.Acoplar(MeuPente,true);       
        Menu.Criar(PlugAtual);
        Menu.gameObject.SetActive(false);
    }
    public void Mostrar(Pente pente)
    {
       // Nome.text = pente.Nome;
        for (int i = 0; i < 6; i++)
        {
            Valor[i].text = pente.Valor[i].ToString();
        }
        Gasto.text = pente.GastoAtual.ToString();
        NomedoMove.text = pente.Move.Nome;
    }
    public void Esconder()
    {
      //  Nome.text = "";
        for (int i = 0; i < 6; i++)
        {
            Valor[i].text = "";
        }
        Gasto.text = "";
        NomedoMove.text = "";
    }
}
