using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuElevadorEntre : MonoBehaviour
{
    public Vector3[] Posicao = new Vector3[4];
    public int[] Cenas = new int[4];
   
    public void Apertar(int MeuAndar)
    {
        PlayerStatus.NextHeroPosition = Posicao[MeuAndar];
        ManagerGame.Instance.SceneToLoad = Cenas[MeuAndar];
        PlayerStatus.ProximaAnimacao = "IdleFrente";
        GameObject.FindWithTag("MainCamera").GetComponent<Diretor>().TrocarACena();       
    }

}
