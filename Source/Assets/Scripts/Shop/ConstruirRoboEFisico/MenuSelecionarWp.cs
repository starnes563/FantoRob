using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSelecionarWp : MonoBehaviour
{
    Construivel Const;
    public Image Spacer;
   public  Button BotaoFisico;
    List<Button> botoes = new List<Button>();
    public PainelNFATK QuadroFisico;
    public QuadroConstruir QuadroConstruir;   
    public GameObject Caixa;
    public Transform MainCamera;
    public void Criar(Construivel ct)
    {        
        this.transform.position = new Vector3(this.transform.position.x, -5.23f);
        LeanTween.moveLocalY(this.gameObject, -2.61f, 0.3f);
        Const = ct;
        QuadroConstruir.Mostrar(ct);
        if (botoes.Count>0)
        {
            foreach (Button bt in botoes)
            {
                Destroy(bt.gameObject);                
            }
            botoes.Clear();
            botoes = new List<Button>();
        }        
        foreach (Weapon wp in PlayerObjects.NucleosFisicos)
        {
            Button bt = Instantiate(BotaoFisico, Spacer.transform) as Button;
            botoes.Add(bt);
            bt.GetComponent<BotaoSelecionarWp>().Criar(wp, this, QuadroFisico);
        }        
    }
    public void Concluir(Weapon wp)
    {
        QuadroConstruir.ReceberWeapon(wp);
        this.gameObject.SetActive(false);       
    }
}
