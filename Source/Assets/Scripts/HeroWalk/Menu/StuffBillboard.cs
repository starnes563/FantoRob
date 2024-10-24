using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StuffBillboard : MonoBehaviour
{
    [HideInInspector]
    public GameObject Thing;
    public Image Sprite;
    public Text Nome;
    public Text Quantidade;
    public Text Level;
    public Text Forca;
    public Text Energy;
    public Button BotaoMove;
    public Image Spacer;
    public Text[] Valorepentecheio = new Text[6];
    public Text[] NomesPentecheio = new Text[6];
    List<GameObject> BotoesMove = new List<GameObject>();
    public GameObject GrupoStatus;
    public Text[] Status = new Text[6];
    public Text StatusParte;
    public void Mostrar(ItemInventario thing)
    {
        Sprite.sprite = thing.MeuSprite;
        Sprite.gameObject.SetActive(true);
        Nome.text = thing.Nome;
        Quantidade.text = thing.Quantidade.ToString();
        switch(thing.MeuTipo)
        {
            case ItemInventario.TipoDeInventario.PENTEVAZIO:
                Level.text = thing.PenteVazio.Level.ToString();               
                Energy.text = thing.PenteVazio.Gasto1.ToString() + "/" + thing.PenteVazio.Gasto2.ToString();
                CriarBotao(thing.PenteVazio.Move);
                break;
            case ItemInventario.TipoDeInventario.PENTECHEIO:
                Level.text = thing.PenteCheio.Level.ToString();
                Energy.text = thing.PenteCheio.GastoAtual.ToString();
                Forca.text = thing.PenteCheio.Valor.ToString();
                AjustarStatus(thing.PenteCheio, null, true);
                CriarBotao(thing.PenteCheio.Move);
                break;
            case ItemInventario.TipoDeInventario.CIRCUITO:               
                Forca.text = ScriptableObject.CreateInstance<Circuit>().RetornarValue(thing.Circuito);
                break;
            case ItemInventario.TipoDeInventario.SILICIO:
                break;
            case ItemInventario.TipoDeInventario.BATERIA:
                Forca.text = thing.Nome;
                break;
            case ItemInventario.TipoDeInventario.PARTE:
                AjustarStatus(null,thing.Part, false);
                Level.text = thing.Part.Nivel.ToString();
                Forca.text = thing.Part.Value.ToString();
                Energy.text = thing.Part.Energyspent.ToString();
                break;
            case ItemInventario.TipoDeInventario.NFISICO:
                foreach(Move mv in thing.NFisico.MovimentosAmbos)
                {
                    CriarBotao(mv);
                }
                foreach (Move mv in thing.NFisico.MovimentosJogador)
                {
                    CriarBotao(mv);
                }
                break;
            case ItemInventario.TipoDeInventario.ITEMCONSTRUIR:
                break;
        }
    }
    void AjustarStatus(Pente pente, RobotPart pt, bool pen)
    {        
        if (pen)
        {
            GrupoStatus.SetActive(true);
            for (int i = 0; i<6;i++)
            {
                Status[i].text = pente.Valor[i].ToString();
            }
        }
        else
        {
            switch(pt.Compilador)
            {
                case 0:
                    switch(ManagerGame.Instance.Idm)
                    {
                        case 0:
                            StatusParte.text = "Atq";
                            break;
                        case 1:
                            StatusParte.text = "Atk";
                            break;
                    }                   
                    break;
                case 1:
                    switch (ManagerGame.Instance.Idm)
                    {
                        case 0:
                            StatusParte.text = "AtqES";
                            break;
                        case 1:
                            StatusParte.text = "AtqSP";
                            break;
                    }                    
                    break;
                case 2:
                    StatusParte.text = "Res";
                    break;
                case 3:
                    StatusParte.text = "Vl";
                    break;
                case 4:
                    StatusParte.text = "It";
                    break;
            }
        }
    }   
    public void CriarBotao(Move move)
    {
        Button botao = Instantiate(BotaoMove, Spacer.transform) as Button;
        BotoesMove.Add(botao.gameObject);
        alterarTexto(botao, 0, move.Nome);
        alterarTexto(botao, 1, move.Forca.ToString());
        alterarTexto(botao, 2, move.Precisao.ToString());
        alterarTexto(botao, 3, move.GastoEnergiaPercentual.ToString());
        if (move.elemental)
        {
            alterarTexto(botao, 4, "Elemental");
        }
        else
        {
            alterarTexto(botao, 4, "Normal");
        }
        switch (move.Efeito)
        {
            case 0:
                switch (ManagerGame.Instance.Idm)
                {
                    case 0:
                        alterarTexto(botao, 5, "Sem"); 
                        break;
                    case 1:
                        alterarTexto(botao, 5, "None");
                        break;
                }                
                break;
            case 1:
                alterarTexto(botao, 5, "Spy");
                break;
            case 2:
                alterarTexto(botao, 5, "Keylogger");
                break;
            case 3:
                alterarTexto(botao, 5, "Trojan");
                break;
            case 4:
                alterarTexto(botao, 5, "Ranson");
                break;
            case 5:
                alterarTexto(botao, 5, "Worm");
                break;
            case 6:
                alterarTexto(botao, 5, "Virus");
                break;
        }
        alterarTexto(botao, 6, move.AumentoEfeito.ToString());        
        
            //alterarTexto(botao, 7, "");
        
    }
    private void alterarTexto(Button botao, int child, string movetext)
    {
        botao.transform.GetChild(child).GetComponent<Text>().text = movetext;
    }
    public void Esconder()
    {
        Sprite.gameObject.SetActive(false);
        Nome.text = "";
        Quantidade.text = "";
        Level.text = "";
        Forca.text = "";
        Energy.text = "";
        StatusParte.text = "";
        GrupoStatus.SetActive(false);
        if (BotoesMove.Count>0)
        {
            foreach(GameObject g in BotoesMove)
            {
                Destroy(g);
            }
            BotoesMove.Clear();
        }
    }
}
