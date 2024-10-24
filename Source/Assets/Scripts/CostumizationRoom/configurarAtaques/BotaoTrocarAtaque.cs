using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotaoTrocarAtaque : MonoBehaviour
{
    [HideInInspector]
    public Move M;
    [HideInInspector]
    public QuadroAtaque quadro;
    [HideInInspector]
    public MenuTrocarAtaque MenuTrocarAt;
    public Text Nome;
    public GameObject MostrarConfirma;
    enum estado
    {
        SEMSELECIONAR,
        SELECIONADO,
    }
   // private estado std;

    public void Criar(Move mv, MenuTrocarAtaque m, QuadroAtaque qd)
    {
        M = mv;
        MenuTrocarAt = m;
        quadro = qd;
        if (mv.Aleatório)
        {
            Nome.text = mv.Nome;
        }
        else
        {
            Nome.text = mv.NamesLang[ManagerGame.Instance.Idm];
        }        
    }  
    public void Clicou()
    {
        quadro.Esconder();
        MenuTrocarAt.TrocarAtaque(M);
        MostrarConfirma.SetActive(false);
        //std = estado.SEMSELECIONAR;
        //switch (std)
        //{
            //case estado.SEMSELECIONAR:
              //  quadro.Esconder();
                //Attack at = ScriptableObject.CreateInstance<Attack>();
                //at.GerarAtaque(M, 0);
                //quadro.Mostrar(at);
                //MostrarConfirma.SetActive(true);
                //std = estado.SELECIONADO;
                //break;
            //case estado.SELECIONADO:
               // quadro.Esconder();
                //MenuTrocarAt.TrocarAtaque(M);
                //MostrarConfirma.SetActive(false);
                //std = estado.SEMSELECIONAR;
                //break;
        //}
        
    }
    public void Esconder()
    {
       // std = estado.SEMSELECIONAR;
       // MostrarConfirma.SetActive(false);
    }
    private void OnMouseOver()
    {
        Attack at = ScriptableObject.CreateInstance<Attack>();
        at.GerarAtaque(M, 0);        
        quadro.Mostrar(at);
    }
    private void OnMouseExit()
    {
        quadro.Esconder();
    }
}
