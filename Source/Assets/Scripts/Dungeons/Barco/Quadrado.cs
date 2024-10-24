using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quadrado : MonoBehaviour
{
    private Animator MeuAnimator;
    public int MeuValor;
    public ControleDeSaida ControleDeSaida;
    public AudioSource AudioSource;
    public AudioClip AudioClip;
    // Start is called before the first frame update
    void Start()
    {
        MeuAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Acender()
    {
        MeuAnimator.SetTrigger("Acender");
        AudioSource.PlayOneShot(AudioClip);
    }
    void enviarmeuValor()
    {
        ControleDeSaida.ReceberValor(MeuValor);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && ControleDeSaida.TurnoPlayer)
        {
            Acender();
            enviarmeuValor();
        }
    }
}
