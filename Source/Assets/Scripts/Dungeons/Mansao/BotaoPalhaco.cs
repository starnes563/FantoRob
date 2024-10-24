using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotaoPalhaco : MonoBehaviour
{
    public int Id;
    public BauDesafio MeuBau;
    // Start is called before the first frame update
    void Start()
    {
        if(StoryEvents.DesafiosCamp[5].Interagiveis[Id])
        {
            abrirBau();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            StoryEvents.DesafiosCamp[5].Interagiveis[Id] = true;
        }
    }
    void abrirBau()
    {
        if(!StoryEvents.DesafiosCamp[5].Interagiveis[MeuBau.MeuID])
        {
            MeuBau.DestrancarBau();
        }       
    }
}
