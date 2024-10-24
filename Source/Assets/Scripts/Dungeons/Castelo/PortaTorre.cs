using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaTorre : MonoBehaviour
{
    public GameObject MinhaPorta;
    public AudioSource AudioSource;
    public AudioClip EfeitoSom;
    public bool Trancada = false;
    public GameObject Luz;
    public bool LiberarPorta;
    // Start is called before the first frame update   
    private void Start()
    {
        if (Trancada)
        {
            if (!StoryEvents.Torredoisaberta)
            {
                MinhaPorta.gameObject.SetActive(false);
            }            
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "Player" && MinhaPorta != null)
        {
            if (AudioSource != null && EfeitoSom != null) { AudioSource.PlayOneShot(EfeitoSom); }
            if(Luz != null) { Luz.gameObject.SetActive(true); }
            if (LiberarPorta && !StoryEvents.Torredoisaberta) { StoryEvents.Torredoisaberta = true; }
            Destroy(MinhaPorta);
        }

    }
}
