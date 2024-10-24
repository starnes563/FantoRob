using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonsDoMenu : MonoBehaviour
{
    private AudioSource meuAudioSource;
    private static AudioSource Instancia;
    public AudioClip ClipConfirma;
    private static AudioClip confirma;
    public AudioClip ClipDesiste;
    private static AudioClip desiste;
    // Start is called before the first frame update
    void Awake()
    {
        meuAudioSource = GetComponent<AudioSource>();
        Instancia = meuAudioSource;
        confirma = ClipConfirma;
        desiste = ClipDesiste;
    }
    // Update is called once per frame
   public static void Confirmar()
    {        
        Instancia.PlayOneShot(confirma);
    }
    public static void Desistir()
    {
        Instancia.PlayOneShot(desiste);
    }
}
