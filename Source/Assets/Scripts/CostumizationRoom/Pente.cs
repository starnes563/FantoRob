using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "Pente", menuName = "Fantorob/Pente")]
[System.Serializable]
public class Pente : ScriptableObject
{ 
 //0 - attack
//1 - spattack
//2 - resistance
//3 - speed
//4 - integrity
//5 - buffer;
//9- sem
[HideInInspector]
public int Id;   
    public int Level;
    //0 - attack
    //1 - spattack
    //2 - resistance
    //3 - speed
    //4 - integrity
    //5 - buffer;
    //9- sem
    [HideInInspector]
    public int[] Valor = new int [6];
       [HideInInspector]
    public int GastoAtual;
    [HideInInspector]
public int Gasto1;
    [HideInInspector]
    public int Gasto2;
[HideInInspector]
public Circuit Slot1;
[HideInInspector]
public Circuit Slot2;
    [HideInInspector]
    public Circuit Slot3;
    [HideInInspector]
    public Circuit Slot4;
[HideInInspector]
public bool Forged = false;
private Merger Merge;
public Move Move;
    public Sprite SpriteAtual;
    public Sprite ForgedImage;
    public Sprite EmptyImage;
    public Type MeuTipo;
    [HideInInspector]
    public int ArrayIndex;
    public string Nome;
    [HideInInspector]
    public Button MyButton;
    // Start is called before the first frame update    
    public void CriarPente(int nivel, Type tipo, Move mv)
    {
        MeuTipo = tipo;
        Level = nivel;
        Move = mv;
        switch (nivel)
            {
            case 1:
                Gasto1 = 15; Gasto2 = 12;
            break;
            case 2:
                Gasto1 = 8; Gasto2 = 4;
                break;
            case 3:
                Gasto1 = 5; Gasto2 = 2;
                break;
            case 4:
                Gasto1 = 5; Gasto2 = 1;
                break;
        }        
    }
    public void ForgeInCreation(Constructor constructor, Circuit circuito1, Circuit circuito2, Circuit circuito3, Circuit circuito4, int cirlvl, Type tipo,Move mv)
    {
        if (!Forged)
        {

            Slot1 = Instantiate(circuito1);
            Slot2 = Instantiate(circuito2);
            Slot3 = Instantiate(circuito3);
            Slot4 = Instantiate(circuito4);
            CriarPente(cirlvl, tipo,mv);
            Forge();         
           
        }
    }
public void Forge()
{
    if (!Forged && Slot1 != null)
    {
            Id = Slot1.Id;
            //calcula o valor:
            Valor[Slot1.Id] += Slot1.value;
            Valor[Slot2.Id] += Slot2.value;
            Valor[Slot3.Id] += Slot3.value;
            Valor[Slot4.Id] += Slot4.value; ;
            Forged = true;
            SpriteAtual = ForgedImage;
            if(Id == 5)
            { 
                GastoAtual = Gasto2; 
                for(int i = 0; i<5;i++)
                {
                    Valor[i] = 0;
                }
            } 
            else
            { 
                GastoAtual = Gasto1;
                Valor[5] = 0;
            }
            //0 - attack
            //1 - spattack
            //2 - resistance
            //3 - speed
            //4 - integrity
            //5 - buffer;
            //9- sem
            switch (Id)
            {
                case 0:
                    switch(ManagerGame.Instance.Idm)
                    {
                        case 0:
                            Nome = "Ataque";
                            break;
                        case 1:
                            Nome = "Attack";
                            break;
                    }                  
                    break;
                case 1:
                    switch (ManagerGame.Instance.Idm)
                    {
                        case 0:
                            Nome = "Ataque Especial";
                            break;
                        case 1:
                            Nome = "Special Attack";
                            break;
                    }
                    
                    break;
                case 2:
                    switch (ManagerGame.Instance.Idm)
                    {
                        case 0:
                            Nome = "Resistencia";
                            break;
                        case 1:
                            Nome = "Resistence";
                            break;
                    }
                    break;
                case 3:
                    switch (ManagerGame.Instance.Idm)
                    {
                        case 0:
                            Nome = "Velocidade";
                            break;
                        case 1:
                            Nome = "Speed";
                            break;
                    }
                    break;
                case 4:
                    switch (ManagerGame.Instance.Idm)
                    {
                        case 0:
                            Nome = "Integridade";
                            break;
                        case 1:
                            Nome = "Integrity";
                            break;
                    }
                    break;
                case 5:
                    Nome = "Buffer";
                    break;
            }
        }
}
 int calculavalor(Circuit circuito)
    {       
      return circuito.value;        
    }
public void Unforge()
{
    if (Forged)
    {
        Destroy(Slot1);
            Destroy(Slot2);
            Destroy(Slot3);
            Destroy(Slot4);
            for(int i = 0; i<6;i++)
            {
                Valor[i] = 0;
            }            
            Id = 9;
        Forged = false;
        SpriteAtual = EmptyImage;
            GastoAtual = 0;
        }
}
    public void CarregarSalvo(DadoPente dado)
    {
        Move mv;
        if (dado.Move.Aleatório)
        {
            mv = ScriptableObject.CreateInstance<Move>();
            mv.CarregarSalvo(dado.Move);
        }
        else
        {
            mv = Constructor.Instance.MovesEstaticos[dado.Move.Array];
            mv.meuIdnoAtaque = dado.Move.meuIdnoAtaque;
            mv.meuIdNoPente = dado.Move.meuIdNoPente;
            mv.meuIdnoAtivos = dado.Move.meuIdnoAtivos;
            mv.emUso = dado.Move.emUso;
        }
        
        if (!dado.Forged)
        {
          CriarPente(dado.Level, Constructor.Instance.TypeCreator(2),mv);
        }
        else
        {
            Circuit c1 = ScriptableObject.CreateInstance<Circuit>();
            c1.CarregarDadoSalvo(dado.Slot1);
            Circuit c2 = ScriptableObject.CreateInstance<Circuit>();
            c2.CarregarDadoSalvo(dado.Slot2);
            Circuit c3 = ScriptableObject.CreateInstance<Circuit>();
            c3.CarregarDadoSalvo(dado.Slot3);
            Circuit c4 = ScriptableObject.CreateInstance<Circuit>();
            c4.CarregarDadoSalvo(dado.Slot4);
            ForgeInCreation(Constructor.Instance, c1, c2, c3, c4, dado.Level, Constructor.Instance.TypeCreator(2), mv);
        }
       
    }

}

