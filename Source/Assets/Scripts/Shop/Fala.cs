using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fala : MonoBehaviour
{
    public Text Nome;
    public Text CaixaDeDialogo;

    public bool DialogoDigitando = false;
    private IEnumerator coroutine;
    [HideInInspector]
    public bool ativo = false;
    private GameObject ImagemAtual;
    public AudioSource AudioSource;
    public AudioClip SomTexto;
    private float contador = 0;
    private bool at = false;
    // Start is called before the first frame update
    public void DigitarNaTela(string frase, string nome)
    {
        at = true;
        contador = 0;
        Nome.text = nome;
        DialogoDigitando = true;
        this.gameObject.SetActive(true);
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        coroutine = digitaDialogo(frase);
        StartCoroutine(coroutine);
    }
    private IEnumerator digitaDialogo(string frase)
    {
        CaixaDeDialogo.text = "";
        foreach (char letra in frase.ToCharArray())
        {
            AudioSource.Stop();
            CaixaDeDialogo.text += letra;
            AudioSource.PlayOneShot(SomTexto);

            yield return new WaitForSeconds(0.03f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (at)
        {
            if (gameObject.activeSelf && !DialogoDigitando)
            {
                at = false;
                ativo = false;
                this.gameObject.SetActive(false);
            }
            contador += Time.deltaTime;
            if (contador >= 2f)
            {
                DialogoDigitando = false;
            }
        }
    }
}

