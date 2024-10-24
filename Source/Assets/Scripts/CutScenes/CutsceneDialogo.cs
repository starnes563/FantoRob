using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class CutsceneDialogo : MonoBehaviour
{
    [HideInInspector]
    public Dialogo MeuDialogo;
    [HideInInspector]
    public Queue<string> sentences;
    public Text Nome;
    public Text CaixaDeDialogo;
    public static int idioma;
    [HideInInspector]
    public bool DialogoDigitando = false;
    private IEnumerator coroutine;
    [HideInInspector]
    public bool ativo = false;
    private GameObject ImagemAtual;
    public AudioSource AudioSource;
    public AudioClip SomTexto;
    [HideInInspector]
    public SequenciaCena Director;
    [HideInInspector]
    public int controleCena;
    [HideInInspector]
    public bool PodeContinuar = false;
    float contador;
    bool completa = false;
    string FraseCompleta;
    public void Awake()
    {
        idioma = ManagerGame.Instance.Idm;
    }
    public void DialogoCut(Dialogo novoDialogo, SequenciaCena dit)
    {
        PodeContinuar = false;
        Director = dit;
        controleCena = 0;
        MeuDialogo = novoDialogo;
        MeuDialogo.LerOTexto(idioma);
        sentences = new Queue<string>();
        sentences.Clear();
        foreach (string frase in MeuDialogo.Sentencas)
        {
            sentences.Enqueue(frase);
        }
        completa = false;
        // Iniciar();       
    }
    public void Iniciar()
    {
        this.gameObject.SetActive(true);
        DisplayNextSetence();
        contador = 0;
    }
    private void Update()
    {
        contador += Time.deltaTime;
        if (gameObject.activeSelf && Input.GetButtonDown("Fire1"))
        {
            if (sentences.Count > 0)
            {
                if (controleCena < Director.ProximoPausaDial)
                {
                    if (contador >= 0.75f) { contador = 0f; DisplayNextSetence(); }
                    else if (!completa) { completa = true; completaSenteca(); }
                }
                else if (controleCena == Director.ProximoPausaDial)
                {
                    PodeContinuar = true;
                }
            }
            else
            {
                PodeContinuar = true;
            }
        }

    }
    public void DisplayNextSetence()
    {

        if (sentences.Count == 0)
        {
            return;
        }
        else
        {
            string proximostring = sentences.Dequeue();
            int tamanho = proximostring.Length;
            int quebra = proximostring.IndexOf(":");
            string proximoNome = proximostring.Substring(0, quebra);
            string proximafrase = proximostring.Substring(quebra + 1, tamanho - (quebra + 1));
            FraseCompleta = proximafrase;
            Nome.text = proximoNome;
            if (ImagemAtual != null)
            {
                Destroy(ImagemAtual);
            }
            if (MeuDialogo.Sprites.ContainsKey(proximoNome))
            {
                ImagemAtual = Instantiate(MeuDialogo.Sprites[proximoNome], transform);
            }
            DialogoDigitando = true;
            DigitarNaTela(proximafrase);
        }
    }
    public void DigitarNaTela(string frase)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = digitaDialogo(frase);
        StartCoroutine(coroutine);
    }
    private IEnumerator digitaDialogo(string frase)
    {
        controleCena++;
        completa = false;
        DialogoDigitando = true;
        CaixaDeDialogo.text = "";
        foreach (char letra in frase.ToCharArray())
        {
            AudioSource.Stop();
            CaixaDeDialogo.text += letra;
            AudioSource.PlayOneShot(SomTexto);

            yield return new WaitForSeconds(0.03f);
        }
        DialogoDigitando = false;

        if (sentences.Count == 0)
        {
            ativo = true;
        }
    }
    void completaSenteca()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        CaixaDeDialogo.text = FraseCompleta;
        AudioSource.PlayOneShot(SomTexto);
    }
}
