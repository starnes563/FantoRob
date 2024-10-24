using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirataChave : NPCBattle
{
    public GameObject BotaoEntregarEsfiha;
    public Dialogo FalaPedirEsfiha;
    public Dialogo FalaAgradecerEsfiha;
    public Dialogo FalaAgradecerEsfihaSChave;
    bool recebeu;
    public AudioClip SomDarChave;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        CaixaDialogo = GameObject.FindWithTag("MainCamera").transform.GetChild(0).GetComponent<CaixaDialogo>();
        if(!StoryEvents.ChaveDespensa)
        {
            TextoFala = FalaPedirEsfiha;
        }
        else
        {
            TextoFala = FalaAgradecerEsfihaSChave;
        }
        if (TextoFala != null)
        {
            TextoFala.LerOTexto(ManagerGame.Instance.Idm);
            setencas = TextoFala.Sentencas.Count;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(recebeu && !CaixaDialogo.DialogoDigitando)
        {
            recebeu = false;
            GetComponent<AudioSource>().PlayOneShot(SomDarChave);
        }
    }
    public override void Falar(Walk walk)
    {
        walk.PararDeAndar();
        CaixaDialogo.ReceberDialogo(TextoFala);
        if (StoryEvents.Esfihas) { BotaoEntregarEsfiha.gameObject.SetActive(true); }
    }
    public void ReceberEsfiha()
    {
        BotaoEntregarEsfiha.gameObject.SetActive(false);
        FalaAgradecerEsfiha.LerOTexto(ManagerGame.Instance.Idm);
        StoryEvents.Esfihas = false;
        StoryEvents.ChaveDespensa = true;
        CaixaDialogo.ReceberDialogo(FalaAgradecerEsfiha);
        recebeu = true;
        TextoFala = FalaAgradecerEsfihaSChave;
        TextoFala.LerOTexto(ManagerGame.Instance.Idm);
        setencas = TextoFala.Sentencas.Count;
        recebeu = true;
    }
}
