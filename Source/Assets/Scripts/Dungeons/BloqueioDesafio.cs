using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloqueioDesafio : MonoBehaviour
{
    public int MeuID;
    public SpriteRenderer Indicador;
    public Sprite DestrancadoSP;
    public Sprite TracandoSP;
    public Sprite FechaduraSP;
    bool PodeAbrir = false;
    public AudioClip AbrirPorta;
    public AudioClip SomDestrancado;
    public enum TIPO
    {
        DESTRANCADO,
        INIMIGOS,
        BOTAO,
        FECHADURA,
    }
    public TIPO MeuTipo;
    bool aberto = false;
    //porta trancada por inimigos
    [HideInInspector]
    public List<GameObject> InimigosADerrotar = new List<GameObject>();
    float contador = 0;
    public int Desafio;
    // Start is called before the first frame update
    void Start()
    {
        portaAberta();
        IniciarPorta();
    }

    // Update is called once per frame
    void Update()
    {
        if (MeuTipo == TIPO.INIMIGOS &&!aberto)
        {
            contador += Time.deltaTime;
            if (contador >= 0.5f)
            {
                contador = 0f;
                bool devoabrir = true;
                foreach (GameObject g in InimigosADerrotar)
                {
                    if (g != null)
                    {
                        devoabrir = false;
                    }
                }
                if (devoabrir) { destrancarPorta(); }
            }
        }

        if (Input.GetButtonDown("Fire1") && PodeAbrir && !aberto && MeuTipo == TIPO.DESTRANCADO)
        {
            Clicou();
        }
        if (Input.GetButtonDown("Fire1") && PodeAbrir && !aberto && MeuTipo == TIPO.FECHADURA)
        {
            TirarFechadura();
        }

    }
    void IniciarPorta()
    {
        switch (MeuTipo)
        {
            case TIPO.DESTRANCADO:
                Indicador.sprite = DestrancadoSP;
                break;
            case TIPO.INIMIGOS:
                Indicador.sprite = TracandoSP;
                break;
            case TIPO.BOTAO:
                Indicador.sprite = TracandoSP;
                break;
            case TIPO.FECHADURA:
                Indicador.sprite = FechaduraSP;
                break;
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
        Indicador.sprite = DestrancadoSP;
        MeuTipo = TIPO.DESTRANCADO;
        StoryEvents.DesafiosCamp[Desafio].Interagiveis[MeuID] = true;
        //tocar o som;
        GetComponent<AudioSource>().PlayOneShot(SomDestrancado);
    }
    void TirarFechadura()
    {
        if (StoryEvents.DesafiosCamp[Desafio].Chavepequena > 0)
        {
            StoryEvents.DesafiosCamp[Desafio].Chavepequena--;
            destrancarPorta();
            UIDesafio.Atualizar();
        }
    }
    void portaAberta()
    {
        if (StoryEvents.DesafiosCamp[Desafio].Interagiveis[MeuID] == true)
        {
            PodeAbrir = false;
            MeuTipo = TIPO.DESTRANCADO;
            aberto = true;
            GetComponent<Animator>().SetTrigger("Abrir");
        }
    }
}