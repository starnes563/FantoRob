using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaPuzzleEntreterimento : MonoBehaviour
{
    public int MeuID;
    bool PodeAbrir = false;
    public AudioClip AbrirPorta;
    public AudioClip SomDestrancado;
    public enum TIPO
    {
        DESTRANCADO,
        TRANCADO,
    }
    public TIPO MeuTipo;
    bool aberto = false;
    //porta trancada por inimigos
    [HideInInspector]
    public List<GameObject> InimigosADerrotar = new List<GameObject>();       
    public List<bool> BoolAbrir = new List<bool>();
    public List<SpriteRenderer> SpritesIndicadores = new List<SpriteRenderer>();
    public Sprite SpriteIndicadorLiberado;
    // Start is called before the first frame update
    void Start()
    {
        portaAberta();       
    }

    // Update is called once per frame
    void Update()
    {        

        if (Input.GetButtonDown("Fire1") && PodeAbrir && !aberto && MeuTipo == TIPO.DESTRANCADO)
        {
            Clicou();
        }       
    }   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PodeAbrir = true;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PodeAbrir = false;
        }
    }
    void Clicou()
    {
        if (!ManagerGame.Instance.Transitando && !ManagerGame.Instance.EmBatalha)
        {
            PodeAbrir = false;
            Diretor.DesativarMenuPlayer();
            aberto = true;

            GetComponent<Animator>().SetTrigger("Abrir");
            GetComponent<AudioSource>().PlayOneShot(AbrirPorta);
        }
    }
    public void destrancarPorta()
    {
        //trocar imagem e abrir       
        MeuTipo = TIPO.DESTRANCADO;
        StoryEvents.DesafiosCamp[6].Interagiveis[MeuID] = true;
        //tocar o som;
        GetComponent<AudioSource>().PlayOneShot(SomDestrancado);
    }    
    void portaAberta()
    {
        if (StoryEvents.DesafiosCamp[6].Interagiveis[MeuID] == true)
        {
            PodeAbrir = false;
            MeuTipo = TIPO.DESTRANCADO;
            aberto = true;
            GetComponent<Animator>().SetTrigger("Abrir");
        }
    }
    public void AvisoAbrir(int Id)
    {
        BoolAbrir[Id] = true;
        SpritesIndicadores[Id].sprite = SpriteIndicadorLiberado;
        analisar();
    }
    void analisar()
    {
        if (MeuTipo == TIPO.TRANCADO)
        {
            bool liberado = true;
            foreach (bool b in BoolAbrir)
            {
                if (b == false) { liberado = false; }
            }
            if (liberado)
            {
                destrancarPorta();
            }
        }        
    }
}
