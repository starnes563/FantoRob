using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZerarDesafio : MonoBehaviour
{
    // Start is called before the first frame update
    public int Desafio;
    void Start()
    {
        StoryEvents.DesafiosCamp[Desafio].Chavegrande = false;
        StoryEvents.DesafiosCamp[Desafio].Chavepequena = 0;
        StoryEvents.DesafiosCamp[Desafio].Itemdesafio = false;
        for (int i = 0; i<99;i++)
        {
            StoryEvents.DesafiosCamp[Desafio].Interagiveis[i] = false;
        }
    }


}
