using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class DadoMove
{
    public string Nome;
    public List<string> NamesLang = new List<string>();
    public float Forca;
    public int Precisao;
    public float GastoEnergiaPercentual;
    public int UsoDeAcoes;   
    public int Efeito;
    public float AumentoEfeito = 0;
    public bool Diferenciado;
    public float FatorArmadilha = 0;
    public bool elemental = false;
    public bool escolhido = false;
    public int Id;   
    public bool Only;
    public List<string> Descrição = new List<string>(); //0-português 
    public bool Aleatório;
    public int Array;   
    public int Animacao;
    public int meuIdNoPente;
    public int meuIdnoAtaque;
    public int meuIdnoAtivos;
    public bool emUso;
    public DadoMove(Move mv)
    {
        Aleatório = mv.Aleatório;
       if(mv.Aleatório)
        {
            Nome = mv.Nome;
            if(mv.NamesLang.Count>0)
            {
                foreach (string n in mv.NamesLang)
                {                   
                    NamesLang.Add(n);
                }
            }          
            Forca = mv.Forca;
            Precisao = mv.Precisao;
            GastoEnergiaPercentual = mv.GastoEnergiaPercentual;
            UsoDeAcoes = mv.UsoDeAcoes;
            Efeito = mv.Efeito;
            AumentoEfeito = mv.AumentoEfeito;
            Diferenciado = mv.Diferenciado;
            FatorArmadilha = mv.FatorArmadilha;
            elemental = mv.elemental;
            escolhido = mv.escolhido;
            Id = mv.Id;
            Only = mv.Only;
            foreach (string des in mv.Descrição)
            {
                Descrição.Add(des);
            }
            Animacao = mv.Animacao;
        }
        else
        {
            Array = mv.Array;
        }
        meuIdNoPente = mv.meuIdNoPente;
        meuIdnoAtaque = mv.meuIdnoAtaque;
        meuIdnoAtivos = mv.meuIdnoAtivos;
        emUso = mv.emUso;
    }
}
