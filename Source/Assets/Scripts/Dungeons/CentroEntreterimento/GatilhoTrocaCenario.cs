using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatilhoTrocaCenario : MonoBehaviour
{
    public List<TrocaCenario> TrocaCenario;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            foreach (TrocaCenario t in TrocaCenario)
            {
                t.Gatilho = 1;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            foreach (TrocaCenario t in TrocaCenario)
            {
                t.Gatilho = 1;
            }
        }
    }
}
