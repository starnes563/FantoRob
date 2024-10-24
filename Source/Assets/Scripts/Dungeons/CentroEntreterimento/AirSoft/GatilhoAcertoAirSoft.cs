using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatilhoAcertoAirSoft : MonoBehaviour
{
    public AlvoAirSoft MeuAlvo;
    public int MeusPontos;
    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if(collision.tag == "BalaAirSoft")
        {
            MeuAlvo.Acertou(MeusPontos);
            Destroy(collision.gameObject);
        }
    }
}
