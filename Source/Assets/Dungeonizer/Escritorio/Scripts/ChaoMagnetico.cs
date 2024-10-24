using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaoMagnetico : MonoBehaviour
{
    public GameObject Box;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(StoryEvents.DesafiosCamp[0].Itemdesafio)
            {
                Box.gameObject.SetActive(false);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (StoryEvents.DesafiosCamp[0].Itemdesafio)
            {
                Box.gameObject.SetActive(true);
            }
        }
    }
}

