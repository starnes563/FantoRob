using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrocaMusica : MonoBehaviour
{
    private AudioSource CXSom;
    private CaixaDeSom SpriptCX;
    // Start is called before the first frame update   
    public void TrocarMusicaInst(AudioClip clip)
    {
        CXSom = CaixaDeSom.Instancia.GetComponent<AudioSource>();
        CXSom.clip = clip;
        CXSom.loop = true;
        CXSom.Play();        
    }
    public void TrocarMusicaSuave(AudioClip clip)
    {
        CXSom = CaixaDeSom.Instancia.GetComponent<AudioSource>();
        SpriptCX = CXSom.gameObject.GetComponent<CaixaDeSom>();
        CXSom.clip = clip;
        CXSom.loop = true;
        CXSom.Play();
        SpriptCX.SobeVolume();
    }
    public void Pausar()
    {
        CXSom = CaixaDeSom.Instancia.GetComponent<AudioSource>();
        CXSom.Pause();
    }
    public void Tocar()
    {
        CXSom = CaixaDeSom.Instancia.GetComponent<AudioSource>();
        CXSom.Play();
    }

}
