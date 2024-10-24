using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orgao : MonoBehaviour
{
    public List<GameObject> Teclas = new List<GameObject>(8);
    public AudioSource AudioSource;
    public AudioClip Musica;
    public AudioClip SomAbrir;
    public static bool aberto = false;
    private Animator anim;
    private Walk player;
    int NumeroAchado;
    public GameObject CineMachine;
    bool podeabrir = false;
    public PortaMansao PortaEntrada;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();       
        if (StoryEvents.DesafiosCamp[5].Interagiveis[51]) { anim.SetBool("Aberto", true); PortaEntrada.abrir(); }
    }
    private void Update()
    {
        if(podeabrir && Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(abrir());
        }
    }
    void MostrarTeclas()
    {
        for (int i = 0; i < 4; i++)
        {
            Teclas[i].gameObject.SetActive(StoryEvents.TeclasOrgao[i]);
        }
    }
     IEnumerator abrir()
    {
        podeabrir = false;
        player.PararDeAndar();
        AudioSource.PlayOneShot(Musica);
        while(AudioSource.isPlaying)
        {
            yield return null;
        }
        anim.SetTrigger("Abrir");        
    }
    public void LigarAbertura()
    {
        StoryEvents.DesafiosCamp[5].Interagiveis[51] = true;
        anim.SetBool("Aberto", true);
        player.CanIWalk = true;
    }
    void alterarnumeroDeTeclas()
    {
        NumeroAchado = 0;
        for (int i = 0; i < 4; i++)
        {
            if (StoryEvents.TeclasOrgao[i]) { NumeroAchado++; }
        }
        MostrarTeclas();
        if (NumeroAchado > 3) { podeabrir = true; }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.tag =="Player" && !aberto && NumeroAchado<4)
        {
            player = collision.GetComponent<Walk>();
            alterarnumeroDeTeclas();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(NumeroAchado<4 && !aberto)
        {
            foreach(GameObject tecla in Teclas)
            {
                tecla.SetActive(false);
                podeabrir = false;
            }
        }
    }
    public void DesativaCineMachine()
    {
        CineMachine.SetActive(false);
    }
    public void AtivaCinemachine()
    {
        CineMachine.SetActive(true);
    }
    public void TocarSomAbrir()
    {
        AudioSource.clip = SomAbrir;
        AudioSource.loop = true;
        AudioSource.Play();
    }
    public void PararDeTocar()
    {
        AudioSource.Stop();
        AudioSource.loop = false;
        player.CanIWalk = true;
        StoryEvents.DesafiosCamp[5].Interagiveis[51] = true;
        PortaEntrada.abrir();
    }

}
