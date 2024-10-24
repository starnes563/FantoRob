using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Circuito", menuName = "Fantorob/Circuitos")]
[System.Serializable]
public class Circuit : ScriptableObject
{
    //0 - attack
    //1 - spattack
    //2 - resistance
    //3 - speed
    //4 - integrity
    //5 - buffer;
    [HideInInspector]
    public int Id;
    [HideInInspector]
    public int Level;
    [HideInInspector]
    public string Nome;
   [HideInInspector]
    public int value;
    [HideInInspector]
    public int Arrayindex;
    [HideInInspector]
    public Sprite MeuSprite;
    public Sprite[] Sprites = new Sprite[6];
    public static Sprite sp;
    public static int vl;
    public void IniciarCircuito(int array)
    {
        int tp = 0;
        int nv = 0;
        Arrayindex = array;
        switch (array)
        {
            case 0:
                tp = 0;
                nv = 1;
                break;
            case 1:
                tp = 0;
                nv = 2;
                break;
            case 2:
                tp = 1;
                nv = 1;
                break;
            case 3:
                tp = 1;
                nv = 2;
                break;
            case 4:
                tp = 2;
                nv = 1;
                break;
            case 5:
                tp = 2;
                nv = 2;
                break;
            case 6:
                tp = 2;
                nv = 3;
                break;
            case 7:
                tp = 3;
                nv = 1;
                break;
            case 8:
                tp = 3;
                nv = 2;
                break;
            case 9:
                tp = 4;
                nv = 1;
                break;
            case 10:
                tp = 4;
                nv = 2;
                break;
            case 11:
                tp = 4;
                nv = 3;
                break;
            case 12:
                tp = 4;
                nv = 4;
                break;
            case 13:
                tp = 5;
                nv = 1;
                break;
            case 14:
                tp = 5;
                nv = 2;
                break;
        }
        MeuSprite = Sprites[tp];
        criarcircuito(tp, nv);
       
    }
void criarcircuito(int tipo, int nivel)
    {
        switch (tipo)
            {
            case 0:
                at(nivel);
                break;
            case 1:
                ae(nivel);
                break;
            case 2:
                re(nivel);
                break;
            case 3:
                ve(nivel);
                break;
            case 4:
                it(nivel);
                break;
            case 5:
                bf(nivel);
                break;
        }
    }
    void at(int nivel)
    {
        Id = 0;
        Level = nivel;
        Nome = "AK";
      switch( nivel)
        {
            case 1:
                value = 1;
                break;
            case 2:
                value = 2;
                break;
        }
    }
    void ae(int nivel)
    {
        Id = 1;
        Level = nivel;
        Nome = "AE";
        switch (nivel)
        {
            case 1:
                value = 1;
                break;
            case 2:
                value = 2;
                break;
        }
    }
    void re(int nivel)
    {
        Id = 2;
        Level = nivel;
        Nome = "RE";
        switch (nivel)
        {
            case 1:
                value = 1;
                break;
            case 2:
                value = 3;
                break;
            case 3:
                value = 4;
                break;
        }
    }
    void ve(int nivel)
    {
        Id = 3;
        Level = nivel;
        Nome = "VE";
        switch (nivel)
        {
            case 1:
                value = 2;
                break;
            case 2:
                value = 3;
                break;
        }
    }
    void it(int nivel)
    {
        Id = 4;
        Level = nivel;
        Nome = "IT";
        switch (nivel)
        {
            case 1:
                value = 4;
                break;
            case 2:
                value = 6;
                break;
            case 3:
                value = 11;
                break;
            case 4:
                value = 14;
                break;
        }
    }
    void bf(int nivel)
    {
        Id = 5;
        Level = nivel;
        Nome = "BF";
        switch (nivel)
        {
            case 1:
                value = 5;
                break;
            case 2:
                value = 7;
                break;
        }
    }
// metodos para acelerar a criação de botoes
public Sprite RetornarSprite(int array)
    {
        Sprite sp;
        int tp=0;
        switch (array)
        {
            case 0:
                tp = 0;                
                break;
            case 1:
                tp = 0;                
                break;
            case 2:
                tp = 1;                
                break;
            case 3:
                tp = 1;                
                break;
            case 4:
                tp = 2;                
                break;
            case 5:
                tp = 2;                
                break;
            case 6:
                tp = 2;               
                break;
            case 7:
                tp = 3;                
                break;
            case 8:
                tp = 3;                
                break;
            case 9:
                tp = 4;               
                break;
            case 10:
                tp = 4;               
                break;
            case 11:
                tp = 4;                
                break;
            case 12:
                tp = 4;               
                break;
            case 13:
                tp = 5;                
                break;
            case 14:
                tp = 5;                
                break;
        }
        sp = Constructor.RetornarSprite(5, 0, tp, 0,0);
                return sp;
    }
 public string RetornarValue(int array)
    {
        int value = 0;
        switch (array)
        {
            case 0:
                value = 1;
                break;
            case 1:
                value = 2;
                break;
            case 2:
                value = 1;
                break;
            case 3:
                value = 2;
                break;
            case 4:
                value = 1;
                break;
            case 5:
                value = 3;
                break;
            case 6:
                value = 4;
                break;
            case 7:
                value = 2;
                break;
            case 8:
                value = 3;
                break;
            case 9:
                value = 4;
                break;
            case 10:
                value = 6;
                break;
            case 11:
                value = 11;
                break;
            case 12:
                value = 14;
                break;
            case 13:
                value = 5;
                break;
            case 14:
                value = 7;
                break;
        }
        string valor = value.ToString();
                return valor;
    }
    public int RetornarValueint(int array)
    {
        int value = 0;
        switch (array)
        {
            case 0:
                value = 1;
                break;
            case 1:
                value = 2;
                break;
            case 2:
                value = 1;
                break;
            case 3:
                value = 2;
                break;
            case 4:
                value = 1;
                break;
            case 5:
                value = 3;
                break;
            case 6:
                value = 4;
                break;
            case 7:
                value = 2;
                break;
            case 8:
                value = 3;
                break;
            case 9:
                value = 4;
                break;
            case 10:
                value = 6;
                break;
            case 11:
                value = 11;
                break;
            case 12:
                value = 14;
                break;
            case 13:
                value = 5;
                break;
            case 14:
                value = 7;
                break;
        }
        
        return value;
    }
    public void CarregarDadoSalvo(DadoCircuito dados)
    {
        IniciarCircuito(dados.arrayindex);
    }
    public int RetornarId(int array)
    {
        int tp = 0;
        switch (array)
        {
            case 0:
                tp = 0;
                break;
            case 1:
                tp = 0;
                break;
            case 2:
                tp = 1;
                break;
            case 3:
                tp = 1;
                break;
            case 4:
                tp = 2;
                break;
            case 5:
                tp = 2;
                break;
            case 6:
                tp = 2;
                break;
            case 7:
                tp = 3;
                break;
            case 8:
                tp = 3;
                break;
            case 9:
                tp = 4;
                break;
            case 10:
                tp = 4;
                break;
            case 11:
                tp = 4;
                break;
            case 12:
                tp = 4;
                break;
            case 13:
                tp = 5;
                break;
            case 14:
                tp = 5;
                break;
        }

        return tp;
    }
}



