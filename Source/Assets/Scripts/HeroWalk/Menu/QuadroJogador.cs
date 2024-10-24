using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuadroJogador : MonoBehaviour
{
    //Estrelas
    public List<GameObject> Estrelas = new List<GameObject>();
    //Nivel
    public Text NivelAtual;
    public Text ExperienciaAtual;
    public Text ExperienciaProximo;
    public Slider SliderNivel;
    //Reputação
    public Text ReputacaoAtual;
    public Text FamaAtual;
    public Text FamaProxima;
    public Slider SliderReputacao;
    // dinheiro
    public Text Dinheiro;
    public Text Creditos;
    //nome
    public Text Nome;
    //dias
    public List<Text> DiasTexto;    
    public List<string> Complemento = new List<string>();
    public GameObject PosicEPt;
    public Text Posicao;
    public Text Pontos;
    private void Start()
    {
        Mostrar();
    }
    private void OnEnable()
    {
        Mostrar();
    }    
    public void Mostrar()
    {
        for(int i = 0; i< PlayerStatus.Estrelas; i++)
        {
            Estrelas[i].SetActive(true);
        }
        NivelAtual.text = PlayerStatus.Level.ToString();
        ExperienciaAtual.text = PlayerStatus.Exp.ToString();
        ExperienciaProximo.text = PlayerStatus.nextLevel.ToString();
        SliderNivel.maxValue = PlayerStatus.nextLevel;
        SliderNivel.value = PlayerStatus.Exp;
        ReputacaoAtual.text = PlayerStatus.Reputation.ToString();
        FamaAtual.text = PlayerStatus.Trending.ToString();
        FamaProxima.text = PlayerStatus.nextReputation.ToString();
        SliderReputacao.maxValue = PlayerStatus.nextReputation;
        SliderReputacao.value = PlayerStatus.Reputation;
        Dinheiro.text = PlayerObjects.Fantodin.ToString();
        Creditos.text = PlayerObjects.Creditos.ToString();
        // Nome.text = PlayerStatus.Nome;

        foreach(Text t in DiasTexto)
        {
            if (PlayerStatus.CartaEndosso)
            {
                t.text = PlayerStatus.DaysLeft.ToString() + " " + Complemento[ManagerGame.Instance.Idm];
            }
            else
            {
                t.gameObject.SetActive(false);
            }
        }
        if (PlayerStatus.CartaEndosso)
        {
            PosicEPt.gameObject.SetActive(true);
            Posicao.text = PlayerStatus.Posicao.ToString();
            Pontos.text = PlayerStatus.Pontos.ToString();
        }
        else
        {
            PosicEPt.gameObject.SetActive(false);
        }

            this.gameObject.SetActive(true);
    }
}
