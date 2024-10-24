using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomFantoRob : MonoBehaviour
{
    public AudioSource audioSource;
    //audioClips
    public AudioClip FisicoNaoEfetivo;
    public AudioClip FisicoEfetivo;
    public AudioClip ElementalNaoEfetivo;
    public AudioClip ElementalEfetivo;
    public AudioClip AumentoDeBarra;
    public AudioClip SomNaoFuncionou;
    public AudioClip SomPerdeu;
    public List<AudioClip> SomNota = new List<AudioClip>();    
    public static AudioSource Instancia;
    private static AudioClip nelemementanefetivo;
    private static AudioClip nelementalefetivo;
    private static AudioClip elementalnefetivo;
    private static AudioClip elementalefetivo;
    private static AudioClip subirbarra;
    private static AudioClip negado;
    private static AudioClip perdeu;
    public static List<AudioClip> somNota = new List<AudioClip>();
    // Start is called before the first frame update
    void Awake()
    {        
        Instancia = audioSource;
        nelemementanefetivo = FisicoNaoEfetivo;
        nelementalefetivo = FisicoEfetivo;
        elementalnefetivo = ElementalNaoEfetivo;
        elementalefetivo = ElementalEfetivo;
        subirbarra = AumentoDeBarra;
        negado = SomNaoFuncionou;
        perdeu = SomPerdeu;
        foreach(AudioClip clip in SomNota)
        {
            somNota.Add(clip);
        }
    }

    public static void NElementalNEfetivo()
    {
     
        Instancia.PlayOneShot(nelemementanefetivo);
    }
    public static void NElementalEfetivo()
    {
        Instancia.PlayOneShot(nelementalefetivo);
    }
    public static void ElementalNEfetivo()
    {
        Instancia.PlayOneShot(elementalnefetivo);
    }
    public static void ElementEfetivo()
    {
        Instancia.PlayOneShot(elementalefetivo);
    }
    public static void AumentarBarra()
    {
        Instancia.PlayOneShot(subirbarra);
    }
    public static void SomNegado()
    {
        Instancia.PlayOneShot(negado);
    }
    public static void TocarSomPerdeu()
    {
        Instancia.PlayOneShot(perdeu);
    }
    public static void TocarSomNota(int i)
    {
        Instancia.PlayOneShot(somNota[i]);
    }
}
