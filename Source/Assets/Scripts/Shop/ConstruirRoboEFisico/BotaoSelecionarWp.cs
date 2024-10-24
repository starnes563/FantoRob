using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotaoSelecionarWp : MonoBehaviour
{
    Weapon MeuFisico;
    MenuSelecionarWp MeuMenu;
    public Text Nome;
    public Image MeuSprite;
    PainelNFATK QuadroFisico;
    public GameObject BotaoConfirmar;
    public GameObject BotaoDesistir;
    public void Criar(Weapon wp, MenuSelecionarWp menu,PainelNFATK qd)
    {
        MeuFisico = wp;
        MeuMenu = menu;
        QuadroFisico = qd;

        Nome.text = MeuFisico.Nome[ManagerGame.Instance.Idm];
        //MeuSprite.sprite = MeuFisico.MySprite;
    }
    private void OnMouseEnter()
    {
        QuadroFisico.Mostrar(MeuFisico);
        QuadroFisico.gameObject.SetActive(true);
    }
    private void OnMouseExit()
    {
        //QuadroFisico.gameObject.SetActive(false);
    }
    public void Clicar()
    {
        Leticia.TocarSomConfimar();
        MeuMenu.Concluir(MeuFisico);
    }
    public void Confirmar()
    {
        Leticia.TocarSomConfimar();
       
    }
    public void Desistir()
    {
        Leticia.TocarSomDesiste();
        BotaoConfirmar.SetActive(false);
        BotaoDesistir.SetActive(false);
    }

}
