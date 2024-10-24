using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porta : MonoBehaviour
{
    public GameObject MinhaPorta;
    public AudioSource AudioSource;
    public AudioClip EfeitoSom;
    public bool CartaDeEndosso = false;
    public bool Desafio = false;
    public bool ChaveTorre = false;
    public int Estrela;
    bool aberta = false;
    // Start is called before the first frame update       
    private void OnTriggerEnter2D(Collider2D other)
    {
     if(CartaDeEndosso)
     {
       if(PlayerStatus.CartaEndosso)
       {
                if (Desafio)
                {
                    if (PlayerStatus.Estrelas >= Estrela)
                    {
                        if (other.GetComponent<Walk>().CanIWalk && !aberta)
                        {
                            if (AudioSource != null && EfeitoSom != null) { AudioSource.PlayOneShot(EfeitoSom); }
                            Destroy(MinhaPorta);
                            aberta = true;
                        }
                    }
                }
                else if (other.tag == "Player" && MinhaPorta != null)
                {
                    if (other.GetComponent<Walk>().CanIWalk && !aberta)
                    {
                        if (AudioSource != null && EfeitoSom != null) { AudioSource.PlayOneShot(EfeitoSom); }
                        Destroy(MinhaPorta);
                        aberta = true;
                    }
                }
       }

     }
     else if (ChaveTorre)
     {
            if(StoryEvents.ChaveTorre)
            {
                if (other.tag == "Player" && MinhaPorta != null)
                {
                    if (other.GetComponent<Walk>().CanIWalk && !aberta)
                    {
                        if (AudioSource != null && EfeitoSom != null) { AudioSource.PlayOneShot(EfeitoSom); }
                        Destroy(MinhaPorta);
                        aberta = true;
                    }
                }
            }
     }
      else if(other.tag == "Player" && MinhaPorta != null)
     {
            if (other.GetComponent<Walk>().CanIWalk && !aberta)
            {
                if (AudioSource != null && EfeitoSom != null) { AudioSource.PlayOneShot(EfeitoSom); }
                Destroy(MinhaPorta);
                aberta = true;
            }
     }     
    }
}
