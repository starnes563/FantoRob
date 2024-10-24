using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Centrifuga : MonoBehaviour
{
    public UnMerger um;
    public AudioSource source;
    public AudioClip Rodando;
    public AudioClip Finalizou;
    public void Finalizarcentrifuga()
    {
        um.Separar();
        source.loop = false;
        source.Stop();
        source.PlayOneShot(Finalizou);
        um.animando = false;
    }
    public void Somrodando()
    {
        source.clip = Rodando;
        source.Play();
        source.loop = true;
    }
    public void DispararCentrifuga()
    {
        if(!um.animando)
        {
            this.GetComponent<Animator>().SetTrigger("centrifuga");
            um.animando = true;
        }        
    }
}