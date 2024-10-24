using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaixaDeForca : MonoBehaviour
{
    public GameObject FuzilAzul;
    public AudioSource Source;
    public AudioClip SomLigar;
    public AudioClip SomDesligar;
    bool pode = false;
    enum estado
    {
        LIGADO,
        DESLIGADO,
    }
    estado meuEstado;
    // Start is called before the first frame update
    void Start()
    {
        if(!StoryEvents.BoolCaverna[1])
        {
            FuzilAzul.SetActive(false);
            meuEstado = estado.DESLIGADO;
        }
        else
        {
            FuzilAzul.SetActive(true);
            meuEstado = estado.LIGADO;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(pode && Input.GetButtonDown("Fire1"))
        {

            switch (meuEstado)
            {               
                case estado.LIGADO:
                    desligar();                    
                    break;
                case estado.DESLIGADO:
                    ligar();                   
                    break;
            }           
        }
    }
    void ligar()
    {       
        if (!StoryEvents.BoolCaverna[1]&&StoryEvents.DesafiosCamp[4].Itemdesafio)
        {            
            StoryEvents.BoolCaverna[1] = true;
            FuzilAzul.SetActive(true);
            Source.PlayOneShot(SomLigar);
            meuEstado = estado.LIGADO;
            StoryEvents.TrapacaDesafio[4] = false;
        }
    }
    void desligar()
    {
        if (StoryEvents.BoolCaverna[1])
        {
            StoryEvents.BoolCaverna[1] = false;
            FuzilAzul.SetActive(false);
            Source.PlayOneShot(SomDesligar);
            meuEstado = estado.DESLIGADO;
            StoryEvents.TrapacaDesafio[4] = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
            {
            pode = true;           
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            pode = false;           
        }
    }
}
