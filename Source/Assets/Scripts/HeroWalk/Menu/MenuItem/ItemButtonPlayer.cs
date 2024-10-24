using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemButtonPlayer : MonoBehaviour
{
    [HideInInspector]
    public Item MyItem;
    [HideInInspector]
    public ItensMenu Menu;    
    [HideInInspector]
    public PlayerMenu PlayerMenu;
    [HideInInspector]
    public PartyMenu PartyMenu;
    // Start is called before the first frame update
    void Start()
    {
        PartyMenu = PlayerMenu.PartyMenu.GetComponent<PartyMenu>();
    }    
    public void Clicou()
    {
       
        if (Menu.MyRobot == null)
        {
            SonsMenu.Confimar();
            Menu.MyItem = MyItem;
            Menu.botao = this.gameObject;
            PlayerMenu.AbrirMenuParty();
            PartyMenu.ItemUsar = MyItem;
        }
        else
        {
            SonsMenu.Recuperar();
            Menu.MyItem = MyItem;
            Menu.botao = this.gameObject;
            Menu.UsarItem();
        }
    }
}
