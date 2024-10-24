using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
public class DialogoCutscene : MonoBehaviour
{
    public Dialogo MeuDialogo;
    public Queue<string> sentences;
    public Text Nome;
    public Text CaixaDeDialogo;
    public int Idioma;
    public static int idioma;
    public int Sentencas;
    // [HideInInspector]
    public bool DialogoDigitando = false;
    private IEnumerator coroutine;
    public bool ativo = false;
    private GameObject ImagemAtual;
    public AudioSource AudioSource;
    public AudioClip SomTexto;
    public PlayableAsset playbale;
    public PlayableDirector Director;
    public void Awake()
    {
        idioma = ManagerGame.Instance.Idm;
    }
    public void DialogoCut(Dialogo novoDialogo)
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

        Iniciar();
    }    
    public void Iniciar()
    {
        this.gameObject.SetActive(true);
        DisplayNextSetence();

    }
    
    private void Update()
    {
        if (!ManagerGame.Instance.EmBatalha)
        {
            if (gameObject.activeSelf && Input.GetButtonDown("Fire1"))
            {
                if (sentences.Count > 0)
                {
                    DisplayNextSetence();
                }
                else if (ativo)
                {
                    ativo = false;
                    if (playbale != null)
                    {
                        ChamarProximaCena();
                    }
                    else
                    {
                        this.gameObject.SetActive(false);
                        if (GameObject.FindWithTag("Player"))
                        {
                            GameObject.FindWithTag("Player").GetComponent<Walk>().CanIWalk = true;
                        }
                    }


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
    private void ChamarProximaCena()
    {
        ManagerGame.Instance.IniciaCustcene();
        Director.Play(playbale);
        playbale = null;
        
        this.gameObject.SetActive(false);
    }
    public void ReceberProximaCena(PlayableAsset cena)
    {
        playbale = cena;
    }
}
