using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bau : MonoBehaviour
{
    bool aberto = false;
    bool podeabrir = false;
    public Sprite BauAberto;
    public Dialogo Achou;
    public int Tecla;
    public AudioClip SomAbrir;
    public GameObject ItemBau;
    CaixaDialogo cx;
    private void Start()
    {
        cx = GameObject.FindWithTag("MainCamera").transform.GetChild(0).GetComponent<CaixaDialogo>();
        Achou.LerOTexto(ManagerGame.Instance.Idm);
        BauJaAberto();
    }
    void abrir()
    {
        trocarSprite();
        podeabrir = false;
        cx.ReceberDialogo(Achou);
        StoryEvents.TeclasOrgao[Tecla] = true;
        ItemBau.gameObject.SetActive(true);
        GetComponent<AudioSource>().PlayOneShot(SomAbrir);
        UIMansao.MostrarTeclas();
    }
    private void Update()
    {
        if (podeabrir && Input.GetButtonDown("Fire1")&&!cx.gameObject.activeSelf)
        {
            abrir();
        }
    }
    public void trocarSprite()
    {
        this.GetComponent<SpriteRenderer>().sprite = BauAberto;
        aberto = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !aberto&& StoryEvents.DesafiosCamp[5].Chavegrande)
        {
            if(Tecla ==3)
            {
                if (StoryEvents.UltimaTecla) { podeabrir = true; }
            }
            else
            {
                podeabrir = true;
            }            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            podeabrir = false;
        }
    }
    public void BauJaAberto()
    {
        if (StoryEvents.DesafiosCamp[5].Interagiveis[Tecla] == true)
        {
            podeabrir = false;
            aberto = true;
            trocarSprite();
        }
    }
}
