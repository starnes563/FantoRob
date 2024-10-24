using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonsMenu : MonoBehaviour
{
    public AudioClip SomConfirmar;
    public AudioClip SomDesistir;
    public AudioClip SomRecuperar;
    public AudioClip SomNaoPode;
    private AudioSource audiosource;    
    private static AudioSource source;
    private static AudioClip clipConfimar;
    private static AudioClip clipDesistir;
    private static AudioClip clipRecuperar;
    private static AudioClip clipnegado;
    // Start is called before the first frame update
    void Awake()
    {
        audiosource = GetComponent<AudioSource>();
        source = audiosource;
        clipConfimar = SomConfirmar;
        clipDesistir = SomDesistir;
        clipRecuperar = SomRecuperar;
        clipnegado = SomNaoPode;
    }
    // Update is called once per frame
    public static void Confimar()
    {
        source.PlayOneShot(clipConfimar);
    }
    public static void Desistir()
    {
        source.PlayOneShot(clipDesistir);
    }
    public static void Recuperar()
    {
        source.PlayOneShot(clipRecuperar);
    }
    public static void Negado()
    {
        source.PlayOneShot(clipnegado);
    }
}
