using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LinhaQuadroDesafio : MonoBehaviour
{
    public Text Nome;
    public Image Sprite;
    public Text Dia;
    public Text Posic;
    public Text PontuacaoTotal;
    public Text PontuacaoRecebida;
    public void MostrarParticipante(ParticipanteDEsafio p)
    {
        Nome.gameObject.SetActive(false);
        Sprite.gameObject.SetActive(false);
        Dia.gameObject.SetActive(false);
        Posic.gameObject.SetActive(false);
        PontuacaoTotal.gameObject.SetActive(false);
        PontuacaoRecebida.gameObject.SetActive(false);
        Nome.text = p.Nome;
        Sprite.sprite = p.SpriteParticipante;
        Nome.gameObject.SetActive(true);
        Sprite.gameObject.SetActive(true);
    }
    public void MostrarDia(int dia, int pontrecebida)
    {
        Dia.gameObject.SetActive(true);
        PontuacaoRecebida.gameObject.SetActive(true);
        Dia.text = dia.ToString();
        PontuacaoRecebida.text = pontrecebida.ToString();
    }
    public void MostrarPosicao(ParticipanteDEsafio p)
    {
        Dia.gameObject.SetActive(false);
        PontuacaoRecebida.gameObject.SetActive(false);
        Posic.gameObject.SetActive(true);
        PontuacaoTotal.gameObject.SetActive(true);
        Posic.text = p.PosicaoAtual.ToString();
        PontuacaoTotal.text = p.PontuacaoAtual.ToString();
        Nome.text = p.Nome;
        Sprite.sprite = p.SpriteParticipante;
        Nome.gameObject.SetActive(true);
        Sprite.gameObject.SetActive(true);
    }    
}
