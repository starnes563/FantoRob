using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuadroConstruir : MonoBehaviour
{
    public Image ImagemFantoRob;
    public Image ImagemNucleoelemental;
    public Image ImagemFisico;

    public List<Image> ImageItensNecessarios = new List<Image>(5);
    public List<Text> QuantidadesNecessarias = new List<Text>(5);
    public List<Text> QuantidadesPossuidas = new List<Text>(5);
    public List<Image> Fundos = new List<Image>();
    public List<Text> TextosDosNomes = new List<Text>(5);    
    public List<Sprite> Figuras = new List<Sprite>();
    public Text Preco;
    public Text DinheiroAtual;
    public QuadroRobo Quadro;
    public PainelNFATK QuadroFisico;
    public GameObject QuadroPropriedades;
    private Construivel Construivel;
    public MenuSelecionarWp MenuSelecionarFisico;
    public QuadroConstruiuNF QuadroConstruiuNF;
    [HideInInspector]
    public Weapon WeaponSelecionada;
    public GameObject Caixa;
    public Transform MainCamera;
    public Text NomeNF;
    public Button BotaoNF;
    public Sprite SpriteBotaoNfBase;
    public List<string> TextoBotaoNFBase;
    public void Mostrar(Construivel ct)
    {        
        this.transform.position = new Vector3(this.transform.position.x, -7.23f);
        LeanTween.moveLocalY(this.gameObject, -0.04f, 0.3f);
        Apagar();
        Construivel = ct;
        QuadroPropriedades.transform.position = new Vector3(11f, QuadroPropriedades.transform.position.y);
        LeanTween.moveLocalX(QuadroPropriedades.gameObject, 6.42f, 0.4f);
        QuadroPropriedades.SetActive(true);
        //mostrar fantorob ou nucleo fisico
        DinheiroAtual.text = PlayerObjects.Fantodin.ToString();
        NomeNF.text = TextoBotaoNFBase[ManagerGame.Instance.Idm];
        BotaoNF.GetComponent<Image>().sprite = SpriteBotaoNfBase;
        switch (ct.Tipo)
        {
            case 0:
                ImagemFantoRob.gameObject.SetActive(true);
                ImagemFantoRob.sprite = ct.Fantorob.MenuIconeFantorob;
                ImagemNucleoelemental.gameObject.SetActive(true);
                ImagemNucleoelemental.sprite = ct.Fantorob.SpriteElemento;
                Quadro.Mostrar(ct.Fantorob);
                Quadro.gameObject.SetActive(true);
                NomeNF.gameObject.SetActive(true);
                NomeNF.text = TextoBotaoNFBase[ManagerGame.Instance.Idm];
                BotaoNF.gameObject.SetActive(true);
                BotaoNF.GetComponent<Image>().sprite = SpriteBotaoNfBase;
                break;
            case 1:
                ImagemFisico.gameObject.SetActive(true);
                ImagemFisico.sprite = ct.Weapon.MySprite;
                QuadroFisico.Mostrar(ct.Weapon);
                QuadroFisico.gameObject.SetActive(true);
                NomeNF.gameObject.SetActive(false);
                BotaoNF.gameObject.SetActive(false);
                break;
        }
       
        //mostra os icones dos itens
        for (int i = 0; i<ct.ObjetosNecessarios.Count;i++)
        {
            
            TextosDosNomes[i].text = Constructor.RetornarNome(6,0,0,0,ct.ObjetosNecessarios[i],0);           
            Fundos[i].gameObject.SetActive(true);           
            //ativaobjeto
            ImageItensNecessarios[i].gameObject.SetActive(true);
            //pega sprite   
            
            ImageItensNecessarios[i].sprite = Constructor.RetornarSprite(6,0,0, ct.ObjetosNecessarios[i],0);
            //pegaquantidade           
            QuantidadesNecessarias[i].text = ct.QuantidadesNecessarias[i].ToString();            
            QuantidadesPossuidas[i].text = PlayerObjects.ItensConstruir[ct.ObjetosNecessarios[i]].ToString();            
            //poepreço          
            Preco.text = ct.Preco.ToString();
           

        }

        this.gameObject.SetActive(true);
        
    }
   public void Apagar()
    {
        DinheiroAtual.text = "";
        Quadro.gameObject.SetActive(false);
        QuadroFisico.Apagar();
        ImagemFantoRob.gameObject.SetActive(false);
        ImagemNucleoelemental.gameObject.SetActive(false);
        ImagemFisico.gameObject.SetActive(false);
        for (int i = 0; i < 5; i++)
        {
            Fundos[i].gameObject.SetActive(false);
            //ativaobjeto
            ImageItensNecessarios[i].gameObject.SetActive(false);
            TextosDosNomes[i].text = "";
            //pegaquantidade
            QuantidadesNecessarias[i].text = "";
            QuantidadesPossuidas[i].text = "";
            //poepreço
            Preco.text = "";
        }
    }
    public void AtualizarDinheiro()
    {
        DinheiroAtual.text = PlayerObjects.Fantodin.ToString();
    }
    public void Confirmar()
    {
        if (Construivel.PossoConstruir())
        {
            Leticia.TocarSomConfimar();
            switch (Construivel.Tipo)
            {

                case 0:
                    //menu de seleção de nucleo
                    if(WeaponSelecionada != null)
                    {
                        //poe weapon no fantorob
                        Construivel.Fantorob.Fisico = Instantiate(WeaponSelecionada);
                        Construivel.Fantorob.SpriteFisico = WeaponSelecionada.MySprite;
                        //ativaFantorob
                        Construivel.Construir();
                        //retirar o nucleo do inventario
                        QuadroFisico.gameObject.SetActive(false);
                        this.gameObject.SetActive(false);
                        PlayerObjects.NucleosFisicos.Remove(WeaponSelecionada);
                        WeaponSelecionada = null;
                        //fecha o menu para evitar erro
                        Leticia.TocarSomConfimar();
                        this.Apagar();
                        Caixa.GetComponent<SpriteRenderer>().sprite = Construivel.Fantorob.MinhaCaixa;
                        Instantiate(Caixa, MainCamera);
                        this.gameObject.SetActive(false);
                        this.AtualizarDinheiro();
                    }
                    else
                    {
                        Leticia.TocarSomNaoPode();
                    }
                    
                    break;
                case 1:
                    Construivel.Construir();
                    QuadroConstruiuNF.Mostrar(Construivel.Weapon);
                    break;
            }
        }
        else
        {
            Leticia.TocarSomNaoPode();
        }
    }
    public void ApagarQuadroFisico()
    {
        if(Construivel.Tipo == 1)
        {
            this.gameObject.SetActive(false);
            QuadroPropriedades.SetActive(false);
        }
    }
    public void AbrirMenuNF()
    {        
        MenuSelecionarFisico.Criar(Construivel);
        MenuSelecionarFisico.gameObject.SetActive(true);
        Leticia.TocarSomConfimar();
    }
    public void ReceberWeapon(Weapon wp)
    {
        WeaponSelecionada = wp;
        NomeNF.text = wp.Nome[ManagerGame.Instance.Idm];
        BotaoNF.GetComponent<Image>().sprite = WeaponSelecionada.MySprite;        
    }
}
