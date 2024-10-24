using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaixaDialogo : MonoBehaviour
{
    [HideInInspector]
    public Dialogo MeuDialogo;
    public Queue<string> sentences;
    public Text Nome;
    public Text CaixaDeDialogo;
    public int Idioma;
    public static int idioma;
    [HideInInspector]
    public int Sentencas;
    [HideInInspector]
    public bool DialogoDigitando = false;
    private IEnumerator coroutine;
    [HideInInspector]
    public bool ativo = false;
    private GameObject ImagemAtual;
    public AudioSource AudioSource;
    public AudioClip SomTexto;
    bool Cutscene = false;
    float contador;
    bool completa = false;
    string FraseCompleta;
    public void Awake()
    {
        idioma = ManagerGame.Instance.Idm;
    }
    public void DialogoCutscene(Dialogo novoDialogo)
    {
        MeuDialogo = novoDialogo;
        MeuDialogo.LerOTexto(Idioma);
        sentences = new Queue<string>();
        sentences.Clear();
        foreach (string frase in MeuDialogo.Sentencas)
        {
            sentences.Enqueue(frase);
        }
        Sentencas = sentences.Count;
        Cutscene = true;
        Iniciar();
    }
    public void ReceberDialogo(Dialogo novodialogo)
    {
        MeuDialogo = novodialogo;
        sentences = new Queue<string>();
        sentences.Clear();
        foreach (string frase in MeuDialogo.Sentencas)
        {
            sentences.Enqueue(frase);
        }
        Sentencas = sentences.Count;
        Cutscene = false;
        Iniciar();
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
        if (gameObject.activeSelf && Input.GetButtonDown("Fire1") && sentences != null && !ManagerGame.Instance.Transitando && !ManagerGame.Instance.EmBatalha)
        {
            if (sentences.Count > 0)
            {
                if (contador >= 0.75f) { DisplayNextSetence(); contador = 0; }
                else if (!completa) { completa = true; completaSenteca(); }
            }
            else if (ativo)
            {
                ativo = false;
                this.gameObject.SetActive(false);
                if (!Cutscene)
                {
                    GameObject.FindWithTag("Player").GetComponent<Walk>().CanIWalk = true;
                }
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
    //posso usar tbm para escrever textos que não estao no Queue para escrever coisas na tela;
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
        DialogoDigitando = true;
        CaixaDeDialogo.text = "";
        completa = false;
        foreach (char letra in frase.ToCharArray())
        {
            AudioSource.Stop();
            CaixaDeDialogo.text += letra;
            AudioSource.PlayOneShot(SomTexto);

            yield return new WaitForSeconds(0.03f);
        }
        DialogoDigitando = false;
        if (sentences != null)
        {
            if (sentences.Count == 0)
            {
                ativo = true;
            }
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
