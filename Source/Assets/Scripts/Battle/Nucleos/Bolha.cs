using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bolha
{
    public GameObject BotaoTocarMusica;
    public string TextoTocarMusica;
    public List<Notas> MinhasNotas;
    int notaatual;
    [HideInInspector]
    public bool Ativado = false;
    public enum Tipo
    {
        JOGADOR,
        RIVAL,
    }
    public Tipo MeuTipo;
    BattleManager battleManager;
    RobotManager myRobot;
    int[] ContadosStatusBuffado = new int[3];
    WeaponMethods weaponMethods;
    bool velocidade = false;
    bool ataque  = false;
    bool ataqueespecial = false;
    // Start is called before the first frame update
    public void Ativar(Tipo tp, RobotManager robo, BattleManager bm, UIFisico ui, WeaponMethods wp)
    {       
        weaponMethods = wp;
        MeuTipo = tp;
        myRobot = robo;
        battleManager = bm;
        Ativado = true;
        MinhasNotas = new List<Notas>();
        switch (MeuTipo)
        {
            case Tipo.JOGADOR:
                ui.ZerarUI();
                BotaoTocarMusica = ui.BotaoTocarMusica;
                BotaoTocarMusica.GetComponent<Button>().onClick.AddListener(TocarNotas);
                TextoTocarMusica = ui.TextoTocarMusica[ManagerGame.Instance.Idm];
                battleManager.JogadorFimTurno += PegarVibracao;
                BotaoTocarMusica.SetActive(true);
                BotaoTocarMusica.transform.GetChild(0).GetComponent<Text>().text = TextoTocarMusica;
                BotaoTocarMusica.transform.GetChild(1).GetComponent<Text>().text = TextoTocarMusica;
                foreach (Notas nt in ui.MinhasNotas)
                {
                    MinhasNotas.Add(nt);
                }
                break;
            case Tipo.RIVAL:
                battleManager.RivalFimTurno += PegarVibracao;
                for(int i = 0; i<3;i++)
                {
                    MinhasNotas.Add(new Notas());
                }
                break;
        }
    }
    void PegarVibracao()
    {
        if (Ativado && notaatual < 3)
        {
            switch(MeuTipo)
            {
                case Tipo.JOGADOR:
                    MinhasNotas[notaatual].Mostrar(Random.Range(0, 2), Notas.Tipo.JOGADOR);
                    break;
                case Tipo.RIVAL:
                    MinhasNotas[notaatual].Mostrar(Random.Range(0, 2), Notas.Tipo.RIVAL);
                    break;
            }
            notaatual++;
        }
    }
    public void TocarNotas()
    {
        bool pelomenosuma = false;
        foreach (Notas nt in MinhasNotas)
        {
            if (nt.meuBuff != 9)
            {
                ContadosStatusBuffado[nt.meuBuff] = 0;
                switch (nt.meuBuff)
                {
                    case 0:
                        //velocidade                      
                        myRobot.VelocidadeAtual *= 1.25f;                      
                        velocidade = true;
                        switch (MeuTipo)
                        {
                            case Tipo.JOGADOR:
                                SomFantoRob.TocarSomNota(0);
                                battleManager.JogadorFimTurno += VelocidadeBuffada;
                                break;
                            case Tipo.RIVAL:
                                battleManager.RivalFimTurno += VelocidadeBuffada;
                                break;
                        }
                        break;
                    case 1:
                        //Ataque                       
                        myRobot.AtaqueAtual *= 1.25f;                       
                        ataque = true;
                        switch (MeuTipo)
                        {
                            case Tipo.JOGADOR:
                                SomFantoRob.TocarSomNota(1);
                                battleManager.JogadorFimTurno += AtaqueBuffado;
                                break;
                            case Tipo.RIVAL:
                                battleManager.RivalFimTurno += AtaqueBuffado;
                                break;
                        }
                        break;
                    case 2:
                        //AtaqueEspecal                        
                        myRobot.AtaqueEnergeticoAtual *= 1.25f;                      
                        ataqueespecial = true;
                        switch (MeuTipo)
                        {
                            case Tipo.JOGADOR:
                                SomFantoRob.TocarSomNota(2);
                                battleManager.JogadorFimTurno += AtaqueEspecialBuffado;
                                break;
                            case Tipo.RIVAL:
                                battleManager.RivalFimTurno += AtaqueEspecialBuffado;
                                break;
                        }
                        break;
                }
                nt.Apagartudo();
                notaatual = 0;
                weaponMethods.AnimacaoAtivarArma();
                if(MeuTipo == Tipo.JOGADOR)
                {
                    ManagerGame.Instance.UsarNF(3);
                }
                pelomenosuma = true;
            }
           
        }
        if (!pelomenosuma)
        {
            if (MeuTipo == Tipo.JOGADOR)
            {
                SomFantoRob.SomNegado();
            }
        }
    }
    void VelocidadeBuffada()
    {
        ContadosStatusBuffado[0]++;
        if (ContadosStatusBuffado[0] >= 3)
        {
            ContadosStatusBuffado[0]=0;
            myRobot.VelocidadeAtual = myRobot.MyStatus.Velocidade;  
            if(!velocidade && !ataque && !ataqueespecial) { weaponMethods.DesativarAnimArma(); }
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
            myRobot.AtaqueAtual = myRobot.MyStatus.Ataque;
            if (!velocidade && !ataque && !ataqueespecial) { weaponMethods.DesativarAnimArma(); }
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
        if (ContadosStatusBuffado[2] == 2)
        {
            ContadosStatusBuffado[2] = 0;
            myRobot.AtaqueEnergeticoAtual = myRobot.MyStatus.AtaqueEnergetico;
            if (!velocidade && !ataque && !ataqueespecial) { weaponMethods.DesativarAnimArma(); }
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
    public void Limpar(Tipo tp, BattleManager bm)
    {
        if (Ativado)
        {


            switch (tp)
            {
                case Tipo.JOGADOR:
                    bm.JogadorFimTurno -= PegarVibracao;
                    bm.JogadorFimTurno -= AtaqueBuffado;
                    bm.JogadorFimTurno -= VelocidadeBuffada;
                    bm.JogadorFimTurno -= AtaqueEspecialBuffado;

                    break;
                case Tipo.RIVAL:
                    bm.RivalFimTurno -= PegarVibracao;
                    bm.RivalFimTurno -= VelocidadeBuffada;
                    bm.RivalFimTurno -= AtaqueBuffado;
                    bm.RivalFimTurno -= AtaqueEspecialBuffado;
                    break;
            }
            foreach (Notas nt in MinhasNotas)
            {
                nt.Apagartudo();
                notaatual = 0;
            }
       
            Ativado = false;
        }
    }

}
    [System.Serializable]
public class Notas
{
    public List<GameObject> Nt;
    [HideInInspector]
    public int meuBuff = 9;
    [HideInInspector]
    public enum Tipo
    {
        JOGADOR,
        RIVAL,
    }
    [HideInInspector]
    public Tipo MeuTipo = Tipo.RIVAL;
    public void Mostrar(int i, Tipo mt)
    {        
        MeuTipo = mt;
        if(MeuTipo == Tipo.JOGADOR)
        {
            Nt[i].SetActive(true);
        }       
        meuBuff = i;        
    }
    public void Apagartudo()
    {        
        meuBuff = 9;
        if (MeuTipo == Tipo.JOGADOR)
        {
            foreach (GameObject g in Nt)
            {
                g.SetActive(false);
            }
        }
    }
    public bool PossoTocar()
    {
        bool posso = false;
        if(meuBuff != 9) { posso = true; }
        return posso;
    }

}
