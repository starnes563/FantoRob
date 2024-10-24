using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class DadoPente
{
    public int Id;   
    public int Level;   
    public int[] Valor = new int[6];   
    public int GastoAtual;   
    public int Gasto1;
    public int Gasto2;   
    public DadoCircuito Slot1;   
    public DadoCircuito Slot2;   
    public DadoCircuito Slot3;   
    public DadoCircuito Slot4;  
    public bool Forged = false;   
    public DadoMove Move;   
    public int ArrayIndex;
    public string Nome;    
    public DadoPente (Pente cb)
    {
        if (cb.Forged)
        {
            Id = cb.Id;
            Level = cb.Level;
            Valor = cb.Valor;
            GastoAtual = cb.GastoAtual;
            Gasto1 = cb.Gasto1;
            Gasto2 = cb.Gasto2;
            Slot1 = new DadoCircuito(cb.Slot1);
            Slot2 = new DadoCircuito(cb.Slot2);
            Slot3 = new DadoCircuito(cb.Slot3);
            Slot4 = new DadoCircuito(cb.Slot4);
            Forged = cb.Forged;
            Move = new DadoMove(cb.Move);
            ArrayIndex = cb.ArrayIndex;
            Nome = cb.Nome;
        }
        else
        {
            Id = cb.Id;
            Level = cb.Level;
            Valor = cb.Valor;
            GastoAtual = cb.GastoAtual;
            Forged = cb.Forged;
            Move = new DadoMove(cb.Move);
            ArrayIndex = cb.ArrayIndex;
            Nome = cb.Nome;
        }
    }
}
