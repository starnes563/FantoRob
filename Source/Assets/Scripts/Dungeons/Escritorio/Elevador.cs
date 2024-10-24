using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevador : MonoBehaviour
{   
    public bool Aberta = false;
    public bool ElevadorUltimoAndar = false;
    public Animator AnimatorElevador;
    public AudioClip SomAbrirPorta;
    public AudioSource AudioSource;
    private bool pdabrir = false;
    // Start is called before the first frame update 
    private void Start()
    {
        if(ElevadorUltimoAndar && StoryEvents.UltimoandarLiberado)
        {
            Aberta = true;
        }
    }
    public void AbrirPorta()
    {
        Aberta = true;
    }
   private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && Aberta)
        {
            pdabrir = true;            
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player" && Aberta)
        {
            pdabrir = false;
        }
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && Aberta && pdabrir)
        {
            Clicou();
        }
    }
    private void Clicou()
    {
        Diretor.DesativarMenuPlayer();
        Aberta = false;
        pdabrir = false;
        AnimatorElevador.SetTrigger("Abre");
        AudioSource.PlayOneShot(SomAbrirPorta);
    }
}
