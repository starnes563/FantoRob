using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Batalha : MonoBehaviour
{
    [Range(10, 100)]
    public int Dificuldade;
    public bool TestarPlayerPerder;
    public FantoRob FantoRobPlayer;
    public Weapon FisicoPlayer;
    public bool TestaMovePlayer;    
    public Move MovePlayer;
    public bool TestarRivalPerder;
    public FantoRob FantoRobRival;
    public Weapon FisicoRival;
    public bool TestaMoveRival;   
    public Move MoveRival;
    public int Estrelas;
    [Range(0, 9)]
    public int dificuldadeNPC;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    } 
    public void GerarBatalha(NPCBattle NPC)
    {
        NPC.Robots.Clear();
        NPC.Tipo = 0;
        NPC.Estrelas = PlayerStatus.Estrelas;
        NPC.Nivel = Nivel(NPC.Estrelas);
        NPC.Dificuldade = dificuldadeNPC;
        NPC.Exp = Exp(NPC.Estrelas);
        NPC.Money = GerarDinheiro();
        NPC.Persegue = false;
        //NPC.Velocidade = Random.Range(8, 11);
        //gera time        
        NPC.Trend = 3;
        NPC.Robots.Add(GerarFantorob(FantoRobRival, FisicoRival, MoveRival, TestaMoveRival, false));
        //poe seu time
        PlayerObjects.RobotsInUse.Add(GerarFantorob(FantoRobPlayer, FisicoPlayer, MovePlayer, TestaMovePlayer, true));        
    }
    int Nivel(int estrelas)
    {
        int nivel = 0;
        switch (estrelas)
        {
            case 0:
                nivel = Random.Range(0, 5);
                break;
            case 1:
                nivel = Random.Range(5, 11);
                break;
            case 2:
                nivel = Random.Range(11, 19);
                break;
            case 3:
                nivel = Random.Range(19, 31);
                break;
            case 4:
                nivel = Random.Range(31, 47);
                break;
            case 5:
                nivel = Random.Range(47, 67);
                break;
            case 6:
                nivel = Random.Range(71, 100); ;
                break;
            case 7:
                nivel = Random.Range(100, 152);
                break;
            case 8:
                nivel = Random.Range(152, 10000);
                break;
        }
        return nivel;
    }
    int Exp(int estrelas)
    {
        int e = Random.Range(1, 3);

        if (estrelas >= 5 && estrelas < 7)
        {
            e = Random.Range(2, 4);
        }
        else if (estrelas == 7)
        {
            e = Random.Range(3, 5);
        }
        else if (estrelas >= 8)
        {
            e = Random.Range(4, 6);
        }
        return e;
    }
    int GerarDinheiro()
    {
        int din = 56;
        return din;
    }
    FantoRob GerarFantorob(FantoRob robo, Weapon wp, Move MO, bool testamove,bool player)
    {        
        //testar todos os fantorobs na batalha       
        FantoRob rob = Instantiate(robo);
        rob.Fisico = Instantiate(wp);
        if (testamove)
        {
            rob.Fisico.MovesAtivos.Add(MO);
            rob.Fisico.CarregarAtaques();
        }
        else
        {
            //montar ataques
            if(player)
            {
                PorEmAtivos(rob.MovimentoAmbos, rob.Fisico);
                PorEmAtivos(rob.MovimentoJogador, rob.Fisico);
                PorEmAtivos(rob.Fisico.MovimentosAmbos, rob.Fisico);
                PorEmAtivos(rob.Fisico.MovimentosJogador, rob.Fisico);
            }
            else
            {
                PorEmAtivos(rob.MovimentoAmbos, rob.Fisico);
                PorEmAtivos(rob.MovimentoInimigo, rob.Fisico);
                PorEmAtivos(rob.Fisico.MovimentosAmbos, rob.Fisico);
                PorEmAtivos(rob.Fisico.MovimentosInimigo, rob.Fisico);
            }            
            //adiciona moves aleatorios
            for (int i = 0; i < Random.Range(1, 4); i++)
            {
                int nivelmv = 1;
                switch (dificuldadeNPC)
                {
                    case 0:
                        nivelmv = 1;
                        break;
                    case 1:
                        nivelmv = Random.Range(1, 2);
                        break;
                    case 2:
                        nivelmv = Random.Range(1, 2);
                        break;
                    case 3:
                        nivelmv = Random.Range(1, 3);
                        break;
                    case 4:
                        nivelmv = Random.Range(1, 3);
                        break;
                    case 5:
                        nivelmv = Random.Range(2, 4);
                        break;
                    case 6:
                        nivelmv = Random.Range(2, 3);
                        break;
                    case 7:
                        nivelmv = 4;
                        break;
                    case 8:
                        nivelmv = 4;
                        break;
                    case 9:
                        nivelmv = 4;
                        break;
                }
                rob.Fisico.MovesAtivos.Add(Instantiate(Constructor.MoveConstructor(nivelmv)));
            }
            //retira ate ficar igua o attacksmax e nao dar erro na hora de carregar o ataque
            while (rob.Fisico.MovesAtivos.Count > rob.Fisico.AttacksMax)
            {
                rob.Fisico.MovesAtivos.RemoveAt(Random.Range(0, rob.Fisico.MovesAtivos.Count));
            }
            //carrega os ataque
            rob.Fisico.CarregarAtaques();
        }
            //criar combo
            if (Estrelas < dificuldadeNPC && rob.Fisico.Model == 5)
            {
                rob.Fisico.MontarArma(rob.Fisico.AttacksMax, Random.Range(3, 4));
            }
            else if (dificuldadeNPC >= 3)
            {
                if (rob.Fisico.Model == 5)
                {
                    rob.Fisico.MontarArma(rob.Fisico.AttacksMax, Random.Range(3, 8));
                }
                else
                {
                    rob.Fisico.MontarArma(rob.Fisico.AttacksMax, Random.Range(3, 7));
                }
            }
        
        //montarstatus
        float intg = Random.Range(0, Dificuldade) / 100;
        rob.Integridade = Mathf.RoundToInt(rob.IntegridadeMinima / 2 + rob.IntegridadeMinima * intg);        
        // rob.Integridade = 5;
        rob.Resistencia = calcularStatus(rob.ResistenciaMinima, rob.ResistenciaMaxima);
        rob.Ataque = calcularStatus(rob.AtaqueMinimo, rob.AtaqueMaximo);
        rob.AtaqueElemental = calcularStatus(rob.AtaqueElementalMinimo, rob.AtaqueElementalMaximo);
        rob.Velocidade = calcularStatus(rob.VelocidadeMinima, rob.VelocidadeMaxima);      
        rob.IntegridadeAtual = rob.Integridade;
        float fator = Random.Range(85, 101);
        rob.BateriaAtual = rob.Bateria;
        if (!player)
        {
            //rob.BateriaAtual = 5;
            if (TestarRivalPerder)
            {
                rob.IntegridadeAtual = 1;
                rob.Resistencia = 1;
            }
        }
        else
        {
            if (TestarPlayerPerder)
            {
                rob.IntegridadeAtual = 1;
                rob.Resistencia = 1;
            }
        }       
        return rob;
    }
    int calcularStatus(int minimo, int maximo)
    {
        int st;
        float ale = Random.Range(0, Dificuldade) / 100;
        st = Mathf.RoundToInt(minimo + ((maximo - minimo) * ale));
        return st;
    }
    void PorEmAtivos(List<Move> m, Weapon f)
    {
        if (m != null && m.Count > 0)
        {
            if (m.Count >= 1)
            {
                f.MovesAtivos.Add(m[m.Count - 1]);
            }
            if (m.Count >= 2)
            {
                f.MovesAtivos.Add(m[m.Count - 2]);
            }

        }
    }
}
