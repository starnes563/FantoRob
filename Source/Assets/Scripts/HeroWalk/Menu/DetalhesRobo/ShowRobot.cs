using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowRobot : MonoBehaviour
{
    [HideInInspector]
    public GameObject Bateria;
    [HideInInspector]
    public string Nome;
    [HideInInspector]
    public GameObject Arma;
    [HideInInspector]
    public PlayerMenu Menu;
    int IDAtual;

    //Quadro robo
    public QuadroRobo QuadroRobo;
    //ArmaEAtaques
    public Text PercentualNucleo;
    public List<GameObject> AtaquesBT = new List<GameObject>(6);
    //objetos    
    public List<ComboNote> ListaCombo = new List<ComboNote>();    
    public PlayerMenu PMenu;    
    // Update is called once per frame   
    public void CriarMenu(FantoRob Robot, PlayerMenu menu)
    {   Menu = menu;       
        //Cria O Quadro
        QuadroRobo.Mostrar(Robot);
        //Arma
        MontarMenuArma(Robot.Fisico);
    }
    public void OnEnable()
    {
        this.transform.position = new Vector3(this.transform.position.x, -7.23f);
        LeanTween.moveLocalY(this.gameObject, -0.27f, 0.4f);
    }
    public void MontarMenuArma(Weapon arma)
    {
        PercentualNucleo.text = arma.Forca.ToString();        
        for(int i = 0; i<arma.AttacksMax; i++)
        {
           if(arma.Ataque[i] != null)
            {
                AtaquesBT[i].SetActive(true);
                AtaquesBT[i].GetComponent<BtVerAtaque>().MeuAtaque = arma.Ataque[i];
                AtaquesBT[i].transform.GetChild(0).GetComponent<Text>().text = arma.Ataque[i].Nome;
            }                                 
        }
        //gera os demonstrativos de combo;
        
          foreach (ComboNote c in ListaCombo)
          {
          c.gameObject.SetActive(false);
          }
          for (int i = 0; i<arma.Combo.Count;i++)
        {
            ListaCombo[i].gameObject.SetActive(true);
            ListaCombo[i].Gerar(arma.Combo[i]);
        }
        
     
        
    }
    public void Fechar()
    {
        SonsMenu.Desistir();
        gameObject.SetActive(false);
        QuadroRobo.Esconder();
        foreach(GameObject bt in AtaquesBT)
        {
            bt.SetActive(false);
        }
        PMenu.gameObject.SetActive(true);
        PMenu.AbrirMenuParty();
    }


}
