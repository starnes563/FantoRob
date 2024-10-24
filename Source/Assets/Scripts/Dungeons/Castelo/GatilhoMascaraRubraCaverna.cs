using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class GatilhoMascaraRubraCaverna : MonoBehaviour
{
    public PlayableDirector Director;
    [HideInInspector]
    public Walk player;
    public int ID;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !StoryEvents.DesafiosCamp[4].Interagiveis[ID])
        {
            player = collision.GetComponent<Walk>();
            StoryEvents.DesafiosCamp[4].Interagiveis[ID] = true;
            player.PararDeAndar();
            Director.Play();
        }
    }
}
