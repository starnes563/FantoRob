using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotaoSaveSystem : MonoBehaviour
{
    public Text Dinheiro;
    public Text TempoDeJogo;
    public List<GameObject> Estrelas;
    public GameObject ExisteArquivo;
    public GameObject NaoExisteArquivo;
    public Image ImagemFundo;
    public Sprite BtSelecionado;
    public Sprite BtDesselecionado;
    public AudioSource Source;
    public AudioClip SomSeleciona;
    public AudioClip SomConfirma;
    public MenuSaveSystem Menu;
    [HideInInspector]
    public int ID;
    bool existo;
    enum Estado
    {
        SELECIONADO,
        DESSELECIONADO,
    }
    Estado MeuEstado = Estado.DESSELECIONADO;
    public enum Tipo
    {
        NOVOJOGO,
        CARREGAR,
        SALVAR,
    }
    public Tipo MeuTipo;
    public void ArquivoExistente(int din, float tempo,int stars)
    {
        ExisteArquivo.SetActive(true);
        NaoExisteArquivo.SetActive(false);
        Dinheiro.text = din.ToString();
        TempoDeJogo.text = tempo.ToString("hh':'mm");
        for (int e = 0; e < stars; e++)
        {
            Estrelas[e].SetActive(true);
        }
        existo = true;
    }
    public void ArquivoInexistente()
    {
        ExisteArquivo.SetActive(false);
        NaoExisteArquivo.SetActive(true);
        existo = false;
    }

    public void Clicou()
    {
        switch (MeuEstado)
        {
            case Estado.SELECIONADO:                
                Selecionado();                
                break;
            case Estado.DESSELECIONADO:
                Menu.DesselecionaTudo();
                MeuEstado = Estado.SELECIONADO;
                Source.PlayOneShot(SomSeleciona);
                ImagemFundo.sprite = BtSelecionado;
                break;
        }
    }
    public void Desseleciona()
    {
        MeuEstado = Estado.DESSELECIONADO;
        ImagemFundo.sprite = BtDesselecionado;
    }
    void Selecionado()
    {
        switch(MeuTipo)
        {
            case Tipo.NOVOJOGO:
                Source.PlayOneShot(SomConfirma);
                ManagerGame.Instance.ActualSavePath = ID;
                if (!existo)
                {
                    Menu.MenuInicial.Escolher();
                }
                else
                {
                    Menu.MostrarConfirmacao();
                }
                break;
            case Tipo.CARREGAR:
               
                if (!existo)
                {
                    Source.PlayOneShot(SomSeleciona);
                }
                else
                {
                    Source.PlayOneShot(SomConfirma);
                    ManagerGame.Instance.ActualSavePath = ID;
                    StartCoroutine(ManagerGame.Instance.Load());
                }
                break;
            case Tipo.SALVAR:               
                ManagerGame.Instance.ActualSavePath = ID;
                if (!existo)
                {
                    Menu.AcionarSaveSystem.Salvar();
                }
                else
                {
                    Menu.MostrarConfirmacao();
                }
                break;
        }
       
        
    }
  
}
