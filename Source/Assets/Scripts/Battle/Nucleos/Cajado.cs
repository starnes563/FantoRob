using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cajado
{
    public GameObject BotaoCarregaDescarrega;
    public string TextoCarrega;
    public string TextoDescarrega;
    public enum Modo
    {
        MODOCARREGA,
        MODODESCARREGA,
    }
    public Modo ModoAtual;
    public float PoderCarreagado;
    public Slider BarraDePoder;
   public bool ativado = false;
    RobotManager MyRobot;
    public enum Tipo
    {
        JOGADOR,
        RIVAL,
    }
    public Tipo MeuTipo;
    WeaponMethods weaponMethods;
    // Start is called before the first frame update
    public void Ativar(RobotManager robo, Tipo tp, UIFisico UI, WeaponMethods wp)
    {
        MyRobot = robo;
        MeuTipo = tp;
        ModoAtual = Modo.MODOCARREGA;
        weaponMethods = wp;
        ativado = true;
        if(MeuTipo == Tipo.JOGADOR)
        {
            UI.ZerarUI();
            TextoCarrega = UI.TextoCarrega[ManagerGame.Instance.Idm];
            TextoDescarrega = UI.TextoDescarrega[ManagerGame.Instance.Idm];
            BotaoCarregaDescarrega = UI.BotaoCarregaDescarrega;
            BotaoCarregaDescarrega.SetActive(true);
            BotaoCarregaDescarrega.transform.GetChild(0).GetComponent<Text>().text = TextoDescarrega;
            BotaoCarregaDescarrega.transform.GetChild(1).GetComponent<Text>().text = TextoDescarrega;
            BotaoCarregaDescarrega = UI.BotaoCarregaDescarrega;
            BarraDePoder = UI.SliderEnergia;
            BarraDePoder.gameObject.SetActive(true);
            BotaoCarregaDescarrega.GetComponent<Button>().onClick.RemoveAllListeners();
            BotaoCarregaDescarrega.GetComponent<Button>().onClick.AddListener(Trocar);
        }
    }
    public void Trocar()
    {
        switch(ModoAtual)
        {
            case Modo.MODOCARREGA:
                if(PoderCarreagado>0)
                {
                    weaponMethods.AnimacaoAtivarArma();
                    ModoAtual = Modo.MODODESCARREGA;
                    MyRobot.VelocidadeAtual *= 1.5f;
                    if (MeuTipo == Tipo.JOGADOR)
                    {
                        SonsDoMenu.Confirmar();
                        BotaoCarregaDescarrega.transform.GetChild(0).GetComponent<Text>().text = TextoCarrega;
                        BotaoCarregaDescarrega.transform.GetChild(1).GetComponent<Text>().text = TextoCarrega;
                    }
                }               
                break;
            case Modo.MODODESCARREGA:
                weaponMethods.DesativarAnimArma();
                ModoAtual = Modo.MODOCARREGA;
                MyRobot.VelocidadeAtual = MyRobot.MyStatus.Velocidade;
                if (MeuTipo == Tipo.JOGADOR)
                {
                    ManagerGame.Instance.UsarNF(5);
                    BotaoCarregaDescarrega.transform.GetChild(0).GetComponent<Text>().text = TextoDescarrega;
                    BotaoCarregaDescarrega.transform.GetChild(1).GetComponent<Text>().text = TextoDescarrega;
                }
                break;
        }
    }
    public float Agir(float dano, int valortotal)
    {
        float dn = dano;
        if(ativado)
        {
            switch (ModoAtual)
            {
                case Modo.MODOCARREGA:                   
                    PoderCarreagado += (dano / valortotal)*50f;                   
                    if (PoderCarreagado > 100) { PoderCarreagado = 100; }
                    weaponMethods.AtualizaPoderCajado();
                    dn *= 1 - 0.2f;
                    break;
                case Modo.MODODESCARREGA:                  
                    PoderCarreagado -= (dano / valortotal)*35f;                    
                    if (PoderCarreagado < 0) { PoderCarreagado = 0; }
                    weaponMethods.AtualizaPoderCajado();
                    dn *= 1 + 0.3f;
                    break;
            }
        }
        return dn;       
    }
    public IEnumerator AtualizaBarraDePoder()
    {
        if (MeuTipo == Tipo.JOGADOR)
        {
            BarraDePoder.value = PoderCarreagado;                   
        }
        if(PoderCarreagado <= 0 && ModoAtual == Modo.MODODESCARREGA)
        {
           Trocar();
        }
        yield return null;
    }
    public void Desativar()
    {
        if (ativado)
        {
            ativado = false;
        }
    }
}
