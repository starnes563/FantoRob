using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItensMenu : MonoBehaviour
{
    private PlayerObjects playerObjects;
    public Button BotaoDeItem;
    public Image Spacer;
    [HideInInspector]
    public FantoRob MyRobot;
    [HideInInspector]
    public PlayerMenu PMenu;
    [HideInInspector]
    public Item MyItem;
    [HideInInspector]
    public GameObject botao;
    List<GameObject> bot = new List<GameObject>();
    private void OnEnable()
    {
        MyItem = null;
    }
    public void Criar(PlayerMenu player)
    {
       this.transform.position = new Vector3(this.transform.position.x, -7.23f,30f);
        LeanTween.moveLocalY(this.gameObject, 6.99f, 0.3f);
        PMenu = player;
        playerObjects = PlayerObjects.PlayerObjectsStatic;
        if (bot.Count > 0)
        {
            foreach (GameObject g in bot)
            {
                Destroy(g);
            }
        }
        bot.Clear();
        if(PlayerObjects.PlayerObjectsStatic.Itens.Count>0)
        {
            foreach (GameObject item in PlayerObjects.PlayerObjectsStatic.Itens)
            {
                if(item.GetComponent<Item>().Quantidade>0)
                {
                    Button botao = Instantiate(BotaoDeItem, Spacer.transform);
                    bot.Add(botao.gameObject);
                    botao.GetComponent<ItemButtonPlayer>().MyItem = item.GetComponent<Item>();
                    botao.GetComponent<ItemButtonPlayer>().PlayerMenu = player;
                    botao.GetComponent<ItemButtonPlayer>().Menu = this;
                    botao.transform.GetChild(0).GetComponent<Image>().sprite = item.GetComponent<Item>().Sprite;
                    botao.transform.GetChild(1).GetComponent<Text>().text = item.GetComponent<Item>().Nome[ManagerGame.Instance.Idm];
                    botao.transform.GetChild(2).GetComponent<Text>().text = (string)item.GetComponent<Item>().Quantidade.ToString();
                    botao.transform.GetChild(3).GetComponent<Text>().text = item.GetComponent<Item>().Descricao[ManagerGame.Instance.Idm];
                }                
            }
        }        
    }
    public void ReceberRobo(FantoRob robo)
    {
        MyRobot = robo;
    }
    public void UsarItem()
    {
        FantoRob robo = MyRobot;
        switch (MyItem.Tipo)
        {
            case 0:
                robo.RetiraSpy();
                break;
            case 1:
                robo.RetiraKeylogger();
                break;
            case 2:
                robo.RetiraTrojan();
                break;
            case 3:
                robo.RetiraRanson();
                break;
            case 4:
                robo.RetiraWorm();
                break;
            case 5:
                robo.RetiraVirus();
                break;
            case 6:
                robo.RecuperaIntegridade(MyItem.Fator);
                break;
            case 7:
                robo.RecuperaBateria(MyItem.Fator);
                break;
            case 8:
                robo.RecuperaIntegridade(MyItem.Fator/5);
                robo.RecuperaBateria(MyItem.Fator);
                break;
            case 9:
                robo.RetiraSpy();
                robo.RetiraKeylogger();
                robo.RetiraTrojan();
                robo.RetiraRanson();
                robo.RetiraWorm();
                robo.RetiraVirus();
                break;
        }
        // atualiza quantidade de itens.
        MyItem.GastarItem(1);
        Criar(PMenu);
    }
    public void Fechar()
    {        
        PMenu.MostrarQuadro();
        gameObject.SetActive(false);
        MyRobot = null;
        MyItem = null;
        botao = null;
        PMenu.Active = true;
    }
}
