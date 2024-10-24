using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomBarraDeAcao : MonoBehaviour
{
    private AudioSource meuAudioSource;
    public static AudioSource Instancia;
    // Start is called before the first frame update
    void Start()
    {
        meuAudioSource = GetComponent<AudioSource>();
        Instancia = meuAudioSource;
    }

    
}
