using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySelectButton : MonoBehaviour
{
    [HideInInspector]
    public ItemInventario MyItem;
    [HideInInspector]
    public StuffBillboard Menu;
    public Image Sprite;
    public Text Nome;
    public Text Quantidade;      
    public void Clicar()
    {
        Menu.Esconder();
        Menu.Mostrar(MyItem);
    }
    public void Criar(ItemInventario item, StuffBillboard stuff)
    {
        MyItem = item;
        Sprite.sprite = item.MeuSprite;
        Nome.text = item.Nome;
        Menu = stuff;
        Quantidade.text = item.Quantidade.ToString();
    }
}

public class ItemInventario
{
    public Sprite MeuSprite;
    public string Nome;
    public Pente PenteVazio;
    public Pente PenteCheio;
    public int Circuito;
    public int Silicio;
    public RobotPart Part;
    public int Bateria;
    public Weapon NFisico;
    public int ItemConstruir;
    public int Quantidade;
    public enum TipoDeInventario
    {
        PENTEVAZIO,
        PENTECHEIO,
        CIRCUITO,
        SILICIO,
        PARTE,
        BATERIA,
        NFISICO,
        ITEMCONSTRUIR,
    }
    public TipoDeInventario MeuTipo;

    public ItemInventario(TipoDeInventario tipo,Pente vazio, Pente Cheio, int c, int silicio, RobotPart part, int Bat, 
        Weapon Nfisico, int itemconst)
    {
        MeuTipo = tipo;
        switch (tipo)
        {
            case TipoDeInventario.PENTEVAZIO:
                MeuSprite = Constructor.RetornarSprite(4, 0, 0, 0,0);
                Nome = Constructor.RetornarNome(1, 0, 0, 0, 0, 0);
                PenteVazio = vazio;
                Quantidade = 1;
                break;
            case TipoDeInventario.PENTECHEIO:
                MeuSprite = Constructor.RetornarSprite(1, 0, 0, 0,0);
                Nome = Constructor.RetornarNome(1, 0, 0, 0, 0, 0);
                PenteCheio = Cheio;
                Quantidade = 1;
                break;
            case TipoDeInventario.CIRCUITO:
                MeuSprite = Constructor.RetornarSprite(5, 0, c, 0,0);
                Nome = Constructor.RetornarNome(5, 0, 0, c, 0, 0);
                Circuito = c;
                Quantidade = PlayerObjects.Circuits[c];
                break;
            case TipoDeInventario.SILICIO:
                MeuSprite = Constructor.RetornarSprite(0, 0, 0, 0,0);
                Nome = Constructor.RetornarNome(0, 0, 0, 0, 0, 0);
                Silicio = silicio;
                Quantidade = PlayerObjects.Silicon;
                break;
            case TipoDeInventario.PARTE:
                MeuSprite = Constructor.RetornarSprite(7, 0, 0, 0,part.Id);
                Nome = Constructor.RetornarNome(7, 0, 0, 0, 0, part.Id) ;
                Part = part;
                Quantidade = 1;
                break;
            case TipoDeInventario.BATERIA:
                MeuSprite = Constructor.RetornarSprite(3, 0, 0, 0,0);
                Nome = Constructor.RetornarNome(3, 0, Bat, 0, 0, 0);
                Bateria = Bat;
                Quantidade = PlayerObjects.PlayerObjectsStatic.Batteries[Bat];
                break;
            case TipoDeInventario.NFISICO:
                MeuSprite = Nfisico.MySprite;
                Nome = Nfisico.Nome[ManagerGame.Instance.Idm];
                NFisico = Nfisico;
                Quantidade = 1;
                break;
            case TipoDeInventario.ITEMCONSTRUIR:
                MeuSprite = Constructor.RetornarSprite(6,0,0,itemconst,0);
                Nome = Constructor.RetornarNome(6,0,0,0,itemconst,0);
                ItemConstruir = itemconst;
                Quantidade = PlayerObjects.ItensConstruir[itemconst];
                break;
        }
    }
}
