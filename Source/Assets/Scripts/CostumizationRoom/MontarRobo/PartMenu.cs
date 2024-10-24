using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartMenu : MonoBehaviour
{
    [HideInInspector]
    public RobotPart MyPiece;
    public GameObject[] Icones = new GameObject[5];
    public Text Texto;    
    public PartMenuSelecao SelcPart;
    public ChooseRobotMenu MenuRobo;
    public List<Board> Placas = new List<Board>();
    public int ParteAtual;
    public void Montar(RobotPart piece, int minhaParte)
    {
        this.transform.position = new Vector3(this.transform.position.x, 0.4787f);
        LeanTween.moveLocalY(this.gameObject, 0.27f, 0.3f);
        Texto.text = "";
        foreach (GameObject g in Icones)
        {
            g.SetActive(false);
        }
        if(MyPiece != null)
        {
            Placas[MyPiece.Placa].Concluir();
        }
        MyPiece = null;
        SelcPart.gameObject.SetActive(false);
        if (piece != null)
        {
            MyPiece = piece;
            Icones[minhaParte].SetActive(true);
            Texto.text = MyPiece.Nome + "-" + MyPiece.Nivel.ToString();
        }
        ParteAtual = minhaParte;
    }
    public void AbrirTrocar()
    {
        foreach(Board pl in Placas)
        {
            pl.gameObject.SetActive(false);
        }
        SelcPart.gameObject.SetActive(true);
        SelcPart.Criar(ParteAtual, MenuRobo.MeuFantorob.Modelo);
    }
    public void SelecionarParte(RobotPart piece)
    {
        if(MyPiece != null)
        {
            MyPiece.ZerarPlaca();
            PlayerObjects.RobotParts.Add(Instantiate(MyPiece));           
            Destroy(MyPiece);
        }
        MenuRobo.MeuFantorob.ReceberPart(Instantiate(piece), ParteAtual);       
        PlayerObjects.RobotParts.Remove(piece);
        Montar(MenuRobo.MeuFantorob.RobotPart[ParteAtual], ParteAtual);
        MenuRobo.RemontarRobo();
        MenuRobo.TocarSomConfirma();
    }

    public void AbrirPlaca()
    {
        if(MyPiece != null&&MyPiece.Placa<=Placas.Count)
        {
            SelcPart.gameObject.SetActive(false);
            Placas[MyPiece.Placa].Abrir(MyPiece);
            Placas[MyPiece.Placa].gameObject.SetActive(true);
        }        
    }
     public void Concluir()
    {
        if(Placas !=null && Placas.Count>0)
        {
            if(MyPiece != null&&MyPiece.Placa <= Placas.Count)
            {
                Placas[MyPiece.Placa].Concluir();
            }            
        }
        MyPiece = null;
        SelcPart.gameObject.SetActive(false);
        this.gameObject.SetActive(false);

    }
}
