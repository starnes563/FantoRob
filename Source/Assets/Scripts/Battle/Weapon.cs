using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "NucleoFisico", menuName = "Fantorob/NucleoFisico")]
[System.Serializable]
public class Weapon : ScriptableObject
{
    public Attack[] Ataque = new Attack[6];
    public List<Combo> Combo = new List<Combo>();
    public float Forca;   
    private int instanciadoCombo = 0;    
    public List<string> Nome;    
    public int WeaponID;    
    // Define a arma em uso colocar hide in inspector depois
    //0 - Cortante
    //1 - Impacto
    //2 - Escudo
    //3 - Instrumento
    //4 - Cajado
    //5 - Canhao
    public int Model;    
    public int PowerPer;
    public Sprite MySprite;
    
    public int AttacksMin;   
    public int AttacksMax;
    public int NumeroDeAtaques;
    public int CombosMin;    
    public int CombosMax;

    public GameObject AnimacaoEspecial;

    public List<Move> MovimentosAmbos = new List<Move>();
    public List<Move> MovimentosJogador = new List<Move>();
    public List<Move> MovimentosInimigo = new List<Move>();
    //esses sao os moves que estao transformados em ataque
    public List<Move> MovesAtivos = new List<Move>();
    [HideInInspector]
    public List<Move> MovesDePentes = new List<Move>();
    public int Array;
    public void IniciarArma(bool rival)
    {
        MovesAtivos.Clear();
        Combo.Clear();
        //adiciona os movimentos aleatoriamente entre o ataque minimo e o maximo      
        for(int i =0; i<Random.Range(AttacksMin, AttacksMax+1);i++)
        {
            if (MovimentosAmbos.Count>i)
            {
                MovesAtivos.Add(MovimentosAmbos[i]);
            }
            else
            {
                break;
            }
            
        }        
        //completa com movimentos do jogador se nao tiver atingido o maximo
        if (AttacksMax - MovesAtivos.Count > 0)
        {
            if (!rival)
            {               
                for (int i = 0; i < AttacksMax - MovesAtivos.Count; i++)
                {
                    if (MovimentosJogador.Count > i)
                    {
                        MovesAtivos.Add(MovimentosJogador[i]);
                    }
                    else
                    {
                        break;
                    }
                }
               
            }
            else
            {
                for (int i = 0; i < AttacksMax - MovesAtivos.Count; i++)
                {
                    if (MovimentosInimigo.Count > i)
                    {
                        MovesAtivos.Add(MovimentosInimigo[i]);
                    }
                    else
                    {
                        break;
                    }
                }               
            }
        }        
        CarregarAtaques();
    }
    public void MontarArma(int numeroDeAtaques, int numerodeCombos)
    {
        NumeroDeAtaques = numeroDeAtaques;
        if(numerodeCombos>0)
        {
            Combo.Clear();
            Combo = new List<Combo>();
            instanciadoCombo = 0;
           for (int i = 0; i<numerodeCombos; i++)
            {
                Combo combos = new Combo();
                gerarCombos(combos, AttacksMax);
                Combo.Add(combos);
            }
        }         
    }   
    private void gerarCombos(Combo combo, int numeroDeAtaques)
    {
        int i = 0;
        switch (instanciadoCombo)
        {
            case 0:               
                while (i < 3)
                {
                    combo.GerarCombo(Random.Range(1, numeroDeAtaques+1), i);
                    i++;
                }
                break;
            case 1:
                {                    
                    while (i < 4)
                    {
                        combo.GerarCombo(Random.Range(1, numeroDeAtaques + 1), i);
                        i++;
                    }
                }
                break;
            case 2:               
                while (i < 5)
                {
                    combo.GerarCombo(Random.Range(1, numeroDeAtaques + 1), i);
                    i++;
                }
                break;
        }
        if (instanciadoCombo >2)
        {              
            while (i < Random.Range(4, 7))
            {
                combo.GerarCombo(Random.Range(1, numeroDeAtaques + 1), i);
                i++;
            }
        }
        instanciadoCombo++;
    }
    
