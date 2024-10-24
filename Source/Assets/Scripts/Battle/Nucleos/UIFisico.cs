using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class UIFisico
{
    //cortante
    public GameObject BotoaFrenesi;
    public List<string> TextosFrenesi;
    public List<string> TextosAcalma;
    //impacto
    public GameObject BotaoTrancar;
    public GameObject SinalAtaque;
    public GameObject SinalDefesa;
    public List<string> NomesTrancar;
    public List<string> NomesDestrancar;
    //escudo
    public GameObject BotaoCarregar;
    public Slider SliderEnergia;
    public List<GameObject> SlotsDeEnergia;
    public List<string> TextoCarregar;
    //bolha
    public GameObject BotaoTocarMusica;
    public List<string> TextoTocarMusica;
    public List<Notas> MinhasNotas;
    //cajado
    public GameObject BotaoCarregaDescarrega;
    public List<string> TextoCarrega;
    public List<string> TextoDescarrega;
    //canhao
    public GameObject BotaoDescarregar;
    public List<string> textoDescarregar;
    public List<GameObject> Slots;
    public List<GameObject> Animacoes;

    public void ZerarUI()
    {
        //cortante
        BotoaFrenesi.SetActive(false);
        //impacto
        BotaoTrancar.SetActive(false);
        SinalAtaque.SetActive(false);
        SinalDefesa.SetActive(false);
        //escudo
        BotaoCarregar.SetActive(false);
        SliderEnergia.value = 0;
        SliderEnergia.gameObject.SetActive(false);
        foreach(GameObject g in SlotsDeEnergia)
        {
            g.SetActive(false);
        }
        //bolha
        BotaoTocarMusica.SetActive(false);
        foreach(Notas n in MinhasNotas)
        {
            n.Apagartudo();
        }
        //cajado
        BotaoCarregaDescarrega.SetActive(false);
        //canhao
        BotaoDescarregar.SetActive(false);
        foreach (GameObject g in Slots)
        {
            g.SetActive(false);
        }

    }
}
