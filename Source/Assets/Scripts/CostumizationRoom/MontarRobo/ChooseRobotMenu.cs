using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseRobotMenu : MonoBehaviour
{
    [HideInInspector]
    public FantoRob MeuFantorob;
    public Text[] Stats = new Text[7];
    public GameObject[] SetaCima = new GameObject[7];
    public GameObject[] SetaBaixa = new GameObject[7];
    public SetaDeTroca[] SetasDetroca = new SetaDeTroca[2];
    public GameObject[] BotoesPartes = new GameObject[5];
    [HideInInspector]
    public int Atual = 0;
    public AudioSource Source;
    public AudioClip SomConfirma;
    public AudioClip SomDesiste;
    public PartMenu MenuParte;
    private int[] StatsAnteriores = new int[7];
    private int[] StatsAtuais = new int[7];
    public Image NucleoElemental;
    public Image NucleoFisico;
    public Image SpriteFantorob;
    public ConfigurarAtaques MenuDeAtaques;
    public GameObject MenuDeBateria;
    public List<GameObject> PartesRoboBt;
    // Start is called before the first frame update
    public void OnEnable()
    {
        Atual = 0;
        Criar(PlayerObjects.RobotsInUse[Atual]);
        foreach (SetaDeTroca sta in SetasDetroca)
        {
            sta.Reiniciar();
        }
    }
    public void Criar(FantoRob fanto)
    {
        foreach(GameObject g in PartesRoboBt)
        {
            g.SetActive(false);
        }
        for(int i =0; i<fanto.NumeroDePartes; i++)
        {
            PartesRoboBt[i].SetActive(true);
        }
        MeuFantorob = fanto;
        Stats[0].text = MeuFantorob.Ataque.ToString();
        Stats[1].text = MeuFantorob.AtaqueElemental.ToString();
        Stats[2].text = MeuFantorob.Velocidade.ToString();
        Stats[3].text = MeuFantorob.Resistencia.ToString();
        Stats[4].text = MeuFantorob.Integridade.ToString();
        Stats[5].text = MeuFantorob.Bateria.ToString();
        Stats[6].text = MeuFantorob.GastoEnergiaTotal.ToString();
        NucleoElemental.sprite = MeuFantorob.SpriteElemento;
        NucleoFisico.sprite = MeuFantorob.SpriteFisico;
        SpriteFantorob.sprite = MeuFantorob.MenuIconeFantorob;
            
        StatsAnteriores[0] = MeuFantorob.Ataque;
        StatsAnteriores[1] = MeuFantorob.AtaqueElemental;
        StatsAnteriores[2] = MeuFantorob.Velocidade;
        StatsAnteriores[3] = MeuFantorob.Resistencia;
        StatsAnteriores[4] = MeuFantorob.Integridade;
        StatsAnteriores[5] = MeuFantorob.Bateria;
        StatsAnteriores[6] = MeuFantorob.GastoEnergiaTotal;

        StatsAtuais[0] = MeuFantorob.Ataque;
        StatsAtuais[1] = MeuFantorob.AtaqueElemental;
        StatsAtuais[2] = MeuFantorob.Velocidade;
        StatsAtuais[3] = MeuFantorob.Resistencia;
        StatsAtuais[4] = MeuFantorob.Integridade;
        StatsAtuais[5] = MeuFantorob.Bateria;
        StatsAtuais[6] = MeuFantorob.GastoEnergiaTotal;

        //apagarsetas
        apagarSetas();
        //acionaosbotoes
        for (int i =0; i<MeuFantorob.NumeroDePartes; i++)
        {
            BotoesPartes[i].SetActive(true);
        }
    }
    public void RemontarRobo()
    {
        MeuFantorob.MontarRobo();
        StatsAtuais[0] = MeuFantorob.Ataque;
        StatsAtuais[1] = MeuFantorob.AtaqueElemental;
        StatsAtuais[2] = MeuFantorob.Velocidade;
        StatsAtuais[3] = MeuFantorob.Resistencia;
        StatsAtuais[4] = MeuFantorob.Integridade;
        StatsAtuais[5] = MeuFantorob.Bateria;
        StatsAtuais[6] = MeuFantorob.GastoEnergiaTotal;
       
        Stats[0].text = MeuFantorob.Ataque.ToString();
        Stats[1].text = MeuFantorob.AtaqueElemental.ToString();
        Stats[2].text = MeuFantorob.Velocidade.ToString();
        Stats[3].text = MeuFantorob.Resistencia.ToString();
        Stats[4].text = MeuFantorob.Integridade.ToString();
        Stats[5].text = MeuFantorob.Bateria.ToString();
        Stats[6].text = MeuFantorob.GastoEnergiaTotal.ToString();

        apagarSetas();
        for (int i =0; i<6;i++)
        {
            if (StatsAnteriores[i] < StatsAtuais[i]) { SetaCima[i].SetActive(true); }
            if (StatsAnteriores[i] > StatsAtuais[i]) { SetaBaixa[i].SetActive(true); }
        }
    }
    void apagarSetas()
    {
        for (int i = 0; i < 6; i++)
        {
            if (SetaBaixa[i].activeSelf) { SetaBaixa[i].SetActive(false); }
            if (SetaCima[i].activeSelf) { SetaCima[i].SetActive(false); }

        }
    }
    public void TrocarFantorob(int troca)
    {
        if(MenuParte.gameObject.activeSelf)
        {
            MenuParte.gameObject.SetActive(false);
        }
        switch(PlayerObjects.RobotsInUse.Count)
        {
            case 1:
                //faz nada os tem 1
                break;
            case 2:
                TocarSomConfirma();
                Atual = troca;
                if (Atual > 1){Atual = 1;}
                Criar(PlayerObjects.RobotsInUse[Atual]);
                switch (Atual)
                {
                    case 0:
                        SetasDetroca[0].valoratual = 1;
                        SetasDetroca[1].valoratual = 1;
                        break;
                    case 1:
                        SetasDetroca[0].valoratual = 0;
                        SetasDetroca[1].valoratual = 0;
                        break;                    
                }
                break;
            case 3:
                TocarSomConfirma();
                Atual = troca;
                Criar(PlayerObjects.RobotsInUse[Atual]);
                switch (Atual)
                {
                    case 0:
                        SetasDetroca[0].valoratual = 2;
                        SetasDetroca[1].valoratual = 1;
                        break;
                    case 1:
                        SetasDetroca[0].valoratual = 0;
                        SetasDetroca[1].valoratual = 2;
                        break;
                    case 2:
                        SetasDetroca[0].valoratual = 1;
                        SetasDetroca[1].valoratual = 0;
                        break;
                }
                break;
        }        
    }
    public void AbrirMenuParte(int minhaparte)
    {
        MenuParte.gameObject.SetActive(false);
        MenuParte.gameObject.SetActive(true);
        MenuParte.Montar(MeuFantorob.RobotPart[minhaparte], minhaparte);
        TocarSomConfirma();
    }
    public void RetirarTudo()
    {
        MenuDeBateria.SetActive(false);
        MenuDeAtaques.Finalizar();
        MenuParte.Concluir();
        MeuFantorob.RetirarTudo();
        TocarSomConfirma();
    }
    public void TocarSomConfirma()
    {
        Source.PlayOneShot(SomConfirma);
    }
    public void TocarSomDesiste()
    {
        Source.PlayOneShot(SomDesiste);
    }
    public void Finalizar()
    {
        MenuDeBateria.SetActive(false);
        MenuDeAtaques.Finalizar();
        MenuParte.Concluir();
        this.gameObject.SetActive(false);
    }
}
