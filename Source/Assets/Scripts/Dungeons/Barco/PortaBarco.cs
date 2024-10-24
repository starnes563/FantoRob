using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaBarco : MonoBehaviour
{
    public GameObject MinhaPorta;
    public AudioSource AudioSource;
    public AudioClip EfeitoSom;
    public int MinhaOrdem;
    public bool Chave = false;
    // Start is called before the first frame update       
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (MinhaOrdem==StoryEvents.PortasBarco && !Chave || PlayerStatus.Estrelas>=3)
        {            
                if (other.tag == "Player" && MinhaPorta != null)
                {
                    if (AudioSource != null && EfeitoSom != null) { AudioSource.PlayOneShot(EfeitoSom); }
                    Destroy(MinhaPorta);
                }            
        }
        else if(Chave)
        {
            if(StoryEvents.ChaveDespensa)
            {
                if (other.tag == "Player" && MinhaPorta != null)
                {
                    if (AudioSource != null && EfeitoSom != null) { AudioSource.PlayOneShot(EfeitoSom); }
                    Destroy(MinhaPorta);
                }
            }
        }
    }
}
