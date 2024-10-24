using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaTrono : MonoBehaviour
{
    public GameObject MinhaPorta;
    public List<SpriteRenderer> Arco = new List<SpriteRenderer>(); 
    public AudioSource AudioSource;
    public AudioClip EfeitoSom;
    bool PodeTrono = false;
    // Start is called before the first frame update 
    private void Start()
    {
        PodeTrono = true;
        foreach( bool b in StoryEvents.CavaeleiroCast)
        {
            if (!b) { PodeTrono = false; }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (PodeTrono)
        {           
                if (other.tag == "Player" && MinhaPorta != null)
                {
                    if (AudioSource != null && EfeitoSom != null) { AudioSource.PlayOneShot(EfeitoSom); }
                    foreach(SpriteRenderer r in Arco) { r.sortingOrder = 8; }
                    Destroy(MinhaPorta);
                }            
        }    
    }
}
