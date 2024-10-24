using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cortante
{
    public GameObject BotaoFrenesi;
    [HideInInspector]
    public bool Ativado = false;
    public string TextosFrenesi;
    public string TextosAcalma;
    [HideInInspector]
    public RobotManager RobotMan;
    [HideInInspector]
    public enum Modo
    {
        FRENESI,
        CALMO,
    }
    [HideInInspector]
    public Modo ModoAtual = Modo.CALMO;
    float multiplicador;
    public enum Tipo
    {
        JOGADOR,
        RIVAL,
    }
    public Tipo MeuTipo;
    WeaponMethods weaponMethods;
    // Start is called before the first frame update
    public void ReceberStatus(RobotManager robot, Tipo tp, UIFisico ui, WeaponMethods wp)
    {
        weaponMethods = wp;
        if(tp == Tipo.JOGADOR)
        {
            ui.ZerarUI();
        }       
        RobotMan = robot;
        Ativado = true;
        MeuTipo = tp;
        BotaoFrenesi = ui.BotoaFrenesi;
        TextosAcalma = ui.TextosAcalma[ManagerGame.Instance.Idm];
        TextosFrenesi = ui.TextosFrenesi[ManagerGame.Instance.Idm];
        Ativar();
        multiplicador = 1;
        BotaoFrenesi.transform.GetChild(0).GetComponent<Text>().text = TextosFrenesi;
        BotaoFrenesi.transform.GetChild(1).GetComponent<Text>().text = TextosFrenesi;
        if(ModoAtual == Modo.FRENESI)
        {            
            if (MeuTipo == Tipo.JOGADOR)
            {
                BotaoFrenesi.transform.GetChild(0).GetComponent<Text>().text = TextosFrenesi;
                BotaoFrenesi.transform.GetChild(1).GetComponent<Text>().text = TextosFrenesi;
                SonsDoMenu.Confirmar();
                ManagerGame.Instance.UsarNF(0);
            }
            desaivarFrenesi();
            ModoAtual = Modo.CALMO;
            multiplicador = 1;
        }
    }
    // Update is called once per frame
    public void Atualiza()
    {
        if (ModoAtual == Modo.FRENESI && Ativado)
        {
            diminuirResistencia();
        }
    }
    public void Ativar()
    {
        if(MeuTipo == Tipo.JOGADOR)
        {
           
            BotaoFrenesi.SetActive(true);
            BotaoFrenesi.GetComponent<Button>().onClick.RemoveAllListeners();
            BotaoFrenesi.GetComponent<Button>().onClick.AddListener(Clicar);
        }       
        Ativado = true;
    }
    public void Clicar()
    {
        if (!RobotMan.broke)
        {
            switch (ModoAtual)
            {
                case Modo.CALMO:
                    if (MeuTipo == Tipo.JOGADOR)
                    {
                        BotaoFrenesi.transform.GetChild(0).GetComponent<Text>().text = TextosAcalma;
                        BotaoFrenesi.transform.GetChild(1).GetComponent<Text>().text = TextosAcalma;
                        SonsDoMenu.Confirmar();
                    }
                    ativarFrenesi();
                    ModoAtual = Modo.FRENESI;
                    break;
                case Modo.FRENESI:
                    if (MeuTipo == Tipo.JOGADOR)
                    {
                        BotaoFrenesi.transform.GetChild(0).GetComponent<Text>().text = TextosFrenesi;
                        BotaoFrenesi.transform.GetChild(1).GetComponent<Text>().text = TextosFrenesi;
                        SonsDoMenu.Confirmar();
                        ManagerGame.Instance.UsarNF(0);
                    }
                    desaivarFrenesi();
                    ModoAtual = Modo.CALMO;
                    multiplicador = 1;
                    break;
            }
        }
        else
        {
            if (MeuTipo == Tipo.JOGADOR)
            {
                SomFantoRob.SomNegado();
            }
        }
    }
    void ativarFrenesi()
    {
        RobotMan.VelocidadeAtual *= 1.4f;
        weaponMethods.AnimacaoAtivarArma();
    }
    void desaivarFrenesi()
    {
        RobotMan.VelocidadeAtual = RobotMan.MyStatus.Velocidade;
        weaponMethods.DesativarAnimArma();
    }
    void diminuirResistencia()
    {
        if (weaponMethods.battleManager.BattleState != BattleManager.BattleStateMachine.PLAYERANIMATION || weaponMethods.battleManager.BattleState != BattleManager.BattleStateMachine.ENEMYANIMATION)
        {
            multiplicador *= 1.000008f;
            RobotMan.ResistenciaAtual -= Time.deltaTime * multiplicador;
            RobotMan.atualizaBarraResistencia();
        }
    }
    public void DesativarQuebrado()
    {
        if (MeuTipo == Tipo.JOGADOR)
        {
            BotaoFrenesi.transform.GetChild(0).GetComponent<Text>().text = TextosFrenesi;
            BotaoFrenesi.transform.GetChild(1).GetComponent<Text>().text = TextosFrenesi;            
        }
        desaivarFrenesi();
        ModoAtual = Modo.CALMO;
        multiplicador = 1;
    }
    public void Desativar()
    {
        if(Ativado)
        {
            Ativado = false;
        }        
    }
}
