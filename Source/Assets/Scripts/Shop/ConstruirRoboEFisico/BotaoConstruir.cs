using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotaoConstruir : MonoBehaviour
{
    private Construivel Construivel;
    private QuadroConstruir quadro;
    public Image SpriteFantorob;
    public Image SpriteFisico;
    public Text MeuTexto;
    public GameObject Confimar;
    public GameObject Desistir;
    private MenuSelecionarWp menuSelecionarFisico;
    Leticia let;
    public void Criar( Construivel ct, QuadroConstruir qd, MenuSelecionarWp wp, Leticia l)
    {
        let = l;
        menuSelecionarFisico = wp;
        Construivel = ct;
        quadro = qd;
        switch (Construivel.Tipo)
        {
            case 0:
                //SpriteFantorob.gameObject.SetActive(true);
               // SpriteFisico.gameObject.SetActive(false);
               // SpriteFantorob.sprite = Construivel.RertonarSprite();
                break;
            case 1:
                //SpriteFantorob.gameObject.SetActive(false);
                //SpriteFisico.gameObject.SetActive(true);
                //SpriteFisico.sprite = Construivel.RertonarSprite();
                break;
        }
        string nm = Construivel.RetornarNome();
        nm.ToLower();
        FirstCharSubstring(nm);
        MeuTexto.text = nm;
    }
    public void Clicar()
    {
        let.EsconderSub();
        quadro.Apagar();
        quadro.Mostrar(Construivel);
        //Confimar.SetActive(true);
        //Desistir.SetActive(true);
        Leticia.TocarSomConfimar();
    }
    public void Confirmar()
    {
        if(Construivel.PossoConstruir())
        {
            Leticia.TocarSomConfimar();
            switch (Construivel.Tipo)
            {

                case 0:
                    //menu de seleção de nucleo                  
                    menuSelecionarFisico.Criar(Construivel);
                    menuSelecionarFisico.gameObject.SetActive(true);
                    break;
                case 1:
                    Construivel.Construir();
                    break;
            }
            
        }
        else
        {
            Leticia.TocarSomNaoPode();
        }
       
    }   
    public void des()
    {
        Leticia.TocarSomDesiste();
    }
    private void OnMouseEnter()
    {
        quadro.Mostrar(Construivel);
    }
    private void OnMouseExit()
    {
        quadro.Apagar();
    }
    public void EsconderSubBotoes()
    {
        Confimar.SetActive(false);
        Desistir.SetActive(false);
    }
    public string FirstCharSubstring(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return string.Empty;
        }
        return $"{input[0].ToString().ToUpper()}{input.Substring(1)}";
    }
}
