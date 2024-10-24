using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuadroRobo : MonoBehaviour
{
    public Text Ataque;
    public Text AtaqueEsp;
    public Text Velocidade;
    public Text Resistencia;
    public Text IntAtual;
    public Text IntTotal;
    public Slider SliderIntegridade;
    public Text BatAtual;
    public Text BatTotal;
    public Slider SliderBateria;
    public Text Nome;
    public Image SpriteRobo;
    public Image SpriteElemnt;
    public Image SpriteFisic;
    public Text Gasto;
    public GameObject Spy;
    public GameObject Keylogger;
    public GameObject Trojan;
    public GameObject Ranson;
    public GameObject Worm;
    public GameObject Virus;
    public void Mostrar(FantoRob status)
    {
        Gasto.text = status.GastoEnergiaTotal.ToString();
        Ataque.text = status.Ataque.ToString();
        AtaqueEsp.text = status.AtaqueElemental.ToString();
        Velocidade.text = status.Velocidade.ToString();
        Resistencia.text = status.Resistencia.ToString();
        IntAtual.text = status.IntegridadeAtual.ToString();
        IntTotal.text = status.Integridade.ToString();
        //slider integridade
        SliderIntegridade.maxValue = status.Integridade;
        SliderIntegridade.value = status.IntegridadeAtual;
        BatAtual.text = status.BateriaAtual.ToString();
        BatTotal.text = status.Bateria.ToString();
        //sliderBateria
        SliderBateria.maxValue = status.Bateria;
        SliderBateria.value = status.BateriaAtual;
        Nome.text = status.Nome.ToString();
        if(SpriteRobo != null) { SpriteRobo.sprite = status.MenuIconeFantorob; }
        if (SpriteElemnt != null) { SpriteElemnt.sprite = status.SpriteElemento; }
        if (SpriteFisic != null) { SpriteFisic.sprite = status.Fisico.MySprite; }      
        if (status.Spy) { Spy.SetActive(true); }
        if (status.Keylogger) { Keylogger.SetActive(true); }
        if (status.Trojan) { Trojan.SetActive(true); }
        if (status.Ranson) { Ranson.SetActive(true); }
        if (status.Worm) { Worm.SetActive(true); }
        if (status.Virus) { Virus.SetActive(true); }

        Ataque.gameObject.SetActive(true);
        AtaqueEsp.gameObject.SetActive(true);
        Velocidade.gameObject.SetActive(true);
        Resistencia.gameObject.SetActive(true);
        IntAtual.gameObject.SetActive(true);
        IntTotal.gameObject.SetActive(true);
        BatAtual.gameObject.SetActive(true);
        BatTotal.gameObject.SetActive(true);
        Nome.gameObject.SetActive(true);
        if (SpriteRobo != null) { SpriteRobo.gameObject.SetActive(true); }
        if (SpriteElemnt != null) { SpriteElemnt.gameObject.SetActive(true); }
        if (SpriteFisic != null) { SpriteFisic.gameObject.SetActive(true); }       
        this.gameObject.SetActive(true);
    }
    public void Esconder()
    {
        Ataque.gameObject.SetActive(false);
        AtaqueEsp.gameObject.SetActive(false);
        Velocidade.gameObject.SetActive(false);
        Resistencia.gameObject.SetActive(false);
        IntAtual.gameObject.SetActive(false);
        IntTotal.gameObject.SetActive(false);
        BatAtual.gameObject.SetActive(false);
        BatTotal.gameObject.SetActive(false);
        Nome.gameObject.SetActive(false);
        if (SpriteRobo != null) { SpriteRobo.gameObject.SetActive(false); }
        if (SpriteElemnt != null) { SpriteElemnt.gameObject.SetActive(false); }
        if (SpriteFisic != null) { SpriteFisic.gameObject.SetActive(false); }              
         Spy.SetActive(false); 
         Keylogger.SetActive(false); 
        Trojan.SetActive(false);
        Ranson.SetActive(false); 
        Worm.SetActive(false); 
        Virus.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
