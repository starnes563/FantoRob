using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcionaElevador : MonoBehaviour
{
    public Animator Elevador;
    bool podeabrir = false;
    public AudioSource Source;
    public AudioClip SomPorta;
    bool aberto = false;
    // Update is called once per frame
    void Update()
    {
        if(podeabrir && Input.GetButtonDown("Fire1")&&!aberto)
        {
            Elevador.SetTrigger("abrir");
            Source.PlayOneShot(SomPorta);
            aberto = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            podeabrir = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            podeabrir = false;            
        }
    }
}
