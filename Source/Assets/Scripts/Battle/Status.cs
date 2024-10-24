using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Status : MonoBehaviour

   // essa classe controla os status do robo
{
    //essa variavel vai servir de endereço para controlar os robos no campo

    // Status do Hexagono do Robo
    public Sprite MeuSprite;
    public Sprite SNucleo;
    public Sprite SArma;
    public string Nome;
    public int Integridade;
    public int Bateria;
    public int Resistencia;
    public int Velocidade;
    public int Ataque;
    public int AtaqueEnergetico;
    public int IntegridadeAtual;
    public int BateriaAtual;
    public Weapon NucleoFisico;
    //controla o numero do turnos vai de 4 a 6
    public int Acoes;
    public int Modelo;
    // Define o Nucleo colocar hide in inspector depois
    //0 - Vermelho
    //1 - Azul
    //2 - Amarelo
    //3 - Verde
    //4 - Laranja
    //5 - Roxo
    public int NucleoElemental;
    public Sprite SpriteNucleo;
    // Define a arma em uso colocar hide in inspector depois
    //0 - Cortante
    //1 - Impacto
    //2 - Escudo
    //3 - BolhaDeEnergia
    //4 - Cajado
    //5 - Canhao
    public int NucleoF;
    [HideInInspector]
    public int armaAtual;
    // define o numero maximo de turnos que um keylogger pode tirar
    public int keyloggerVariant;
    // define o tipo de overlock é sempre um percentual
    public float EnergiaOV;
    public float ResistenciaOV;
    public float VelocidadeOV;
    public float AtaqueOV;
    public float AtaqueEnergeticoOV;

    public float FatordeRecuperação;

    public Color Broke;
    public Color Overlock;
    public Color Infeccao;

    //variaveis de  controle de situaçao atual
    [HideInInspector]
    public bool Spy = false;
    [HideInInspector]
    public bool Keylogger = false;
    [HideInInspector]
    public bool Trojan = false;
    [HideInInspector]
    public float FatorTrojan = 0;
    [HideInInspector]
    public bool Ranson = false;
    [HideInInspector]
    public float FatorRanson = 0;
    [HideInInspector]
    public bool Worm = false;
    [HideInInspector]
    public bool Virus = false;
    [HideInInspector]
    public float AumentoSpy;
    [HideInInspector]
    public int KeyloggerAtual;
    [HideInInspector]
    public float WormPercentual;
    [HideInInspector]
    public float VirusPercentual;
}


