using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicaFugirFanto : MonoBehaviour
{
    public AudioSource Source;
    public AudioClip Clip;
    bool acionado;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        if(acionado)
        {
            if(!Source.isPlaying)
            {
                CaixaDeSom.Instancia.GetComponent<AudioSource>().Play();
                CaixaDeSom.Instancia.SobeVolume();
                acionado = false;
            }
        }
    }
    public void Acionar()
    {
        CaixaDeSom.Instancia.GetComponent<AudioSource>().Pause();
        Source.PlayOneShot(Clip);
        acionado = true;
    }
}
