using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigurarAtaques : MonoBehaviour
{
    public BtVerAtaque[] InterfaceAtaque = new BtVerAtaque[6];
    public ChooseRobotMenu MenuRobo;    
    public GameObject ComboNote;
    public GameObject MenuTrocarAtaque;
    public List<ComboNote> ListaCombo = new List<ComboNote>();  
    public void IniciarMenu()
    {
        MenuTrocarAtaque.SetActive(false);
        //montar ataques
        Weapon arma = MenuRobo.MeuFantorob.Fisico;
        for (int i = 0; i < arma.AttacksMax; i++)
        {
            InterfaceAtaque[i].gameObject.SetActive(true);
            if (arma.Ataque[i] != null)
            {
                InterfaceAtaque[i].GetComponent<BtVerAtaque>().MeuAtaque = arma.Ataque[i];
                InterfaceAtaque[i].transform.GetChild(0).GetComponent<Text>().text = arma.Ataque[i].Nome;
            }
            else
            {
                InterfaceAtaque[i].transform.GetChild(0).GetComponent<Text>().text = "";
            }
        }
        //gera os demonstrativos de combo;
        foreach (ComboNote c in ListaCombo)
        {
            c.gameObject.SetActive(false);
        }
        for (int i = 0; i < arma.Combo.Count; i++)
        {
            ListaCombo[i].gameObject.SetActive(true);
            ListaCombo[i].Gerar(arma.Combo[i]);
        }
    }
    public void Finalizar()
    {
        MenuTrocarAtaque.SetActive(false);
        this.gameObject.SetActive(false);
        for (int i = 0; i < 6; i++)
        {
            InterfaceAtaque[i].gameObject.SetActive(false);
        }
    }
}
