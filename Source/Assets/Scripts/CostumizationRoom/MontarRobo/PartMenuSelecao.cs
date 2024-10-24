using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartMenuSelecao : MonoBehaviour
{
    public Image Spacer;
    public Button BotaoParte;
    public PartMenu MenuParte;
    private List<GameObject> BotoesAtivos = new List<GameObject>();
    public void Criar(int id, int modelo)
    {
        this.transform.position = new Vector3(0.736f, this.transform.position.y);
        LeanTween.moveLocalX(this.gameObject, 0.473f, 0.4f);
        //verifica se o menu ja esta feito e o destroi
        if (BotoesAtivos.Count>0)
        {
            foreach (GameObject bt in BotoesAtivos)
            {
                Destroy(bt);
            }
            BotoesAtivos.Clear();
        }
        //faz o menu      
        foreach (RobotPart parte in PlayerObjects.RobotParts)
        {           
            if(parte.Id == id)
            {               
                    Button botao = Instantiate(BotaoParte) as Button;
                    botao.transform.SetParent(Spacer.transform, false);
                    PartSelectionButton bt = botao.GetComponent<PartSelectionButton>();
                    bt.MyPiece = parte;
                    bt.PartMenu = MenuParte;
                    bt.SelecaoParte = this;
                    bt.Icones[id].SetActive(true);
                    bt.Texto.text = parte.Nome + "-" + parte.Nivel.ToString();
                    BotoesAtivos.Add(botao.gameObject);                               
            }           
        }
    } 
    bool podeaparecer(int nivel, int modelo)
    {
        bool pode = false;
        if(nivel == 1 && modelo <=25)
        {
            pode = true;
        }
        if(nivel == 2 && modelo >25)
        {
            pode = true;
        }
        return pode;
    }
}
