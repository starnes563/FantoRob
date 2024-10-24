using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FantoPorta : MonoBehaviour
{
    public List<GameObject> Portas = new List<GameObject>();
    public AudioClip SomPorta;
    public int Desafio;
    bool aberta = false;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(StoryEvents.DesafiosCamp[Desafio].Chavegrande && collision.tag == "Player" && !aberta)
        {
            foreach(GameObject g in Portas)
            {
                g.SetActive(false);
            }
            GetComponent<AudioSource>().PlayOneShot(SomPorta);
            aberta = true;
        }
    }
}
