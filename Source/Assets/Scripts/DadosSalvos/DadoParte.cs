using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class DadoParte
{   
    public int Id;
    public string Nome;    
    public int Compilador;
    public int Placa;
    public int[] Value = new int[6];
    public int Energyspent;
    public DadoPente[] Pente = new DadoPente[5];
    public int Nivel;
    public bool Nulo = true;
    public DadoParte(RobotPart p)
    {
        Id = p.Id;
        Nome = p.Nome;
        Compilador = p.Compilador;
        Placa = p.Placa;
        Value = p.Value;
        Energyspent = p.Energyspent;
        for (int i = 0; i<5;i++)
        {
            if(p.Pente[i] !=null)
            {
                Pente[i] = new DadoPente(p.Pente[i]);
            }
        }
        Nivel = p.Nivel;
        Nulo = false;
    }

}
