using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaMansao : MonoBehaviour
{
    public GameObject PortaAberta;
    public GameObject PortaFechada;
    bool pode;
    public AudioClip SomAbrir;
    public AudioClip SomFechar;
    bool aberta = false;
    // Update is called once per frame
    void Update()
    {
        if(pode && Input.GetButtonDown("Fire1")&& !ManagerGame.Instance.EmBatalha && !ManagerGame.Instance.Transitando) { abrir(); }
    }
    public void abrir()
    {
        PortaAberta.SetActive(true);
        PortaFechada.SetActive(false);
        GetComponent<AudioSource>().PlayOneShot(SomAbrir);
        aberta = true;
    }
    void fechar()
    {
        PortaAberta.SetActive(false);
        PortaFechada.SetActive(true);
        aberta = false;
        GetComponent<AudioSource>().PlayOneShot(SomFechar);
    }
   private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player") { pode = true; }       
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            pode = false;
            if (aberta) { fechar(); }            
        }
    }
}
