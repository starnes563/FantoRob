using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenuManager : MonoBehaviour
{
    //colcoar nucleio fisico no fantorob comprado com credito
    ShopStock Const;
    public Image Spacer;
    public Button BotaoFisico;
    List<GameObject> botoes = new List<GameObject>();
    public QuadroNucleoFisico QuadroFisico;
    public PreShopMenu QuadroConstruir;
    public GameObject Caixa;
    public List<Weapon> Wp = new List<Weapon>();
    public Transform MainCamera;
    public bool DarNaMissao = false;
    public GameObject Loja;
    public void Criar(ShopStock ct)
    {
        this.transform.position = new Vector3(this.transform.position.x, -7.23f);
        LeanTween.moveLocalY(this.gameObject, -2.006f, 0.3f);
        Const = ct;
        QuadroConstruir.IniciarCompra(ct);
        if (botoes.Count > 0)
        {
            foreach (GameObject bt in botoes)
            {
                Destroy(bt);
            }
            botoes = new List<GameObject>();
        }
        if (Wp.Count > 0)
        {
            foreach (Weapon wp in Wp)
            {
                Button bt = Instantiate(BotaoFisico, Spacer.transform) as Button;
                botoes.Add(bt.gameObject);
                bt.GetComponent<ChangeButton>().Criar(wp, this, QuadroFisico);
            }
        }
        else
        {
            foreach (Weapon wp in PlayerObjects.NucleosFisicos)
            {
                Button bt = Instantiate(BotaoFisico, Spacer.transform) as Button;
                botoes.Add(bt.gameObject);
                bt.GetComponent<ChangeButton>().Criar(wp, this, QuadroFisico);
            }
        }
    }
    public void Concluir(Weapon wp)
    {       
        //poe weapon no fantorob
        Const.Fantorob.Fisico = Instantiate(wp);
        Const.Fantorob.SpriteFisico = wp.MySprite;
        //ativaFantorob
        Const.Comprar(1, DarNaMissao);
        //retirar o nucleo do inventario
        if(!DarNaMissao)
        {
            PlayerObjects.NucleosFisicos.Remove(wp);
            Destroy(wp);
        }       
        //fecha o menu para evitar erro
        ManagerShop.TocarSomConfimar();               
        Caixa.GetComponent<SpriteRenderer>().sprite = Const.Fantorob.MinhaCaixa;
        Instantiate(Caixa, MainCamera);
        this.gameObject.SetActive(false);
        if (DarNaMissao) { Loja.SetActive(false);
            GameObject.FindWithTag("Player").GetComponent<Walk>().CanIWalk = true; }
        
    }
    public void ApagarBotaoEmTodos()
    {       
        foreach (GameObject g in botoes)
        {
            g.GetComponent<ChangeButton>().DesativarBotao();
        }
    }
}
