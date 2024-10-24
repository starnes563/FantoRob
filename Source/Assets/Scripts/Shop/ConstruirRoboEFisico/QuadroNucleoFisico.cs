using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadroNucleoFisico : MonoBehaviour
{
    public List<QuadroAtaque> Quadro = new List<QuadroAtaque>();
    public void Mostrar(Weapon wp)
    {
        int i = 0;
        foreach(Move mv in wp.MovimentosAmbos)
        {
            Attack at = ScriptableObject.CreateInstance<Attack>();
            at.GerarAtaque(mv, i);
            Quadro[i].Mostrar(at);
            Quadro[i].gameObject.SetActive(true);
        }
        foreach (Move mv in wp.MovimentosJogador)
        {
            Attack at = ScriptableObject.CreateInstance<Attack>();
            at.GerarAtaque(mv, i);
            Quadro[i].Mostrar(at);
            Quadro[i].gameObject.SetActive(true);
        }
    }
    public void Apagar()
    {
        foreach(QuadroAtaque qd in Quadro)
        {
            qd.gameObject.SetActive(false);
        }
    }
}
