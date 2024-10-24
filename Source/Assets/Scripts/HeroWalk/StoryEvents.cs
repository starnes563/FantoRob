using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryEvents : MonoBehaviour
{
    public static bool[] PrimeiraMissao  = new bool[5];
    //variaveis para controle de desafios
    //0-escritorios
    //1-fazendadagua
    //2-barco
    //3-castelo
    //4-minas
    //5-mansao
    //6-centroentreterimento
    public static Desafio[] DesafiosCamp = new Desafio[7];
    public static bool[] MapaDesafio = new bool[7];
    public static bool UltimoandarLiberado = false;
    public static bool NivelAguaBaixo = false;
    public static bool BondeLibeardo = false;
    public static bool ChavePortaoFazenda = false;
    public static bool ProibidoEntradaAlter = false;
    public static int PortasBarco = 0;
    public static bool Esfihas = false;
    public static bool FalhouEsfihas = false;
    public static bool ChaveDespensa = false;
    public static bool BauDespensa = false;
    public static bool[] CavaeleiroCast = new bool[4];
    public static bool Torredoisaberta = false;
    public static bool ElevadorCast = false;
    public static bool[] BoolCaverna = new bool[2];
    public static bool[] Tesourado = new bool[2];
    public static bool UltimaTecla = false;
    public static bool[] TeclasOrgao = new bool[8];
    public static int NivelCartao;
    public static bool ChaveTorre = false;
    public static int RecordeFlip = 20;
    public static int[] PontuacoesParticipantes = new int[27];
    public static int[] PosicaoParticipantes = new int[27];
    public static int DesafioAtual = 0;
    public static bool[] TrapacaDesafio = new bool[7];
    public static bool[] MarcadoresDesafio = new bool[2];
    //funcoes para alterar as booleanas
    //tutorial
    public static bool TutorialMarcus;
    public static bool TutorialPrimeiraMissao;
    public static int contTutPrimMiss;
    public static float UltTempoMissao;
    public static bool RegeraEstoque = true;
    public static bool[] ExpasoesInventario = new bool[2]; 
    public static void DarPrimeiraMissao()
    {
        for(int i = 0; i<5;i++)
        {
            PrimeiraMissao[i] = true;
        }
    }
    public static void Carregar(DadosStory D)
    {
        for (int i = 0; i < 5; i++)
        {
            PrimeiraMissao[i] = D.PrimeiraMissao[i];
        }
        for (int i = 0; i < 7; i++)
        {
            MapaDesafio[i] = D.MapaDesafio[i];
        }
        UltimoandarLiberado = D.UltimoandarLiberado;
        BondeLibeardo = D.BondeLiberado;
        ChavePortaoFazenda = D.ChavePortaoFazenda;
        ProibidoEntradaAlter = D.ProibidoEntradaAlter;
        FalhouEsfihas = D.FalhouEsfihas;
        Esfihas = D.Esfihas;
        ChaveDespensa = D.ChaveDespensa;
        BauDespensa = D.BauDespensa;
        for (int i = 0; i < 4; i++)
        {
            CavaeleiroCast[i] = D.CavaeleiroCast[i];
        }
        for (int i = 0; i < 2; i++)
        {
            BoolCaverna[i] = D.PortaoCaverna[i];
        }
        for (int i = 0; i < 2; i++)
        {
            Tesourado[i] = D.Tesourado[i];
        }
        UltimaTecla = D.UltimaTecla;
        for (int i = 0; i < 8; i++)
        {
            TeclasOrgao[i] = D.TeclasOrgao[i];
        }
        NivelCartao = D.NivelCartao;
        ChaveTorre = D.ChaveTorre;
        GatilhoDesafio();
        for (int i = 0; i < DesafiosCamp.Length; i++)
        {
            DesafiosCamp[i].CarregarDesafio(D.dadosDesafios[i]);
        }
        RecordeFlip = D.RecordeFlip;
        for (int i = 0; i < 27; i++)
        {
            PontuacoesParticipantes[i] = D.PontuacoesParticipantes[i];
            PosicaoParticipantes[i] = D.PosicaoParticipantes[i];
        }
        DesafioAtual = D.DesafioAtual;
        for (int i = 0; i < 7; i++)
        {
            TrapacaDesafio[i] = D.TrapacaDesafio[i];
        }
        for (int i = 0; i < 2; i++)
        {
            MarcadoresDesafio[i] = D.MarcadoresDesafio[i];
        }
        TutorialMarcus = D.TutorialMarcus;
        TutorialPrimeiraMissao = D.TutorialPrimeiraMissao;
        contTutPrimMiss = D.contTutPrimMiss;
        UltTempoMissao = D.UltTempoMissao;
        RegeraEstoque = D.RegeraEstoque;
        for (int i = 0; i < 2; i++)
        {
            ExpasoesInventario[i] = D.ExpasoesInventario[i];
        }
    }
    public static void GatilhoDesafio()
    {       
        DesafiosCamp = new Desafio[7];
        for (int i = 0; i < DesafiosCamp.Length; i++)
        {
            DesafiosCamp[i] = new Desafio(); ;
        }
       // PontuacoesParticipantes = new List<int>(27);
        //PosicaoParticipantes = new List<int>(27);
    }
}
[System.Serializable]
public class Desafio
{
    public int Chavepequena;
    public bool Chavegrande;
    public bool Itemdesafio;
    public bool[] Interagiveis = new bool[99];
    public Desafio()
    {
        Chavepequena = 0;
        Chavegrande = false;
        Itemdesafio = false;
        for (int i = 0; i < Interagiveis.Length; i++)
        {
            Interagiveis[i] = false;
        }
    }

    public void CarregarDesafio(DadosDesafio d)
    {
        Chavepequena = d.Chavepequena;
        Chavegrande = d.Chavegrande;
        Itemdesafio = d.Itemdesafio;
        for (int i = 0; i < Interagiveis.Length; i++)
        {
            Interagiveis[i] = d.Interagiveis[i];
        }
    }
}