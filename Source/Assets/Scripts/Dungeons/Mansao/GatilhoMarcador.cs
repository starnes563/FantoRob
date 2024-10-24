using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatilhoMarcador : MonoBehaviour
{
    public int ID;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (ID > 0) { StoryEvents.DesafiosCamp[5].Interagiveis[ID] = true; }            
        }
    }
}
