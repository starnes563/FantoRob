using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevadorTorre : MonoBehaviour
{    
    public GameObject MenuElevador;
    // Start is called before the first frame update
    void MostrarMenu()
    {
        MenuElevador.SetActive(true);
    }
   
    
}