using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class canhao
{
    public GameObject BotaoDescarregar;
    public string textoDescarregar;
    WeaponMethods WPMeth;
    int contadorCombo;
   public int PodeGuardado;
    [HideInInspector]
    public List<GameObject> Slots;
    int slotatual = 0;
    public bool Ativado = false;
    public List<GameObject> Animacoes = new List<GameObject>();
    public enum Tipo
    {
        JOGADOR,
        RIVAL,        
    }
    public Tipo MeuTipo;
    public void Iniciar(WeaponMethods wp, Tipo tp, UIFisico ui)
    {
        WPMeth = wp;
        WPMeth.Combo += Guardar;
        Ativado = true;
        MeuTipo = tp;
        if(MeuTipo == Tipo.JOGADOR)
        {
            ui.ZerarUI();
            BotaoDescarregar = ui.BotaoDescarregar;
            textoDescarregar = ui.textoDescarregar[ManagerGame.Instance.Idm];
            BotaoDescarregar.SetActive(true);
            BotaoDescarregar.transform.GetChild(0).GetComponent<Text>().text = textoDescarregar;
            BotaoDescarregar.transform.GetChild(1).GetComponent<Text>().text = textoDescarregar;
            BotaoDescarregar.GetComponent<Button>().onClick.RemoveAllListeners();
            BotaoDescarregar.GetComponent<Button>().onClick.AddListener(Atirar);
            foreach (GameObject st in ui.Slots)
            {               
                Slots = new List<GameObject>();
                Slots.Add(st);
            }
        }       
        foreach (GameObject an in ui.Animacoes)
        {
            Animacoes.Add(an);
        }
    }
    void Guardar()
    {
        if (Ativado)
        {           
            contadorCombo++;
            if (contadorCombo == 5)
            {
                contadorCombo = 0;               
                if (PodeGuardado <= 3)
                {
                    if (MeuTipo == Tipo.JOGADOR)
                    {
                        Slots[slotatual].SetActive(true);
                    }
                    PodeGuardado++;
                }
            }
        }
    }
    public void Atirar()
    {
        if (PodeGuardado > 0 && Ativado)
        {
            int poder = 10 + (20 * PodeGuardado);
            if (MeuTipo == Tipo.JOGADOR)
            {
                ManagerGame.Instance.UsarNF(4);
                SonsDoMenu.Confirmar();
                WPMeth.battleManager.RivTomaDano();
                WPMeth.AtirarCanhao(Animacoes[0], poder);
                foreach (GameObject s in Slots)
                {
                    s.SetActive(false);
                }
                
            }
            else if (MeuTipo == Tipo.RIVAL)
            {
                WPMeth.battleManager.PLTomaDao();
               WPMeth.AtirarCanhao(Animacoes[1], poder);
            }
            PodeGuardado = 0;
        }
        else
        {
            if (MeuTipo == Tipo.JOGADOR)
            {
                SomFantoRob.SomNegado();
            }
        }
    }
    public void Desativar()
    {
        if (Ativado)
        {
            Ativado = false;
        }
    }
}
