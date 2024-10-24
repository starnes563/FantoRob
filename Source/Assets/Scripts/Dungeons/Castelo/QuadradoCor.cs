using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadradoCor : MonoBehaviour
{
    public bool certo = false;
    public PerguntaCor PerguntaCor;
    
    void responder()
    {
        if(PerguntaCor.TurnoJogador)
        {
            if(certo)
        {
                PerguntaCor.Acertou();
            }
        else
            {
                PerguntaCor.Errou();
            }
        }        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            responder();
        }
    }
}
