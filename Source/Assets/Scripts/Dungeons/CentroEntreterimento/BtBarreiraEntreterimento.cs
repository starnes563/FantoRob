using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtBarreiraEntreterimento : MonoBehaviour
{
    public bool Laranja;
    public AudioSource Source;
    public AudioClip SomApertar;
    public AudioClip SomApertado;
    public SpriteRenderer Renderer;
    public Sprite Apertado;
    public Sprite Desapertado;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Renderer.sprite = Apertado;
            if(StoryEvents.DesafiosCamp[6].Interagiveis[0] != Laranja)
            {
                StoryEvents.DesafiosCamp[6].Interagiveis[0] = Laranja;
                Source.PlayOneShot(SomApertar);
            }
            else
            {
                Source.PlayOneShot(SomApertado);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Renderer.sprite = Desapertado;
        }
    }

}
