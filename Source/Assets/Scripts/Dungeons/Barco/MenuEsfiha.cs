using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuEsfiha : MonoBehaviour
{   
    public List<string> Fala1;
    public List<string> Fala2;
    public List<string> Fala3;
    public List<string> Fala4;
    public List<string> FalaErrou;
    public List<string> FalaJaTem;
    public List<GameObject> Botoes;
    public GameObject Pacote;
    public AudioSource Source;
    public Text MeuTexto;
    enum FalaAtual
    {
        PARTE1,
        PARTE2,
        PARTE3,
        PARTE4,
    }
    FalaAtual meuEstado;   
    bool terminou = false;
    public AudioClip SomAbrir;   
    // Start is called before the first frame update
    void Start()
    {
                
    }
    private void OnEnable()
    {
        foreach (GameObject g in Botoes)
        {
            g.SetActive(false);
        }
        Source.PlayOneShot(SomAbrir);
        if (StoryEvents.Esfihas)
        {
            meuEstado = FalaAtual.PARTE4;
            JaTemEsfiha();
        }
        else if(StoryEvents.FalhouEsfihas)
        {
            meuEstado = FalaAtual.PARTE4;
            Errou();
        }
        else if (!StoryEvents.FalhouEsfihas && !StoryEvents.Esfihas)
        {
            meuEstado = FalaAtual.PARTE1;
            mostrarProximo();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (terminou && Input.GetButtonDown("Fire1"))
        {            
            GameObject.FindWithTag("Player").GetComponent<Walk>().LiberarAndar();
            terminou = false;
            this.gameObject.SetActive(false);
        }
    }
    public void Acertou()
    {
        Source.PlayOneShot(SomAbrir);
        switch (meuEstado)
        {
            case FalaAtual.PARTE1:
                mostrarProximo();
                
                break;
            case FalaAtual.PARTE2:
                mostrarProximo();
                meuEstado = FalaAtual.PARTE3;
                break;
            case FalaAtual.PARTE3:
                mostrarProximo();
                meuEstado = FalaAtual.PARTE4;
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
                MeuTexto.text = Fala1[ManagerGame.Instance.Idm];
                meuEstado = FalaAtual.PARTE2;
                break;
            case FalaAtual.PARTE2:
                Botoes[1].SetActive(true);
                MeuTexto.text = Fala2[ManagerGame.Instance.Idm];
                break;
            case FalaAtual.PARTE3:
                Botoes[2].SetActive(true);
                MeuTexto.text = Fala3[ManagerGame.Instance.Idm];
                break;
            case FalaAtual.PARTE4:
                MeuTexto.text = Fala4[ManagerGame.Instance.Idm];
                entregarEsfiha();
                break;
        }
    }
    public void Errou()
    {
        Source.PlayOneShot(SomAbrir);
        foreach (GameObject g in Botoes)
        {
            g.SetActive(false);
        }
        MeuTexto.text = FalaErrou[ManagerGame.Instance.Idm];        
        terminou = true;
        StoryEvents.FalhouEsfihas = true;
    }
    void entregarEsfiha()
    {
        Pacote.SetActive(true);
        StoryEvents.Esfihas = true;
        terminou = true;        
    }
    void JaTemEsfiha()
    {
        MeuTexto.text = FalaJaTem[ManagerGame.Instance.Idm];
        terminou = true;        
    }
}