    public void ReceberAtaque(Move move, int id)
    {
       Attack novoatack = ScriptableObject.CreateInstance<Attack>();       
       novoatack.GerarAtaque(move, id);
       Ataque[id] = novoatack;
        move.emUso = true;
        //if (Ataque.Length < 0) { Ataque = new Attack[6]; }
    }
    public void RestartWeapon()
    {
        for(int i = 0; i<6; i++)
        {
            Ataque[i] = null;
        }
        Combo.Clear();
        NumeroDeAtaques = 0;
       
        instanciadoCombo = 0;
    }
    public void ReceberMove(Move move)
    {
        MovesDePentes.Add(move);
    }
    public int MoveIDPente(Move move)
    {
        int id =0 ;
        if (MovesDePentes.Count > 0)
        {
            id = MovesDePentes.IndexOf(move);    
        }
        return id;
    }
    public int MoveIDAtaque(Move move)
    {
        int id = 0;
        for (int i = 0; i < Ataque.Length; i++)
        {
            if (Ataque[i] != null)
            {
                if (Ataque[i].Move2 == move)
                {
                    id = i;
                }
            }
        }
        return id;
    }
    public void RemoverMovePente(int idpente)
    {       
        int idAtivos = 0;
        int idAtaque = 0;
        bool achou = false;
        if(MovesDePentes.Count>0)
        {
            for (int i = 0; i < MovesDePentes.Count; i++)
            {
                if (MovesDePentes[i].meuIdNoPente == idpente)
                {                   
                    idAtivos = MovesDePentes[i].meuIdnoAtivos;                   
                    idAtaque = MovesDePentes[i].meuIdnoAtaque;                   
                    achou = MovesDePentes[i].emUso;
                    MovesDePentes.Remove(MovesDePentes[i]);                    
                    break;
                }
            }
        }
        if (achou)
        {
            if (MovesAtivos.Count > 0)
            {
                for (int i = 0; i < MovesAtivos.Count; i++)
                {
                    if (MovesAtivos[i].meuIdnoAtivos == idAtivos)
                    {
                        MovesAtivos.Remove(MovesAtivos[i]);
                    }
                }
            }

            for (int i = 0; i < Ataque.Length; i++)
            {
                if (Ataque[i]!=null)
                {
                    if(Ataque[i].Move2.meuIdnoAtaque == idAtaque)
                    {
                        Ataque[i] = null;
                    }
                   
                }
            }
        }
    }
    public void RemoverMoveAtivo(int id)
    {
        if(Ataque[id]!=null)
        {
            if (Ataque[id].Move2.emUso)
            {
                Ataque[id].Move2.emUso = false;
                if (MovesAtivos.Count >= 0)
                {
                    for (int i = 0; i < MovesAtivos.Count; i++)
                    {
                        if (MovesAtivos[i].meuIdnoAtivos == Ataque[id].Move2.meuIdnoAtivos)
                        {
                            MovesAtivos.Remove(MovesAtivos[i]);
                        }
                    }
                }
                if (Ataque[id] != null)
                {
                    Ataque[id] = null;
                }               
            }
        }       
    }
    public void CarregarAtaques()
    {
       Ataque = new Attack[AttacksMax];
       foreach (Move mv in MovesAtivos)
        {
            if (mv!= null)
            {
                ReceberAtaque(mv, MovesAtivos.IndexOf(mv));                
            }            
        }
        NumeroDeAtaques = Ataque.Length;
    }
    public void CarregarSavlo(DadoNucleoFisico nf)
    {      
        MovesAtivos.Clear();
        Ataque = new Attack[nf.Ataque.Length];
        for (int i = 0; i < nf.Ataque.Length; i++)
        {
            if (nf.Ataque[i] != null)
            {
                if(nf.Ataque[i].Aleatório)
                {                    
                    Move m = ScriptableObject.CreateInstance<Move>();
                    m.CarregarSalvo(nf.Ataque[i]);
                    ReceberAtaque(m, i);
                }
                else
                {
                    ReceberAtaque(Constructor.Instance.MovesEstaticos[nf.Ataque[i].Array], i);
                }
               
            }
        }
        Combo.Clear();
        if (nf.Combo != null && nf.Combo.Count>0)
        {
            foreach (DadoCombo c in nf.Combo)
            {
                Combo a = new Combo();
                a.CarregarSalvo(c);
                Combo.Add(a);
            }
        }        
        NumeroDeAtaques = nf.NumeroDeAtaques;
        foreach (DadoMove m in nf.MovesAtivos)
        {
            if(m.Aleatório)
            {
                Move c = ScriptableObject.CreateInstance<Move>();
                c.CarregarSalvo(m);
                MovesAtivos.Add(c);
            }
            else
            {
                MovesAtivos.Add(Constructor.Instance.MovesEstaticos[m.Array]);
            }
            
        }
        if (nf.MovesDePente != null && nf.MovesDePente.Count > 0)
        {
            foreach (DadoMove m in nf.MovesDePente)
            {
                if (m.Aleatório)
                {
                    Move c = ScriptableObject.CreateInstance<Move>();
                    c.CarregarSalvo(m);
                    ReceberMove(c);
                }
                else
                {
                    ReceberMove(Constructor.Instance.MovesEstaticos[m.Array]);
                }

            }
        }
        Array = nf.Array;        
    }
}
