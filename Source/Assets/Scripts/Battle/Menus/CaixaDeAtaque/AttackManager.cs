using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;

public class AttackManager : MonoBehaviour
{
    public List<Button> BotoesDeAtaque = new List<Button>(6);
    public List<Text> Texto = new List<Text>();
    public List<GameObject> BarraAcoesGastar = new List<GameObject>(6);
    [HideInInspector]
    public BattleManager BattleManager;
    [HideInInspector]
    public bool ativo = false;
    public List<Color> CoresFundo = new List<Color>(7);
    public void GerarMenu(Weapon Weapon, WeaponMethods wp, BattleManager battleManager)
    {
        foreach(Button ba in BotoesDeAtaque)
        {
            ba.gameObject.SetActive(false);
        }
        BattleManager = battleManager;
        int i = 0;        
        foreach (Attack ataque in Weapon.Ataque)
        {
            if(ataque!=null)
            {
                CriarBotao(i, ataque.Nome, ataque.Forca, ataque.Precisao, ataque.GastoEnergia, ataque.UsoDeAcoes,
           ataque.Elemental, ataque.Elemento, wp, Weapon.Model);
                i++;
            }           
        }        
    }
    void Start()
    {
        Apagar();
    }   
    void CriarBotao(int id, string nome, float forca, int precisao, float energia, int acoes, bool elemental, int elm, WeaponMethods wp,int model)
    {
        BotaoDeAtaque bt = BotoesDeAtaque[id].GetComponent<BotaoDeAtaque>();
        bt.Id = id;
        bt.Forca = forca;
        bt.Prescisao = precisao;
        bt.Gasto = energia+wp.regitrarAumentoEnergia;
        bt.Elemental = elemental;
        bt.Elem = elm;
        bt.Acoes = acoes;
        bt.weaponMethods = wp;
        bt.manager = this;
        string NomeAtaque = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(nome.ToLower());
        foreach(Text t in bt.NomeAtaque)
        {
            t.text = NomeAtaque;
        }
        //cor do botao
        if (elemental)
        {
            bt.Fundo.color = CoresFundo[elm];
        }
        else
        {
            bt.Fundo.color = CoresFundo[6];
        }
        //relação
        int rel;
        if (elemental)
        {
            rel = BattleManager.RetornaRelacaoElement(elm);
        }
        else
        {
            rel = BattleManager.RetornaRelacaoFisico(model);
        }
        foreach (Text t in bt.Relacao)
        {
            t.text = BattleManager.screemManager.TextoRelacaos[rel].MinhaRelacao[ManagerGame.Instance.Idm];
        }
        BotoesDeAtaque[id].gameObject.SetActive(true);
    }
    public void Mostrar(float forca, int precisao, float energia, int acoes, bool elemental, int elemento)
    {        
        Texto[0].text = forca.ToString();
        Texto[1].text = precisao.ToString();
        Texto[2].text = energia.ToString();
        if (elemental) 
        {            
            switch(elemento)
            {
                case 0:
                    switch (ManagerGame.Instance.Idm)
                    {
                        case 0:
                            Texto[3].text = "VRM";
                            break;
                        case 1:
                            Texto[3].text = "RED";
                            break;
                    }                   
                    break;
                case 1:
                    switch (ManagerGame.Instance.Idm)
                    {
                        case 0:
                            Texto[3].text = "AZL";
                            break;
                        case 1:
                            Texto[3].text = "BLU";
                            break;
                    }
                    break;
                case 2:
                    switch (ManagerGame.Instance.Idm)
                    {
                        case 0:
                            Texto[3].text = "AML";
                            break;
                        case 1:
                            Texto[3].text = "YLW";
                            break;
                    }
                    break;
                case 3:
                    switch (ManagerGame.Instance.Idm)
                    {
                        case 0:
                            Texto[3].text = "VED";
                            break;
                        case 1:
                            Texto[3].text = "GRN";
                            break;
                    };
                    break;
                case 4:
                    switch (ManagerGame.Instance.Idm)
                    {
                        case 0:
                            Texto[3].text = "LRJ";
                            break;
                        case 1:
                            Texto[3].text = "ORJ";
                            break;
                    }
                    break;
                case 5:
                    switch (ManagerGame.Instance.Idm)
                    {
                        case 0:
                            Texto[3].text = "ROX";
                            break;
                        case 1:
                            Texto[3].text = "PPL";
                            break;
                    }
                    break;
            }
        }
        else { 
            switch(ManagerGame.Instance.Idm)
            {
                case 0:
                    Texto[3].text = "NÃO";
                    break;
                case 1:
                    Texto[3].text = "NO";
                    break;
            }             
        }
        for(int i = 0; i<acoes; i++)
        {
            BarraAcoesGastar[i].SetActive(true);
        }
    }
    public void Apagar()
    {
        Texto[0].text = "";
        Texto[1].text = "";
        Texto[2].text = "";
        Texto[3].text = "";
        for (int i = 0; i < 6; i++)
        {
            BarraAcoesGastar[i].SetActive(false);
        }
    }
}
