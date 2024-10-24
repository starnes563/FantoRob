using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class DadoCircuito
{
    public int arrayindex;
    public DadoCircuito (Circuit circuito)
    {
        arrayindex = circuito.Arrayindex;
    }
}
