using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotaoDesafio : MonoBehaviour
{
    public int MeuID;
    public Sprite SpriteApertado;
    public enum Tipo
    {
        BAU,
        PORTA,
    }
    public Tipo MeuTipo;
    public BloqueioDesafio MinhaPorta;
    public BauDesafio MeuBau;
    bool apertado;
    public AudioClip SomBotao;
    public int Desafio;
    // Start is called before the first frame update
    void Start()
    {
        botaoApertado();
    }

    // Update is called once per frame  
    void botaoApertado()
    {
        if (StoryEvents.DesafiosCamp[Desafio].Interagiveis[MeuID] == true)
        {
            apertado = true;
            GetComponent<SpriteRenderer>().sprite = SpriteApertado;
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player" || collider.tag == "Bloco")
        {
            apertarBotao();
        }
    }
    void apertarBotao()
    {
        if(!apertado)
        {
            apertado = true;
            GetComponent<SpriteRenderer>().sprite = SpriteApertado;
            GetComponent<AudioSource>().PlayOneShot(SomBotao);
            switch(MeuTipo)
            {
                case Tipo.BAU:
                    MeuBau.DestrancarBau();
                    break;
                case Tipo.PORTA:
                    MinhaPorta.destrancarPorta();
                    break;
            }
        }
    }
}
