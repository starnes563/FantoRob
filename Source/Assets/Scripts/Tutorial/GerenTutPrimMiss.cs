using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GerenTutPrimMiss : MonoBehaviour
{
    public GameObject CaixaAcao;
    public BotaoDeAtaque BTAtaque1;
    bool mostrou = false;
    public SequenciaCena Director;
    public PlayableAsset Playable;
    bool ativado = false;
    public int ID;   
    // Update is called once per frame
    private void Start()
    {
        if(StoryEvents.TutorialPrimeiraMissao && StoryEvents.contTutPrimMiss ==ID)
        {
            ativado = true;           
            if (StoryEvents.contTutPrimMiss > 4) { StoryEvents.TutorialPrimeiraMissao = false; }
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
    void Update()
    {
        if (CaixaAcao.activeSelf && !mostrou && ativado)
        {
            Iniciar();
        }
    }
    public void Iniciar()
    {
        mostrou = true;
        Director.Começar(Playable);
    }
    public void MostrarAtacar()
    {
        CaixaAcao.transform.GetChild(0).GetComponent<BtAtaque>().Clicar();
    }
    public void MostrarAtaque()
    {
        BTAtaque1.Mostrar();
    }
    public void PassarNumero()
    {
        StoryEvents.contTutPrimMiss++; ;
    }
}
