using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotPartyButtonSec : MonoBehaviour
{
    [HideInInspector]
    public RobotPartyButton Botao;
   public void Trocar()
    {
        Botao.Retirar();
    }
    public void TornarLider()
    {
        Botao.TornarLider();
    }
    public void Ver()
    {
        Botao.Ver();
    }
    public void UsarItem()
    {
        Botao.UsarItem();
    }
}
