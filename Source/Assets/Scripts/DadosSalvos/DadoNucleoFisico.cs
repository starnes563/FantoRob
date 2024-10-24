using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class DadoNucleoFisico
{
    public DadoMove[] Ataque = new DadoMove[6];
    public List<DadoCombo> Combo = new List<DadoCombo>();
    public int NumeroDeAtaques; 
    public List<DadoMove> MovesAtivos = new List<DadoMove>();
    public List<DadoMove> MovesDePente = new List<DadoMove>();
    public int Array;

    public DadoNucleoFisico(Weapon nf)
    {
        Ataque = new DadoMove[6];
        for(int i =0; i<nf.Ataque.Length;i++)
        {
            if(nf.Ataque[i] != null)
            {
                Ataque[i] = new DadoMove(nf.Ataque[i].Move2);
            }
        }
        foreach (Combo c in nf.Combo)
        {
            Combo.Add(new DadoCombo(c));
        }
        NumeroDeAtaques = nf.NumeroDeAtaques;
        foreach (Move m in nf.MovesAtivos)
        {
            MovesAtivos.Add(new DadoMove(m));
        }
        foreach (Move m in nf.MovesDePentes)
        {
            MovesDePente.Add(new DadoMove(m));
        }
        Array = nf.Array;
    }
}
