using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortaEntreterimento : MonoBehaviour
{
    public int Nivel;
    public GameObject PortaFechada;
    public GameObject PortaAberta;
    AudioSource audioSource;
    public AudioClip SomPorta;
    public List<Text> Numeros = new List<Text>();
    bool aberta = false;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if(Numeros.Count>0)
        {
            foreach(Text t in Numeros)
            {
                t.text = Nivel.ToString();
            }
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
       if (other.tag == "Player" && StoryEvents.NivelCartao >=Nivel && !aberta)
       {
            PortaAberta.SetActive(true);
            PortaFechada.SetActive(false);
            audioSource.PlayOneShot(SomPorta);
            aberta = true;
       }
    }
}
