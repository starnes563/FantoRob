using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaNarrador : MonoBehaviour
{
    GerenciadorDialogo gerenciadorDialogo;
    // Start is called before the first frame update
    void Start()
    {
        gerenciadorDialogo = GetComponent<GerenciadorDialogo>();
        comecar();
    }
    void comecar()
    {
        for(int i = 1; i<4; i++)
        {
            gerenciadorDialogo.DisplayNextSetence();
        }
    }    
}
