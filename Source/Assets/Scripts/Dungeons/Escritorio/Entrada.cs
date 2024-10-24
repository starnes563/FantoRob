using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrada : MonoBehaviour
{
    private GameObject Jogador;
    // Start is called before the first frame update
    void Start()
    {
        Jogador = GameObject.FindWithTag("Player");
        Jogador.transform.position = this.transform.position;
    }

}
