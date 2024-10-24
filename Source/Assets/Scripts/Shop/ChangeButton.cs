using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeButton : MonoBehaviour
{
    Weapon MeuFisico;
    ShopMenuManager MeuMenu;
    public Text Nome;
    public Image MeuSprite;
    QuadroNucleoFisico QuadroFisico;
    public GameObject BotaoConfirmar;
    public GameObject BotaoDesistir;
    public void Criar(Weapon wp, ShopMenuManager menu, QuadroNucleoFisico qd)
    {
        MeuFisico = wp;
        MeuMenu = menu;
        QuadroFisico = qd;

        Nome.text = MeuFisico.Nome[ManagerGame.Instance.Idm];
        MeuSprite.sprite = MeuFisico.MySprite;
    }
    private void OnMouseEnter()
    {
        MeuMenu.QuadroConstruir.QuadorRobo.gameObject.SetActive(false);
        QuadroFisico.Mostrar(MeuFisico);
        QuadroFisico.gameObject.SetActive(true);
    }
    private void OnMouseExit()
    {
        QuadroFisico.gameObject.SetActive(false);
    }
    public void Clicar()
    {       
        MeuMenu.ApagarBotaoEmTodos();
        ManagerShop.TocarSomConfimar();
        BotaoConfirmar.SetActive(true);
        BotaoDesistir.SetActive(true);
    }
    public void Confirmar()
    {
        MeuMenu.ApagarBotaoEmTodos();
        ManagerShop.TocarSomConfimar();
        MeuMenu.Concluir(MeuFisico);
    }
    public void Desistir()
    {
        ManagerShop.TocarSomDesiste();
        BotaoConfirmar.SetActive(false);
        BotaoDesistir.SetActive(false);
    }
    public void DesativarBotao()
    {       
        BotaoConfirmar.SetActive(false);
        BotaoDesistir.SetActive(false);
    }
}
