using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TelaVingaca : MonoBehaviour
{
    public List<MostrarFantorobCelular> FantorobRival;
    public List<MostrarFantorobCelular> FantoroJogador;
    public Image ImagemRival;
    public Text NomeRival;
    public List<GameObject> EstrelasRival;
    public Image ImagemJogador;
    public List<GameObject> EstrelasPlayer;
    NPCBattle MeuNpc;
    public GameObject AvisoSemVinganca;
    public void Criar(int id)
    {
        //npc
        if (ManagerGame.Instance.NPCAtacou.Count > 0)
        {
            NPCBattle npc = ManagerGame.Instance.NPCAtacou[id];
            ImagemRival.sprite = npc.MeuSp;
           // NomeRival.text = npc.Nome[ManagerGame.Instance.Idm];
            foreach (MostrarFantorobCelular rob in FantorobRival)
            {
                rob.gameObject.SetActive(false);
            }
            for (int i = 0; i < npc.Robots.Count; i++)
            {
                FantorobRival[i].gameObject.SetActive(true);
                FantorobRival[i].Mostrar(npc.Robots[i]);
            }
            foreach (GameObject estrela in EstrelasRival)
            {
                estrela.SetActive(false);
            }
            for (int i = 0; i < npc.Estrelas; i++)
            {
                EstrelasRival[i].SetActive(true);
            }
            //player
            ImagemJogador.sprite = PlayerStatus.MeuSprite;
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
            MeuNpc = npc;
        }
        else
        {
            this.gameObject.SetActive(false);
            AvisoSemVinganca.SetActive(true);
        }

    }
    public void Atacar()
    {
        if (MeuNpc != null)
        {
            MeuNpc.Attack();
        }
    }
    
}
