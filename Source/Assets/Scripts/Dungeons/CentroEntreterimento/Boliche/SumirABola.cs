using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SumirABola : MonoBehaviour
{
    public GerenciadorBolhice Gerenciador;
    public bool Finaliza;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bola")
        {
            collision.gameObject.SetActive(false);
            if (Finaliza) { Gerenciador.Finalizar(); }
            
        }
    }
}
