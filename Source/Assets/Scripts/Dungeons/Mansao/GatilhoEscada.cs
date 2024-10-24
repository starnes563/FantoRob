using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatilhoEscada : MonoBehaviour
{
    public enum Tipo
    {
        ESCORREGAR,
        PARAR,
    }
    public Tipo MeuTipo;
    public EscadaMansao MinhaEscada;

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (MeuTipo)
        {
            case Tipo.ESCORREGAR:
                if (other.gameObject.tag == "Player")
                {
                    MinhaEscada.StartSliding();
                }
                break;
            case Tipo.PARAR:
                if (other.gameObject.tag == "Player")
                {
                    MinhaEscada.StopSliding();
                }
                break;

        }
    }

}
