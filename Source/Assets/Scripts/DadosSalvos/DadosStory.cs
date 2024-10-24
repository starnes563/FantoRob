using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class DadosStory
{
    public bool[] PrimeiraMissao = new bool[5];
    public bool[] MapaDesafio = new bool[7];
    public bool UltimoandarLiberado;
    public bool BondeLiberado;
    public bool ChavePortaoFazenda;
    public bool ProibidoEntradaAlter;
    public bool Esfihas = false;
    public bool FalhouEsfihas = false;
    public bool ChaveDespensa = false;
    public bool BauDespensa = false;
    public bool[] CavaeleiroCast = new bool[4];
    public bool[] PortaoCaverna = new bool[2];
    public bool[] Tesourado = new bool[2];
    public bool UltimaTecla = false;
    public bool[] TeclasOrgao = new bool[8];
    public int NivelCartao;
    public bool ChaveTorre = false;
    public DadosDesafio[] dadosDesafios = new DadosDesafio[7];
    public int RecordeFlip = 20;
    public int[] PontuacoesParticipantes = new int[27];
    public int[] PosicaoParticipantes = new int[27];
    public int DesafioAtual = 0;
    public bool[] TrapacaDesafio = new bool[7];
    public bool[] MarcadoresDesafio = new bool[2];
    public bool TutorialMarcus;
    public bool TutorialPrimeiraMissao;
    public int contTutPrimMiss;
    public float UltTempoMissao;
    public bool RegeraEstoque = true;
    public bool[] ExpasoesInventario = new bool[2];
    public DadosStory()
    {
        for (int i = 0; i<5;i++)
        {
            PrimeiraMissao[i] = StoryEvents.PrimeiraMissao[i];
        }
        for (int i = 0; i < 7; i++)
        {
            MapaDesafio[i] = StoryEvents.MapaDesafio[i];
        }
        UltimoandarLiberado = StoryEvents.UltimoandarLiberado;
        BondeLiberado = StoryEvents.BondeLibeardo;
        ChavePortaoFazenda = StoryEvents.ChavePortaoFazenda;
        ProibidoEntradaAlter = StoryEvents.ProibidoEntradaAlter;
        FalhouEsfihas = StoryEvents.FalhouEsfihas;
        Esfihas = StoryEvents.Esfihas;
        ChaveDespensa = StoryEvents.ChaveDespensa;
        BauDespensa = StoryEvents.BauDespensa;
        for (int i = 0; i < 4; i++)
        {
            CavaeleiroCast[i] = StoryEvents.CavaeleiroCast[i];
        }
        for (int i = 0; i < 2; i++)
        {
            PortaoCaverna[i] = StoryEvents.BoolCaverna[i];
        }
        for (int i = 0; i < 2; i++)
        {
            Tesourado[i] = StoryEvents.Tesourado[i];
        }
        UltimaTecla = StoryEvents.UltimaTecla;
        for (int i = 0; i < 8; i++)
        {
            TeclasOrgao[i] = StoryEvents.TeclasOrgao[i];
        }
        NivelCartao = StoryEvents.NivelCartao;
        ChaveTorre = StoryEvents.ChaveTorre;
        for (int i = 0; i < dadosDesafios.Length; i++)
        {
            dadosDesafios[i] = new DadosDesafio(StoryEvents.DesafiosCamp[i]);
        }
        RecordeFlip = StoryEvents.RecordeFlip;
        for(int i = 0; i<27;i++)
        {
            PontuacoesParticipantes[i] = StoryEvents.PontuacoesParticipantes[i];
            PosicaoParticipantes[i] = StoryEvents.PosicaoParticipantes[i];
        }
        DesafioAtual = StoryEvents.DesafioAtual;
        for (int i = 0; i < 7; i++)
        {
            TrapacaDesafio[i] = StoryEvents.TrapacaDesafio[i];
        }
        for (int i = 0; i < 2; i++)
        {
            MarcadoresDesafio[i] = StoryEvents.MarcadoresDesafio[i];
        }
        TutorialMarcus = StoryEvents.TutorialMarcus;
        TutorialPrimeiraMissao = StoryEvents.TutorialPrimeiraMissao;
        contTutPrimMiss = StoryEvents.contTutPrimMiss;
        UltTempoMissao = StoryEvents.UltTempoMissao;
        RegeraEstoque = StoryEvents.RegeraEstoque;
        for (int i = 0; i < 2; i++)
        {
            ExpasoesInventario[i] = StoryEvents.ExpasoesInventario[i];
        }
    }
}
[System.Serializable]
public class DadosDesafio
{
    public int Chavepequena;
    public bool Chavegrande;
    public bool Itemdesafio;
    public bool[] Interagiveis = new bool[99];

    public DadosDesafio(Desafio d)
    {
        Chavepequena = d.Chavepequena;
        Chavegrande = d.Chavegrande;
        Itemdesafio = d.Itemdesafio;
        for(int i = 0;i<Interagiveis.Length;i++)
        {
            Interagiveis[i] = d.Interagiveis[i];
        }       
    }
}
