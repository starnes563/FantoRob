using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Parte", menuName = "Fantorob/Parte")]
[System.Serializable]

public class RobotPart : ScriptableObject
{
    //0 - cabeça
    // 1 - braço direito
    //2 - braço esquerdo
    //3 - perna direita
    //4 - pernsa esquerda
    public int Id;
    public string Nome;
    //0-ak
    //1-as
    //2-re
    //3-ve
    //4-it
    public int Compilador;    
    public int Placa;
    public int[] Value = new int[6];
    public int Energyspent = 0;
    public Pente[] Pente = new Pente[5];
    public int Nivel;
    public void CriarParte(int id, int compilador, int placa, int NIVEL)
    {       
        Id = id;
        Compilador = compilador;
        Placa = placa;
        Nivel = NIVEL;
        switch (Compilador)
        {
            case 0:
                Nome = "AK";
                break;
            case 1:
                Nome = "AS";
                break;
            case 2:
                Nome = "RE";
                break;
            case 3:
                Nome = "VL";
                break;
            case 4:
                Nome = "IT";
                break;
         }
    }   
    public void ZerarPlaca()
    {
        for (int i = 0; i < 6; i++)
        {
            Value[i] = 0;
        }
        Energyspent = 0;
        foreach (Pente pt in Pente)
        {
            if(pt != null)
            {
                PlayerObjects.PentesCheios.Add(Instantiate(pt));
                Destroy(pt);
            }            
        }
    }
    public void CarregarSalvo(DadoParte dado)
    {
        Id = dado.Id;
        Nome = dado.Nome;
        Compilador = dado.Compilador;
        Placa = dado.Placa;
        Value = dado.Value;
        Energyspent = dado.Energyspent;
        for (int i = 0; i < 5; i++)
        {
            if (dado.Pente[i] != null)
            {
                Pente c = ScriptableObject.CreateInstance<Pente>();
                c.CarregarSalvo(dado.Pente[i]);
                Pente[i] = c;
            }
        }
        Nivel = dado.Nivel;
    }
}

