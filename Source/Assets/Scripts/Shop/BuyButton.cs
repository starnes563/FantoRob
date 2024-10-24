using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuyButton : MonoBehaviour
{
    private ShopStock MyThing;   
    private PreShopMenu preShop;
    public Image SpriteFantorob;
    public Image SpriteOutros;
    public Text Nome;
    // Start is called before the first frame update
    public void Criar(ShopStock stock, PreShopMenu pre)
    {
        MyThing = stock;
        SpriteOutros.gameObject.SetActive(true);
        SpriteOutros.sprite = MyThing.RertonarSprite();
        Nome.text = MyThing.RetornarNome();
        preShop = pre;
        if(MyThing.Tipo == 0)
        {
           SpriteOutros.gameObject.SetActive(false);
           SpriteFantorob.gameObject.SetActive(true);
           SpriteFantorob.sprite = MyThing.RertonarSprite();
        }
    }
    public void Clicar()
    {
        preShop.IniciarCompra(MyThing);
        ManagerShop.TocarSomConfimar();
    }
}
