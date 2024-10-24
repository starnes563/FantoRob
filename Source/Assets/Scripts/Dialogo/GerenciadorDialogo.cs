using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GerenciadorDialogo : MonoBehaviour
{
    public Dialogo Narrador;
    private Queue<string> sentences;
    public int sentencas;
    public Text CaixaDeDialogo;
    public Text CaixaDeDialogoSombra;        
    
    [HideInInspector]
    public bool DialogoDigitando = false;
    private IEnumerator coroutine;
    // Start is called before the first frame update
    void Awake()
    {
        sentences = new Queue<string>();
        if(Narrador != null) { Narrador.LerOTexto(ManagerGame.Instance.Idm); MontarCenario(); }       
        
    }
    public void DisplayNextSetence()
    {
        if(sentences.Count == 0)
        {            
            return;
        }
        else
        {
            string proximafrase = sentences.Dequeue();            
            DigitarNaTela(proximafrase);
        }
    }
    //posso usar tbm para escrever textos que não estao no Queue para escrever coisas na tela;
    public void DigitarNaTela(string frase)
    {
        if(coroutine != null)
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
        CaixaDeDialogoSombra.text = "";
        foreach(char letra in frase.ToCharArray())
        {
            CaixaDeDialogo.text += letra;
            CaixaDeDialogoSombra.text += letra;
            yield return new WaitForSeconds(0.03f);
        }

        yield return new WaitForSeconds(1f);
        DialogoDigitando = false;
    }   
    //essa funcao eu vou usar quando estiver
    void MontarCenario()
    {
        sentences.Clear();
               
        {
            foreach(string frase in Narrador.Sentencas)
            {                
                sentences.Enqueue(frase);
            }            
        }
        sentencas = sentences.Count;
    }

}
