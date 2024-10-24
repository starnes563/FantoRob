using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{    
    //Level
    public static int Level =1;
    public static int Exp =0;
    public static int nextLevel =10;
    public static int nextstar = 10;
    //Reputation
    public static  int Reputation = 1;
    public static int Trending = 0;
    public static int nextReputation = 70;
    //dinheiro
     public static float Creditos;
    //estrelas
    public static int Estrelas;
    public static int ControleDeCena;
    public static bool CartaEndosso = false;
    public static Sprite MeuSprite;
    public static string Nome = "";
    //dias desafio
    public static int DaysLeft = 7;
    //desafio
    public static int Posicao = 0;
    public static int Pontos = 0;

    //Position
    [HideInInspector]
    public static Vector3 NextHeroPosition;
    [HideInInspector]
    public static Vector3 LastHeroPosition;
    [HideInInspector]
    public static Vector3 ProximaPosicaoMarcus;
    public static GameObject MarcusV;
    public static bool MarcusAtivo = false;
    public static Vector3 ProximaPosicaoLuiza;
    public static bool LuizaAtiva = false;
    public static GameObject Luiza;
    [HideInInspector]
    public static string ProximaAnimacao;
    public static int PersonagemAtual = 0;

    public static void ReceiveExp(int exp)
    {
        Exp += exp;
        if(Exp> nextLevel)
        {
            passarDeLevel();
        }

    }
    static void passarDeLevel()
    {
        Exp = 0;
        Level++;
        nextLevel += (int)(1.025807266f * nextLevel);
        if(Level>=nextstar)
        {
            darestrela();
        }
    }
    static void darestrela()
    {
        if(Estrelas<8)
        {
            Estrelas++;
            nextstar += 10;
        }
    }
    public static void ReceiveReputation(int trend)
    {
        Trending += trend;        
    }   
   
    public void ReceberCarta()
    {
        CartaEndosso = true;
    }
}
