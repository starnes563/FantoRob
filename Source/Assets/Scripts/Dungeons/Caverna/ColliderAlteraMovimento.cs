using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderAlteraMovimento : MonoBehaviour
{
    [Range(-1, 1)]
    public int X;
    [Range(-1, 1)]
    public int Y;
    Vagoneta vg;
    public enum Tipo
    {
        ALTERAR,
        PARAR,
    }
    public Tipo MeuTipo;
    public Vector3 PosicDesemb;
    public string AnimDesemb;
    public GatilhoAcionarVagoneta MeuDesembarque;
    [Range(-1, 1)]
    public int XCondicional;
    [Range(-1, 1)]
    public int YCondicional;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Vagoneta")
        {            
            vg = collision.gameObject.GetComponent<Vagoneta>();
            switch(MeuTipo)
            {               
                case Tipo.PARAR:
                    vg.X = 0;
                    vg.Y = 0;
                    vg.Desembarar(PosicDesemb, AnimDesemb);
                    if(MeuDesembarque != null)
                    {
                        MeuDesembarque.Estacionado = true;
                        MeuDesembarque.Vagoneta = vg;
                    }
                    break;
            }            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Vagoneta")
        {
            vg = collision.gameObject.GetComponent<Vagoneta>();
            switch (MeuTipo)
            {
                case Tipo.ALTERAR:
                    if(vg.X == XCondicional && vg.Y == YCondicional)
                    {
                        vg.X = X;
                        vg.Y = Y;
                    }              
                    break;
            }
        }
    }
}
