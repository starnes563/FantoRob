using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Loot
{
    public enum TipodeLoot
    {
        ITEMCONSTRUIR,
        PENTEVAZIO,
        PENTECHEIO,
        CIRCUITO,
        SILICIO,
        PARTEROBO,
    }
    public TipodeLoot MeuTipo;
    public int Propriedade;
    public float ChanceDeDrop;
    public int partid;
    public void MeADicionaAoInventario()
    {
        switch(MeuTipo)
        {
            case TipodeLoot.ITEMCONSTRUIR:
                PlayerObjects.ItensConstruir[Propriedade]++;
                break;
            case TipodeLoot.PENTEVAZIO:
                PlayerObjects.PentesVazios.Add(Constructor.Instance.CombConstructor(true, Propriedade));
                break;
            case TipodeLoot.PENTECHEIO:
                PlayerObjects.PentesCheios.Add(Constructor.Instance.CombConstructor(false, Propriedade));
                break;
            case TipodeLoot.CIRCUITO:
                PlayerObjects.Circuits[Propriedade]++;
                break;
            case TipodeLoot.SILICIO:
                PlayerObjects.Silicon++;
                break;
            case TipodeLoot.PARTEROBO:
              
                PlayerObjects.RobotParts.Add(Constructor.Instance.PartConstructor(partid, Random.Range(0, 5), Propriedade));
                break;
        }
    }
    public void GeraPart()
    {
        partid = 1;
        if (PlayerStatus.Estrelas < 2)
        {
            partid = Random.Range(1, 3);
        }
        else if (PlayerStatus.Estrelas < 4)
        {
            partid = Random.Range(1, 5);
        }
        else if (PlayerStatus.Estrelas >= 4)
        {
            partid = Random.Range(0, 5);
        }
    }
}
