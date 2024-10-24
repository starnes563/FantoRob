using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatilhoIniciarAirSoft : MonoBehaviour
{
    bool dentro;
    public GerenciadorAirSoft MeuGerenciador;
    Walk w;
    bool iniciou;
    private void Update()
    {
        if(dentro && Input.GetButtonDown("Fire1")&&!iniciou)
        {
            iniciou = true;
            StartCoroutine(MeuGerenciador.Iniciar(w));
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !dentro)
        {
            w = collision.GetComponent<Walk>();               
            dentro = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            dentro = false;
            iniciou = false;
            w = null;
        }
    }
}
