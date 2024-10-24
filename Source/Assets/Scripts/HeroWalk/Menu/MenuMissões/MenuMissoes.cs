using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuMissoes : MonoBehaviour
{
    public QuestBoard Quadro;
    public Button BotaoMissoes;
    public Image Spacer;   
    public PlayerMenu plmen;
    private List<GameObject> bot;
    public AudioSource AudioSource;
    public void OnEnable()
    {
        this.transform.position = new Vector3(this.transform.position.x, -6.15f,30f);
        LeanTween.moveLocalY(this.gameObject, 0, 0.4f);
        if (bot != null && bot.Count>0)
        {
            foreach(GameObject b in bot)
            {
                Destroy(b);
            }
            bot.Clear();
        }
        bot = new List<GameObject>();
        foreach(Quest missao in PlayerObjects.Missões)
        {
            Button botao = Instantiate(BotaoMissoes, Spacer.transform) as Button;
            bot.Add(botao.gameObject);
            botao.GetComponent<BtMissoes>().Criar(missao, Quadro,AudioSource);           
        }
    }
    public void Fechar()
    {
        SonsMenu.Desistir();
        plmen.MostrarQuadro();
        gameObject.SetActive(false);
    }

}
