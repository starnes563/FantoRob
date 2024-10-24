using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class DadosFantorob
{
    public int IndividualCode;
    public int Modelo;
    public int NumeroDePartes;
    public DadoParte[] RobotPart = new DadoParte[5];
    public DadoNucleoFisico Fisico;
    public int GastoEnergiaTotal = 50;
    // Status Atual
    public int Integridade;
    public int Bateria;
    public int Resistencia;
    public int Velocidade;
    public int Ataque;
    public int AtaqueElemental;
    public int IntegridadeAtual;
    public int BateriaAtual;
    //variaveis de  controle de situaçao atual   
    public bool Spy = false;  
    public bool Keylogger = false;   
    public bool Trojan = false;   
    public bool Ranson = false;   
    public bool Worm = false;  
    public bool Virus = false;   
    public float AumentoSpy;  
    public int KeyloggerAtual;    
    public float WormPercentual;   
    public float VirusPercentual;
    public DadosFantorob(FantoRob f)
    {
        IndividualCode = f.IndividualCode;
        Modelo = f.Modelo;
        NumeroDePartes = f.NumeroDePartes;
        for(int i=0; i<5; i++)
        {
            if(f.RobotPart[i] !=null)
            {
               // Debug.Log(i);
                RobotPart[i] = new DadoParte(f.RobotPart[i]);
            }             
        }
        Fisico = new DadoNucleoFisico(f.Fisico);
        GastoEnergiaTotal = f.GastoEnergiaTotal;
        Integridade = f.Integridade;
        Bateria = f.Bateria;
        Resistencia = f.Resistencia;
        Velocidade = f.Velocidade;
        Ataque = f.Ataque;
        AtaqueElemental = f.AtaqueElemental;
        IntegridadeAtual = f.IntegridadeAtual;
        BateriaAtual = f.BateriaAtual;
        Spy = f.Spy;
        Keylogger = f.Keylogger;
        Trojan = f.Trojan;
        Ranson = f.Ranson;
        Virus = f.Virus;
        AumentoSpy = f.AumentoSpy;
        KeyloggerAtual = f.KeyloggerAtual;
        WormPercentual = f.WormPercentual;
        VirusPercentual = f.VirusPercentual;
    }
}
