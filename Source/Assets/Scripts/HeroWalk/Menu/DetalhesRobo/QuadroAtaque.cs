using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuadroAtaque : MonoBehaviour
{
    public Text Nome;
    public Text Forca;
    public Text Efeito;
    public Text AumentoEfeito;
    public Text GastoEnergia;
    public Text Precisao;
    public List<GameObject> BarrasAcoes = new List<GameObject>();
    public Text Elemental;
    public Text Descricao;

    public void Mostrar(Attack ataque)
    {
        Elemental.gameObject.SetActive(false);
        Nome.text = ataque.Nome;
        Forca.text = ataque.Forca.ToString();
        switch(ataque.Efeito)
        {
            case 0:
                Efeito.text = "";
                break;
            case 1:
                Efeito.text = "Spy";
                break;
            case 2:
                Efeito.text = "KeyLogger";
                break;
            case 3:
                Efeito.text = "Trojan";
                break;
            case 4:
                Efeito.text = "Ranson";
                break;
            case 5:
                Efeito.text = "Worm";
                break;
            case 6:
                Efeito.text = "Vírus";
                break;
        }        
        AumentoEfeito.text = ataque.AumentoEfeito.ToString();
        GastoEnergia.text = ataque.GastoEnergia.ToString();
        Precisao.text = ataque.Precisao.ToString();
        if (ataque.Elemental) { Elemental.gameObject.SetActive(true); }       
        for (int i = 0; i<ataque.UsoDeAcoes; i++)
        {
            BarrasAcoes[i].SetActive(true);
        }
        if(ataque.Move2.Aleatório)
        {
            Descricao.text = ataque.Move2.Descrição[0];
        }
        else
        {
            Descricao.text = ataque.Move2.Descrição[ManagerGame.Instance.Idm];
        }               
    }
    public void Esconder()
    {
        Nome.text = "";
        Forca.text = "";
        Efeito.text = "";
        AumentoEfeito.text = "";
        GastoEnergia.text = "";
        Precisao.text = "";
        Descricao.text = "";
        Elemental.gameObject.SetActive(false);
        foreach (GameObject barra in BarrasAcoes)
        {
            barra.SetActive(false);
        }
    }

}
