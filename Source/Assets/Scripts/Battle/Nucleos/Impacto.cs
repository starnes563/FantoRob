using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Impacto
{
    public GameObject BotaoTrancar;
    [HideInInspector]
    public bool Ativado = false;
    [HideInInspector]
    public enum Modo
    {
        ATAQUE,
        DEFESA,
    }
    [HideInInspector]
    public Modo ModoAtual;
    public GameObject SimAtaque;
    public GameObject SimDefesa;
    public enum Tipo
    {
        JOGADOR,
        RIVAL,
    }
    public Tipo MeuTipo;
    public bool Trancado = false;
    public string NomesTrancar;
    public string NomesDestrancar;
    WeaponMethods WeaponMet;
    // Start is called before the first frame update
    public void Ativar(Tipo tp, BattleManager bm, UIFisico ui, WeaponMethods wp)
    {        
        WeaponMet = wp;
        //ativar
        MeuTipo = tp;
        Ativado = true;
        //randomizar modo
        int r = Random.Range(0, 2);
        switch (r)
        {
            case 0:
                ModoAtual = Modo.DEFESA;
                break;
            case 1:
                ModoAtual = Modo.ATAQUE;
                break;
        }
        if (MeuTipo == Tipo.JOGADOR)
        {
            ui.ZerarUI();
            SimAtaque = ui.SinalAtaque;
            SimDefesa = ui.SinalDefesa;
            SimDefesa.SetActive(false);
            SimAtaque.SetActive(false);
            BotaoTrancar = ui.BotaoTrancar;
            BotaoTrancar.SetActive(true);
            bm.JogadorFimTurno += Trocar;
            NomesDestrancar = ui.NomesDestrancar[ManagerGame.Instance.Idm];
            NomesTrancar = ui.NomesTrancar[ManagerGame.Instance.Idm];
            BotaoTrancar.GetComponent<Button>().onClick.RemoveAllListeners();
            BotaoTrancar.GetComponent<Button>().onClick.AddListener(Trancar);
            BotaoTrancar.transform.GetChild(0).GetComponent<Text>().text = NomesTrancar;
            BotaoTrancar.transform.GetChild(1).GetComponent<Text>().text = NomesTrancar;
        }
        else if (MeuTipo == Tipo.RIVAL)
        {
            bm.RivalFimTurno += Trocar;
        }
        if(Trancado)
        {
            Destrancar();
            r = Random.Range(0, 2);
            switch (r)
            {
                case 0:
                    ModoAtual = Modo.DEFESA;
                    break;
                case 1:
                    ModoAtual = Modo.ATAQUE;
                    break;

            }
            Trocar();
        }
        Trocar();
    }
    public float CalculaDano(float dano)
    {
        float dn = dano;
        if (Ativado)
        {
            if (Ativado && ModoAtual == Modo.DEFESA)
            {
                dn *= Random.Range(0.55f, 0.9f);
            }
            else if (Trancado && ModoAtual == Modo.ATAQUE)
            {
                dn *= Random.Range(1.15f, 1.3f);
            }
        }
        return dn;
    }
    public float CalculaAtaque(float ataque)
    {
        float at = ataque;
        if (Ativado)
        {
            if (Ativado && ModoAtual == Modo.ATAQUE)
            {
                at *= Random.Range(1.15f, 1.3f);
            }
            else if (Trancado && ModoAtual == Modo.DEFESA)
            {
                at *= Random.Range(0.55f, 0.9f);
            }
        }
        return at;
    }
    void Trocar()
    {
        if (Ativado)
        {
            if (!Trancado)
            {               
                switch (ModoAtual)
                {
                    case Modo.DEFESA:
                        if (MeuTipo == Tipo.JOGADOR)
                        {
                            SimAtaque.SetActive(true);
                            SimDefesa.SetActive(false);
                        }
                        ModoAtual = Modo.ATAQUE;
                        break;
                    case Modo.ATAQUE:
                        if (MeuTipo == Tipo.JOGADOR)
                        {
                            SimAtaque.SetActive(false);
                            SimDefesa.SetActive(true);
                        }
                        ModoAtual = Modo.DEFESA;
                        break;
                }
            }
            else
            {
                if (Random.Range(0, 101) > 65)
                {
                    Destrancar();
                    int r = Random.Range(0, 2);
                    switch (r)
                    {
                        case 0:
                            ModoAtual = Modo.DEFESA;
                            break;
                        case 1:
                            ModoAtual = Modo.ATAQUE;
                            break;

                    }
                    Trocar();
                    if (MeuTipo == Tipo.JOGADOR)
                    {
                        SomFantoRob.SomNegado();
                    }
                }
            }
        }
    }
    public void Trancar()
    {
        if (!Trancado)
        {
            WeaponMet.AnimacaoAtivarArma();
            Trancado = true;
            if (MeuTipo == Tipo.JOGADOR)
            {
                BotaoTrancar.transform.GetChild(0).GetComponent<Text>().text = NomesDestrancar;
                BotaoTrancar.transform.GetChild(1).GetComponent<Text>().text = NomesDestrancar;
                SonsDoMenu.Confirmar();
                ManagerGame.Instance.UsarNF(1);
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
    public void Destrancar()
    {
        WeaponMet.DesativarAnimArma();
        Trancado = false;
        if (MeuTipo == Tipo.JOGADOR)
        {
            BotaoTrancar.transform.GetChild(0).GetComponent<Text>().text = NomesTrancar;
            BotaoTrancar.transform.GetChild(1).GetComponent<Text>().text = NomesTrancar;
        }
    }
    public void Limpar(BattleManager bm, Tipo tp)
    {
        if (Ativado)
        {
            if (tp == Tipo.JOGADOR)
        {

            bm.JogadorFimTurno -= Trocar;
        }
        else if (tp == Tipo.RIVAL)
        {

            bm.RivalFimTurno -= Trocar;
        }
        
            Ativado = false;
        }
    }
}
