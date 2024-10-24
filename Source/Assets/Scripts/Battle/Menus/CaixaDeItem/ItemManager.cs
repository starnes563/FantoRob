using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public GameObject Gerenciador;
    private ObjectsDatabase objects;
    public Button BotaodeItem;
    public Image SpacerDeItem;
    List<GameObject> bt = new List<GameObject>();
    // Start is called before the first frame update
    public void GerarMenuItem()
    {
        if(bt.Count>0)
        {
            foreach(GameObject b in bt)
            {
                Destroy(b);
            }
            bt.Clear();
        }
        objects = Gerenciador.GetComponent<ObjectsDatabase>();
        if (objects.Itens.Count>0)
        {
            foreach (GameObject item in objects.Itens)
            {
                if (item != null && item.GetComponent<Item>().Quantidade > 0)
                {

                    Button botao = Instantiate(BotaodeItem) as Button;
                    botao.transform.SetParent(SpacerDeItem.transform, false);
                    botao.GetComponent<ItemButon>().MyItem = item.GetComponent<Item>();
                    botao.transform.GetChild(0).GetComponent<Image>().sprite = item.GetComponent<Item>().Sprite;
                    botao.transform.GetChild(1).GetComponent<Text>().text = item.GetComponent<Item>().Nome[ManagerGame.Instance.Idm];
                    botao.transform.GetChild(2).GetComponent<Text>().text = (string)item.GetComponent<Item>().Quantidade.ToString();
                    bt.Add(botao.gameObject);
                    for (int i = 0; i < item.GetComponent<Item>().GastoAcoes; i++)
                    {
                        botao.transform.GetChild(3).transform.GetChild(i).gameObject.SetActive(true);
                    }

                }
            }
        }
        
    }
}
