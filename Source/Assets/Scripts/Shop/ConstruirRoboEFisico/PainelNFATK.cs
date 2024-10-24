using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PainelNFATK : MonoBehaviour
{
    public GameObject Painel;
    public GameObject QuadroAtaque;
    public Image Spacer;
    List<GameObject> GObj = new List<GameObject>();
    public void Mostrar(Weapon wp)
    {       
        if(GObj !=null && GObj.Count>0)
        {
            foreach(GameObject g in GObj)
            {
                Destroy(g);
            }
            GObj.Clear();
        }
        for(int i=0; i<wp.MovimentosAmbos.Count;i++)
        {
            Attack at = ScriptableObject.CreateInstance<Attack>();
            at.GerarAtaque(wp.MovimentosAmbos[i], i);
            GameObject qd = Instantiate(QuadroAtaque, Spacer.transform) as GameObject;
            GObj.Add(qd);
            qd.GetComponent<QuadroAtaque>().Mostrar(at);
        }
        for (int i = 0; i < wp.MovimentosJogador.Count; i++)
        {
            Attack at = ScriptableObject.CreateInstance<Attack>();
            at.GerarAtaque(wp.MovimentosJogador[i], i);
            GameObject qd = Instantiate(QuadroAtaque, Spacer.transform) as GameObject;
            GObj.Add(qd);
            qd.GetComponent<QuadroAtaque>().Mostrar(at);
        }
        Painel.SetActive(true);
    }
    public void Apagar()
    {
        Painel.SetActive(false);
    }

}
