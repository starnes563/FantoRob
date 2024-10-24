using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Escudo
{
    public float EnergiaGuardada;
    float energiaTemporaria;
    public int EnergiaSalva;
    public Slider BarraDeEnergia;
    public List<GameObject> SlotsDeEnergia = new List<GameObject>();
    bool[]StatusBuffado = new bool[3];
    int []ContadosStatusBuffado = new int [3];
    BattleManager battleManager;
    RobotManager myRobot;
    public bool Ativado = false;
    public GameObject BotaoCarregar;
    public string TextoCarregar;
    int status = 0;
    public enum Tipo
    {
        JOGADOR,
        RIVAL,
    }
    public Tipo MeuTipo;
    WeaponMethods weaponMethods;   
    public void Iniciar(Tipo tp, RobotManager robo, BattleManager bm, UIFisico ui, WeaponMethods wp)
    {
        MeuTipo = tp;
        myRobot = robo;
        battleManager = bm;
        Ativado = true;
        weaponMethods = wp;
        if(MeuTipo == Tipo.JOGADOR)
        {
            ui.ZerarUI();
            TextoCarregar = ui.TextoCarregar[ManagerGame.Instance.Idm];
            BotaoCarregar = ui.BotaoCarregar;
            BotaoCarregar.SetActive(true);
            BotaoCarregar.transform.GetChild(0).GetComponent<Text>().text = TextoCarregar;
            BotaoCarregar.transform.GetChild(1).GetComponent<Text>().text = TextoCarregar;
            BotaoCarregar.GetComponent<Button>().onClick.RemoveAllListeners();
            BotaoCarregar.GetComponent<Button>().onClick.AddListener(BuffarStatus);
            BarraDeEnergia = ui.SliderEnergia;
            BarraDeEnergia.value = 0;
            BarraDeEnergia.gameObject.SetActive(true);
            foreach (GameObject slot in ui.SlotsDeEnergia)
            {
                SlotsDeEnergia.Add(slot);
            }
        }
    }
    public void GuardarEnergia(float dano, int integridade)
    {
        if (Ativado)
        {
            float perc = (dano / integridade) * 1800;
            float energia = Random.Range(0.8f, 0.98f) * perc;
            EnergiaGuardada += energia;
            if (EnergiaGuardada > 100) { energiaTemporaria += EnergiaGuardada - 100; EnergiaGuardada = 100; }
            weaponMethods.AtualizarBarraEscudo();
        }
    }
     public IEnumerator AtualizaBarraDeEnergia()
    {       
        if (MeuTipo == Tipo.JOGADOR)
        {
            float total = EnergiaGuardada - BarraDeEnergia.value;
            while (BarraDeEnergia.value < EnergiaGuardada)
            {
                BarraDeEnergia.value += Time.deltaTime * total;
                yield return null;
            }
        }
       if(EnergiaGuardada>=100)
        {
            SalvaEnergia();
        }
    }
    void SalvaEnergia()
    {
        if (EnergiaSalva < 2)
        {
            EnergiaSalva++;
            EnergiaGuardada = energiaTemporaria;
            energiaTemporaria = 0;
            if (MeuTipo == Tipo.JOGADOR)
            {
                BarraDeEnergia.value = EnergiaGuardada;
                SlotsDeEnergia[EnergiaSalva - 1].SetActive(true);
            }
        }
    }
    public void BuffarStatus()
    {
        if (Ativado)
        {            
            //verifica se pode buffar status
            foreach (bool st in StatusBuffado)
            {
                if (!st)
                {
                    if (EnergiaSalva > 0)
                    {                        
                        if (MeuTipo==Tipo.JOGADOR)
                        {
                            ManagerGame.Instance.UsarNF(2);
                        }                       
                        //reduz o slot 
                        EnergiaSalva--;
                        //reinicia o status
                        if (status >= 2) { status = 0; }
                        //verifica se o status ja nao esta buffado
                        if (StatusBuffado[status])
                        {
                            while (StatusBuffado[status])
                            {
                                status = Random.Range(0, 3);
                            }
                        }
                        //ativa a bool do status buffado
                        //ativa a void no despecito evento de fim e turno para contar o tempo que o status ficara buffado
                        StatusBuffado[status] = true;
                        ContadosStatusBuffado[status] = 0;
                        weaponMethods.AnimacaoAtivarArma();                      
                        switch (status)
                        {
                            case 0:
                                //velocidade (Random.Range(20, 30) / 100);
                               
                                myRobot.VelocidadeAtual *= 1.5f;
                                
                                switch (MeuTipo)
                                {
                                    case Tipo.JOGADOR:
                                        battleManager.JogadorFimTurno += VelocidadeBuffada;
                                        SlotsDeEnergia[EnergiaSalva].SetActive(false);
                                        SonsDoMenu.Confirmar();
                                        break;
                                    case Tipo.RIVAL:
                                        battleManager.RivalFimTurno += VelocidadeBuffada;
                                        break;
                                }
                                break;
                            case 1:
                                //Ataque                               
                                myRobot.AtaqueAtual *= 1.5f;                               
                                switch (MeuTipo)
                                {
                                    case Tipo.JOGADOR:
                                        battleManager.JogadorFimTurno += AtaqueBuffado;
                                        SlotsDeEnergia[EnergiaSalva].SetActive(false);
                                        SonsDoMenu.Confirmar();
                                        break;
                                    case Tipo.RIVAL:
                                        battleManager.RivalFimTurno += AtaqueBuffado;
                                        break;
                                }
                                break;
                            case 2:
                                //AtaqueEspecal
                                myRobot.AtaqueEnergeticoAtual *= 1.5f;
                                switch (MeuTipo)
                                {
                                    case Tipo.JOGADOR:
                                        battleManager.JogadorFimTurno += AtaqueEspecialBuffado;
                                        SlotsDeEnergia[EnergiaSalva].SetActive(false);
                                        SonsDoMenu.Confirmar();
                                        break;
                                    case Tipo.RIVAL:
                                        battleManager.RivalFimTurno += AtaqueEspecialBuffado;
                                        break;
                                }
                                break;
                        }
                        //passa para o proximo status
                        status++;
                    }
                    else
                    {
                        if (MeuTipo == Tipo.JOGADOR)
                        {
                            SomFantoRob.SomNegado();
                        }
                    }
                    break;
                }
                else
                {
                    if(MeuTipo == Tipo.JOGADOR)
                    {
                        SomFantoRob.SomNegado();
                    }
                }
            }
        }
    }
    void VelocidadeBuffada()
    {
        ContadosStatusBuffado[0]++;        
        if (ContadosStatusBuffado[0]>=3)
        {
            ContadosStatusBuffado[0] = 0;
            StatusBuffado[0] = false;            
            myRobot.VelocidadeAtual = myRobot.MyStatus.Velocidade;
            if(!StatusBuffado[0] && !StatusBuffado[1] && !StatusBuffado[2]) { weaponMethods.DesativarAnimArma(); }
            switch (MeuTipo)
            {
                case Tipo.JOGADOR:
                    battleManager.JogadorFimTurno -= VelocidadeBuffada;
                    break;
                case Tipo.RIVAL:
                    battleManager.RivalFimTurno -= VelocidadeBuffada;
                    break;
            }
        }
    }
    void AtaqueBuffado()
    {
        ContadosStatusBuffado[1]++;        
        if (ContadosStatusBuffado[1] >= 2)
        {
            ContadosStatusBuffado[1] = 0;
            StatusBuffado[1] = false;            
            myRobot.AtaqueAtual = myRobot.MyStatus.Ataque;
            if (!StatusBuffado[0] && !StatusBuffado[1] && !StatusBuffado[2]) { weaponMethods.DesativarAnimArma(); }
            switch (MeuTipo)
            {
                case Tipo.JOGADOR:
                    battleManager.JogadorFimTurno -= AtaqueBuffado;
                    break;
                case Tipo.RIVAL:
                    battleManager.RivalFimTurno -= AtaqueBuffado;
                    break;
            }
        }
    }
    void AtaqueEspecialBuffado()
    {
        ContadosStatusBuffado[2]++;       
        if (ContadosStatusBuffado[2] >= 2)
        {
            ContadosStatusBuffado[2] = 0;
            StatusBuffado[2] = false;           
            myRobot.AtaqueEnergeticoAtual = myRobot.MyStatus.AtaqueEnergetico;
            if (!StatusBuffado[0] && !StatusBuffado[1] && !StatusBuffado[2]) { weaponMethods.DesativarAnimArma(); }
            switch (MeuTipo)
            {
                case Tipo.JOGADOR:
                    battleManager.JogadorFimTurno -= AtaqueEspecialBuffado;
                    break;
                case Tipo.RIVAL:
                    battleManager.RivalFimTurno -= AtaqueEspecialBuffado;
                    break;
            }
        }
    }
    public void LimparArma()
    {
        if (Ativado)
        {
            ContadosStatusBuffado[2] = 0;
            StatusBuffado[2] = false;
            myRobot.AtaqueEnergeticoAtual = myRobot.MyStatus.AtaqueEnergetico;
            if (!StatusBuffado[0] && !StatusBuffado[1] && !StatusBuffado[2]) { weaponMethods.DesativarAnimArma(); }
            switch (MeuTipo)
            {
                case Tipo.JOGADOR:
                    battleManager.JogadorFimTurno -= AtaqueEspecialBuffado;
                    break;
                case Tipo.RIVAL:
                    battleManager.RivalFimTurno -= AtaqueEspecialBuffado;
                    break;
            }
            ContadosStatusBuffado[1] = 0;
            StatusBuffado[1] = false;
            myRobot.AtaqueAtual = myRobot.MyStatus.Ataque;
            if (!StatusBuffado[0] && !StatusBuffado[1] && !StatusBuffado[2]) { weaponMethods.DesativarAnimArma(); }
            switch (MeuTipo)
            {
                case Tipo.JOGADOR:
                    battleManager.JogadorFimTurno -= AtaqueBuffado;
                    break;
                case Tipo.RIVAL:
                    battleManager.RivalFimTurno -= AtaqueBuffado;
                    break;
            }
            ContadosStatusBuffado[0] = 0;
            StatusBuffado[0] = false;
            myRobot.VelocidadeAtual = myRobot.MyStatus.Velocidade;
            if (!StatusBuffado[0] && !StatusBuffado[1] && !StatusBuffado[2]) { weaponMethods.DesativarAnimArma(); }
            switch (MeuTipo)
            {
                case Tipo.JOGADOR:
                    battleManager.JogadorFimTurno -= VelocidadeBuffada;
                    break;
                case Tipo.RIVAL:
                    battleManager.RivalFimTurno -= VelocidadeBuffada;
                    break;
            }
       
            Ativado = false;
        }

    }
}
