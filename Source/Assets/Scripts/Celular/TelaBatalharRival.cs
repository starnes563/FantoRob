using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TelaBatalharRival : MonoBehaviour
{
    public Batalha GeradorRival;
    public List<MostrarFantorobCelular> FantorobRival;
    public List<MostrarFantorobCelular> FantoroJogador;
    public Image ImagemRival;
    public Text NomeRival;
    public List<GameObject> EstrelasRival;
    public Image ImagemJogador;
    public List<GameObject> EstrelasPlayer;
    public NPCBattle MeuNpc;
    public void Criar()
    {
        //npc
        GeradorRival.GerarBatalha(MeuNpc);
       // ImagemRival.sprite = MeuNpc.MeuSp;
        //NomeRival.text = MeuNpc.Nome[ManagerGame.Instance.Idm];
        foreach(MostrarFantorobCelular rob in FantorobRival)
        {
            rob.gameObject.SetActive(false);
        }
        for(int i = 0; i < MeuNpc.Robots.Count;i++)
        {
            FantorobRival[i].gameObject.SetActive(true);
            FantorobRival[i].Mostrar(MeuNpc.Robots[i]);
        }
        foreach(GameObject estrela in EstrelasRival)
        {
            estrela.SetActive(false);
        }
        for(int i =0; i< MeuNpc.Estrelas;i++)
        {
            EstrelasRival[i].SetActive(true);
        }
        //player
       // ImagemJogador.sprite = PlayerStatus.MeuSprite;
        foreach (MostrarFantorobCelular rob in FantoroJogador)
        {
            rob.gameObject.SetActive(false);
        }
        for (int i = 0; i < PlayerObjects.RobotsInUse.Count; i++)
        {
            FantoroJogador[i].gameObject.SetActive(true);
            FantoroJogador[i].Mostrar(PlayerObjects.RobotsInUse[i]);
        }
        foreach (GameObject estrela in EstrelasPlayer)
        {
            estrela.SetActive(false);
        }
        for (int i = 0; i < PlayerStatus.Estrelas; i++)
        {
            EstrelasPlayer[i].SetActive(true);
        }
     }
    public void Atacar()
    {
        if(MeuNpc != null)
        {
            ManagerGame.Instance.StartBattle(MeuNpc.Tipo, MeuNpc);
        }       
    }
}
