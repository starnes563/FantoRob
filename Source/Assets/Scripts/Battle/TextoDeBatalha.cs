using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
public class TextoDeBatalha : MonoBehaviour
{
    public List<TextAsset> TextosBatalha= new List<TextAsset>();
    public bool TextoPadrao = false;
    private int idioma;
    [HideInInspector]
    public List<string> textos = new List<string>();
    private GerenciadorDialogo gerenciadorDialogo;
    // Start is called before the first frame update
    void Awake()
    {
        gerenciadorDialogo = GetComponent<GerenciadorDialogo>();
        idioma = ManagerGame.Instance.Idm;
        LerOTexto();
    }
    void Update()
    {
        if(!TextoPadrao)
        {
            DigitarFrasePadrao();
        }
    }
        
    void LerOTexto()
    {
        textos = TextosBatalha[idioma].text.Split('\n').ToList();        
    }
    void DigitarFrasePadrao()
    {
        if(!gerenciadorDialogo.DialogoDigitando)
        {
            TextoPadrao = true;
           gerenciadorDialogo.DigitarNaTela(textos[5]);
        }
    }  
    

}
