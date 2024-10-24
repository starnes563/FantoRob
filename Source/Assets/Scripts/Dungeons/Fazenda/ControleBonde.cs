using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleBonde : MonoBehaviour
{
    public Bonde Gatilho;
    public GameObject Neftari;
    public AudioClip Somporta;
    public AudioClip SomAndando;
    public AudioSource AudioSource;
    public bool Desaparece;
    // Start is called before the first frame update
    void Start()
    {
        if (Desaparece && !StoryEvents.BondeLibeardo) { this.gameObject.SetActive(false); }
    }
    public void DesativarNeftari()
    {
        Neftari.SetActive(false);
    }
    public void TrocarCena()
    {
        Gatilho.TrocarCena();
    }
    public void TocarSomPorta()
    {
        AudioSource.PlayOneShot(Somporta);
    }
    public void TocarSomAndando()
    {
        AudioSource.PlayOneShot(SomAndando);
    }

}
