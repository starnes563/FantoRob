using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Ataque", menuName = "Fantorob/Ataque")]
[System.Serializable]
public class Attack : ScriptableObject
{
    public string Nome;
    public float Forca;
    public int Elemento;
    // efeito tem haver com o tipo de virus que um ataque pode ter
    // 0 - nenhum
    // 1 - spy
    // 2 - keylogger
    // 3 - trojan
    // 4 - ranson
    // 5 - worm
    // 6 - virus
    public int Efeito;
    public float AumentoEfeito;
    public float GastoEnergia;
    public int Precisao;
    public int UsoDeAcoes;
    public float FatorArmadilha = 0;
    //começar id do 1
    public int Id;
    public bool Elemental = false;
    //por enquanto eu vou deixar assim para não gerar muito problema na costumização, quando eu estiver lidando com ela eu arrumo
    public GameObject Move;
    public Move Move2;
    public GameObject AnimacaoDeAtaque;
    public bool Only = false;
    public bool Diferenciado;
    public bool InstanciaInvertido;
    public List<string> NomesAbertura;   
    public void GerarAtaque(Move move, int id)
    {
        if (move.Aleatório)
        {           
            Nome = move.Nome;
        }
        else
        {          
            Nome = move.NamesLang[ManagerGame.Instance.Idm];
        }       
        Elemento = move.Elemento;       
        Forca = move.Forca;       
        Precisao = move.Precisao;       
        GastoEnergia = move.GastoEnergiaPercentual;        
        UsoDeAcoes = move.UsoDeAcoes;       
        Efeito = move.Efeito;       
        AumentoEfeito = move.AumentoEfeito;       
        Diferenciado = move.Diferenciado;       
        FatorArmadilha = move.FatorArmadilha;        
        Elemental = move.elemental;        
        Id = id;       
        AnimacaoDeAtaque = move.AnimacaoDeAtaque;      
        Only = move.Only;        
        Move2 = move;       
        InstanciaInvertido = move.InstanciaInvertido;        
    }
    public void NomeAbertura()
    {
        Nome = NomesAbertura[ManagerGame.Instance.Idm];        
    }
}
