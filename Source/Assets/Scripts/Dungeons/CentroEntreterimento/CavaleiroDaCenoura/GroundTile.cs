using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : MonoBehaviour
{
   
    public GroundSpawner GerenciadorChao;
    bool avisei = false;   
    public void Avisar()
    {
        if (!avisei)
        {
            avisei = true;
            //GerenciadorChao.GenerateTerrain();
        }
    }
}
