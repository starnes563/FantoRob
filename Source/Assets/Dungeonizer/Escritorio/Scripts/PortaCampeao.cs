using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaCampeao : MonoBehaviour
{
    public AudioClip SomAbrirPorta;
    private bool pdabrir = false;
    public Sprite PortaAberta;
    public GameObject Spawn;
    [HideInInspector]
    public CaixaDialogo CaixaDeDialogo;
    public Dialogo TextoLiberarPorta;
    private bool aberta = false;
    public enum Andar
    {
        Primeiro,
        Ultimo,
    }
    public Andar MeuAndar;
    void Start()
    {       
        Spawn.SetActive(false);
        CaixaDeDialogo = GameObject.FindWithTag("MainCamera").transform.GetChild(0).GetComponent<CaixaDialogo>();
        if(MeuAndar == Andar.Ultimo) { TextoLiberarPorta.LerOTexto(ManagerGame.Instance.Idm); }     
    }    
    void Clicou()
    {
        switch (MeuAndar)
        {
            case Andar.Primeiro:
                if (StoryEvents.UltimoandarLiberado)
                {
                    abrir();
                }
                break;
            case Andar.Ultimo:
                abrir();
                if(!StoryEvents.UltimoandarLiberado)
                {
                    StoryEvents.UltimoandarLiberado = true;
                    CaixaDeDialogo.ReceberDialogo(TextoLiberarPorta);
                }
                break;
        }
        
    }
    void abrir()
    {
        Diretor.DesativarMenuPlayer();
        Spawn.SetActive(true);
        if (MeuAndar == Andar.Ultimo)
        {
            GetComponent<AudioSource>().PlayOneShot(SomAbrirPorta);
        }
        GetComponent<SpriteRenderer>().sprite = PortaAberta;
        aberta = true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            pdabrir = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            pdabrir = false;
        }
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && pdabrir && !aberta)
        {
            Clicou();
        }
    }
}
