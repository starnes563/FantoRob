using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotCore : MonoBehaviour
{
    // status minimos
    public int Integridademin;
    public int Resistenciamin;
    public int Velocidademin;
    public int Ataquemin;
    public int AtaqueEnergeticomin;

    public GameObject Bateria;

    //status maximos
    public int Integridademax;
    public int Resistenciamax;
    public int Velocidademax;
    public int Ataquemax;
    public int AtaqueEnergeticomax;

    public string Modelo;
    public string Nome;
    public int Nucleo;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
