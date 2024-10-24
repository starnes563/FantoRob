using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatilhoBauCorreto : MonoBehaviour
{
    public ControlaUltimoBau Controla;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "FantoFugitivo") { Controla.BauAparecer(); }
    }
}
