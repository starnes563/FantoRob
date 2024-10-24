using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntradaAlternativa : MonoBehaviour
{
    public int CenaTroca;
    public Vector3 PosicaoTroca;
    public string AnimacaoTroca;
    public List<string> Fala1;
    public List<string> Fala2;
    public List<string> Fala3;
    public List<string> Fala4;
    public List<string> FalaErrou;
    public List<GameObject> Botoes;
    enum FalaAtual
    {
        PARTE1,
        PARTE2,
        PARTE3,
        PARTE4,
    }
    FalaAtual meuEstado;
    public Text meuTexto;
    bool terminou = false;
    public AudioClip SomAbrir;
    AudioSource source;
      private void OnEnable()
    {       
        source = GetComponent<AudioSource>();
        foreach (GameObject g in Botoes)
        {
            g.SetActive(false);            
        }
        source.PlayOneShot(SomAbrir);
        if(StoryEvents.ProibidoEntradaAlter)
        {
            meuEstado = FalaAtual.PARTE4;
            Errou();
        }
        else
        {
            meuEstado = FalaAtual.PARTE1;
            mostrarProximo();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(terminou && Input.GetButtonDown("Fire1"))
        {
            this.gameObject.SetActive(false);
            GameObject.FindWithTag("Player").GetComponent<Walk>().CanIWalk = true;
        }
    }
    public void Acertou()
    {
        source.PlayOneShot(SomAbrir);
        switch (meuEstado)
        {
            case FalaAtual.PARTE1:
                meuEstado = FalaAtual.PARTE2;
                mostrarProximo();                
                break;
            case FalaAtual.PARTE2:
                meuEstado = FalaAtual.PARTE3;
                mostrarProximo();                
                break;
            case FalaAtual.PARTE3:
                meuEstado = FalaAtual.PARTE4;
                mostrarProximo();               
                break;
            case FalaAtual.PARTE4:
                mostrarProximo();
                break;
        }
    }
    void mostrarProximo()
    {
        foreach (GameObject g in Botoes)
        {
            g.SetActive(false);
        }
        switch (meuEstado)
        {
            case FalaAtual.PARTE1:
                Botoes[0].SetActive(true);
                meuTexto.text = Fala1[ManagerGame.Instance.Idm];
                break;
            case FalaAtual.PARTE2:
                Botoes[1].SetActive(true);
                meuTexto.text = Fala2[ManagerGame.Instance.Idm];               
                break;
            case FalaAtual.PARTE3:
                Botoes[2].SetActive(true);
                meuTexto.text = Fala3[ManagerGame.Instance.Idm];
                break;
            case FalaAtual.PARTE4:
                meuTexto.text = Fala4[ManagerGame.Instance.Idm];
                trocarCena();
                break;
        }
    }
    public void Errou()
    {
        source.PlayOneShot(SomAbrir);
        foreach (GameObject g in Botoes)
        {
            g.SetActive(false);
        }
        meuTexto.text = FalaErrou[ManagerGame.Instance.Idm];
        StoryEvents.ProibidoEntradaAlter = true;
        terminou = true;
    }
    void trocarCena()
    {
        ManagerGame.Instance.SceneToLoad = CenaTroca;
        PlayerStatus.NextHeroPosition = PosicaoTroca;
        PlayerStatus.ProximaAnimacao = AnimacaoTroca;
        GameObject.FindWithTag("MainCamera").GetComponent<Diretor>().TrocarACena();
    }
}

