using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreShopMenu : MonoBehaviour
{
    public Image ImagemFantoRob;
    public List<GameObject> ImagemNucleoelemental;
    public Image ImagemOutros;
    public Text Fantodin;
    public Text Credito;
    public Text Quantidade;
    private int quantidade = 1;
    public QuadroRobo QuadorRobo;
    public QuadroNucleoFisico QuadroFisico;
    public QuadroParte QuadroParte;
    private ShopStock Estoque;
    public ShopMenuManager MenuSelecaoFisico;
    public ManagerShop Escolher;
    ShopStock EstoqueAtual;
    public GameObject SelecionarQuantidade;
    public void IniciarCompra(ShopStock estoque)
    {
        this.transform.position = new Vector3(this.transform.position.x, -7.23f);
        LeanTween.moveLocalY(this.gameObject, -0.04f, 0.3f);
        MenuSelecaoFisico.gameObject.SetActive(false);
        Estoque = estoque;
        //Recomeça tudo
        EstoqueAtual = estoque;
        Fantodin.text = "";
        Credito.text = "";
        ImagemOutros.gameObject.SetActive(false);
        ImagemFantoRob.gameObject.SetActive(false);
        foreach(GameObject g in ImagemNucleoelemental)
        {
            g.gameObject.SetActive(false);

        }        
        QuadorRobo.Esconder();
        QuadroFisico.Apagar();
        QuadroParte.Esconder();
        QuadroFisico.gameObject.SetActive(false);

        Quantidade.text = "1"; 
        quantidade = 1;
        Fantodin.text = estoque.Preco.ToString();
        Credito.text = estoque.Credito.ToString();
        ImagemOutros.sprite = estoque.RertonarSprite();
        ImagemOutros.gameObject.SetActive(true);
        SelecionarQuantidade.SetActive(true);
        switch (estoque.Tipo)
        {
            case 0:
                ImagemOutros.gameObject.SetActive(false);
                ImagemFantoRob.gameObject.SetActive(true);
                ImagemFantoRob.sprite = estoque.Fantorob.MenuIconeFantorob;
                ImagemNucleoelemental[estoque.Fantorob.Elemento].SetActive(true);               
                QuadorRobo.Mostrar(estoque.Fantorob);
                QuadorRobo.gameObject.SetActive(true);
                QuadroFisico.gameObject.SetActive(true);               
                break;
            case 1:                
                QuadroFisico.Mostrar(estoque.Weapon);
                QuadroFisico.gameObject.SetActive(true);
                break;
            case 4:
                SelecionarQuantidade.SetActive(false);
                break;
            case 7:
                QuadroParte.Mostrar(estoque.ParteRobo);
                QuadroParte.gameObject.SetActive(true);
                QuadroFisico.gameObject.SetActive(true);
                SelecionarQuantidade.SetActive(false);
                break;
            case 9:
                SelecionarQuantidade.SetActive(false);
                break;
        }        
        this.gameObject.SetActive(true);
       
    }
  
    public void ConcluirCompra()
    {
        if(Estoque.PossoComprar(quantidade))
        {
            ManagerShop.TocarSomComprar();
            if (Estoque.Tipo == 0)
            {
                ComprarFantorob();
            }
            else
            {
                ComprarOutrosItens();
            }
        }
        else
        {
            ManagerShop.TocarSomNaoPode();
        }
        Escolher.AtualizarValores();
    }
    public void ComprarFantorob()
    {
                //menu de seleção de nucleo
        MenuSelecaoFisico.Criar(Estoque);
        MenuSelecaoFisico.gameObject.SetActive(true);
    }
    public void ComprarOutrosItens()
    {
        Estoque.Comprar(quantidade, false);
        switch (Estoque.Tipo)
        {
            case 0:
                
                break;
            case 1:
   
                Escolher.RegerarMenu();
                break;
            case 2:

                break;
            case 3:

                break;
            case 4:
     
                Escolher.RegerarMenu();
                break;
            case 5:

                break;
            case 6:

                break;
            case 7:
                Escolher.RegerarMenu();
                break;
            case 8:

                break;
            case 9:

                Escolher.RegerarMenu();
                break;

        }
    }
    public void AumentarQuantidade()
    {
        quantidade++;
        Quantidade.text = quantidade.ToString();
        int valortotal = quantidade * EstoqueAtual.Preco;
        Fantodin.text = valortotal.ToString();
    }
    public void DiminuirQuantidade()
    {
        if(quantidade>1)
        {
            quantidade--;
            Quantidade.text = quantidade.ToString();
            int valortotal = quantidade * EstoqueAtual.Preco;
            Fantodin.text = valortotal.ToString();
        }
    }
}
