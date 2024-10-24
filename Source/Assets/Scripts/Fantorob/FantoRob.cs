using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "FantoRob", menuName = "Fantorob/Carcaca")]
[System.Serializable]
public class FantoRob : ScriptableObject
{
    //Define um codigo para comparar robos do mesmo modelo
    public int IndividualCode;
    // Modelo define qual fantorob sera instanciado na cena da batalha
    public int Modelo;
    public Sprite MenuIconeFantorob;
    public Sprite MinhaCaixa;
    public string Nome;

    //PartesDoRobo
    //0 - cabeça
    //1 - braço direito
    //2 - braço esquerdo
    //3 - perna direita
    //4 - perna esquerda
    public int NumeroDePartes;
    public RobotPart[] RobotPart = new RobotPart[5];
    //arma
    public Weapon Fisico;
    public Sprite SpriteFisico;

    public int GastoEnergiaTotal = 50;

    // Define o Nucleo colocar hide in inspector depois
    //0 - Vermelho
    //1 - Azul
    //2 - Amarelo
    //3 - Verde
    //4 - Laranja
    //5 - Roxo
    public int Elemento;
    public Sprite SpriteElemento;

    // Status Atual
    public int Integridade;
    public int Bateria;
    public int Resistencia;
    public int Velocidade;
    public int Ataque;
    public int AtaqueElemental;
    public int IntegridadeAtual;
    public int BateriaAtual;
    
    //StatusMinimo
    public int IntegridadeMinima;
    public int ResistenciaMinima;
    public int VelocidadeMinima;
    public int AtaqueMinimo;
    public int AtaqueElementalMinimo;

    //StatusMaximo
    public int IntegridadeMaxima;
    public int ResistenciaMaxima;
    public int VelocidadeMaxima;
    public int AtaqueMaximo;
    public int AtaqueElementalMaximo;

    //controla o numero do turnos vai de 3 a 6
    public int Acoes;        
    // define o numero maximo de turnos que um keylogger pode tirar
    public int keyloggerVariant;

    // define o tipo de overlock é sempre um percentual
    public float EnergiaOV;
    public float ResistenciaOV;
    public float VelocidadeOV;
    public float AtaqueOV;
    public float AtaqueEnergeticoOV;

    //Define a velocidade que se recupera do broke
    public float FatordeRecuperação;

    //Cores para animações na batalha
    public Color Broke;
    public Color Overlock;
    public Color Infeccao;

    //variaveis de  controle de situaçao atual
    [HideInInspector]
    public bool Spy = false;
    [HideInInspector]
    public bool Keylogger = false;
    [HideInInspector]
    public bool Trojan = false;
    [HideInInspector]
    public float FatorTrojan = 0;
    [HideInInspector]
    public bool Ranson = false;
    [HideInInspector]
    public float FantoRanson = 0;
    [HideInInspector]
    public bool Worm = false;
    [HideInInspector]
    public bool Virus = false;
    [HideInInspector]
    public float AumentoSpy;
    [HideInInspector]
    public int KeyloggerAtual;
    [HideInInspector]
    public float WormPercentual;
    [HideInInspector]
    public float VirusPercentual;

    public List<Move> MovimentoAmbos = new List<Move>();
    public List<Move> MovimentoJogador = new List<Move>();
    public List<Move> MovimentoInimigo = new List<Move>();

    //para testar
    [HideInInspector]
    public int BatalhaTravada;
    [HideInInspector]
    public int BatalhaVencida;
    //para configurar os moves  
    public void IniciarRobo(bool rival)
    {
        Fisico.MovesAtivos.Clear();
        if(MovimentoAmbos!=null && MovimentoAmbos.Count>0)
        {
            Fisico.MovesAtivos.Add(MovimentoAmbos[0]);
        }
        if (Fisico.MovimentosAmbos != null && Fisico.MovimentosAmbos.Count > 0)
        {
            Fisico.MovesAtivos.Add(Fisico.MovimentosAmbos[0]);
        }

        if (!rival)
        {
            if (MovimentoJogador != null && MovimentoJogador.Count > 0)
            {
                Fisico.MovesAtivos.Add(MovimentoJogador[0]);
            }
            if (Fisico.MovimentosJogador != null && Fisico.MovimentosJogador.Count > 0)
            {
                Fisico.MovesAtivos.Add(Fisico.MovimentosJogador[0]);
            }
        }
        else
        {
            if (MovimentoInimigo != null && MovimentoInimigo.Count > 0)
            {
                Fisico.MovesAtivos.Add(MovimentoInimigo[0]);
            }
            if (Fisico.MovimentosInimigo != null && Fisico.MovimentosInimigo.Count > 0)
            {
                Fisico.MovesAtivos.Add(Fisico.MovimentosInimigo[0]);
            }
        }
        Fisico.MontarArma(Fisico.NumeroDeAtaques, Random.Range(Fisico.CombosMin,Fisico.CombosMax+1));
        Fisico.CarregarAtaques();
    }
    public void MontarRobo()
    {
        GastoEnergiaTotal = 50;
        //Atribui os valores minimo
        Ataque = AtaqueMinimo;
        AtaqueElemental = AtaqueElementalMinimo;
        Integridade = IntegridadeMinima;
        Resistencia = ResistenciaMinima;
        Velocidade = VelocidadeMinima;
        //calculo         
            for (int i = 0; i < RobotPart.Length; i++)
            {
                if (RobotPart[i] != null)
                {
                GastoEnergiaTotal += RobotPart[i].Energyspent;             
                    
                        //ak
                            Ataque += (int)RobotPart[i].Value[0];
                            if (Ataque > AtaqueMaximo)
                            {
                                Ataque = AtaqueMaximo;
                            }                           
                       
                        //as
                            AtaqueElemental += (int)RobotPart[i].Value[1];
                            if (AtaqueElemental > AtaqueElementalMaximo)
                            {
                            AtaqueElemental = AtaqueElementalMaximo;
                            }                          
                       
                        //res
                            Resistencia += (int)RobotPart[i].Value[2];
                            if (Resistencia > ResistenciaMaxima)
                            {
                                Resistencia = ResistenciaMaxima;
                            }                           
                       
                            Velocidade += (int)RobotPart[i].Value[3];
                            if (Velocidade > VelocidadeMaxima)
                            {
                                Velocidade = VelocidadeMaxima;
                            }
                           
                        //int
                            Integridade += (int)RobotPart[i].Value[4];
                            if (Integridade > IntegridadeMaxima)
                            {
                                Integridade = IntegridadeMaxima;
                            }                           
                       
                    
                }
            }
    }
    public void ReceberPart(RobotPart parte, int id)
    {
        RobotPart[id] = parte;
        MontarRobo();
    }
    public void ReceberNucleo(GameObject nucleo, Weapon arma)
    {        
        Fisico = arma;        
    }
    //ControlarOStatusAtual
    public void RetiraSpy()
    {
        Spy = false;
        AumentoSpy = 0;
    }
    public void RetiraKeylogger()
    {
        Keylogger = false;
        KeyloggerAtual = 0;
    }
    public void RetiraTrojan()
    {
        Trojan = false;
    }
    public void RetiraRanson()
    {
        Ranson = false;
    }
    public void RetiraWorm()
    {
        Worm = false;
        WormPercentual = 0;
    }
    public void RetiraVirus()
    {
        Virus = false;
        VirusPercentual = 0;
    }
    public void RecuperaIntegridade(float recurepacao)
    {
        IntegridadeAtual += (int)recurepacao;
        if (IntegridadeAtual > Integridade)
        {
            IntegridadeAtual = Integridade;
        }
    }
    public void RecuperaBateria(float recurepacao)
    {
        BateriaAtual += (int)recurepacao;
        if (BateriaAtual > Bateria)
        {
            BateriaAtual = Bateria;
        }

    }
    public void Curartudo()
    {
        RetiraRanson();
        RetiraKeylogger();
        RetiraSpy();
        RetiraWorm();
        RetiraVirus();
        RetiraTrojan();
        RecuperaIntegridade(Integridade);
        RecuperaBateria(Bateria);
    }
    public void RetirarTudo()
    {
        foreach (RobotPart pr in RobotPart)
        {
            if(pr!=null)
            {
                pr.ZerarPlaca();
            }
        }
        if(Fisico.MovesDePentes.Count>0)
        {
            for(int i = Fisico.MovesDePentes.Count-1;i>-1;i--)
            {
                Fisico.RemoverMovePente(Fisico.MovesDePentes[i].meuIdNoPente);
            }
        }        
    }
    public void CarregarSalvo(DadosFantorob f)
    {
        IndividualCode = f.IndividualCode;
        Modelo = f.Modelo;
        NumeroDePartes = f.NumeroDePartes;
        for (int i = 0; i < 5; i++)
        {
            if (f.RobotPart[i] != null)
            {               
                RobotPart part = ScriptableObject.CreateInstance<RobotPart>();
                part.CarregarSalvo(f.RobotPart[i]);
                RobotPart[i] = part;
            }
            else
            {
                RobotPart[i] = null;
            }
        }        
        Weapon nf = Instantiate(Constructor.Instance.NucleosFisicos[f.Fisico.Array]);      
        nf.CarregarSavlo(f.Fisico);      
        Fisico = nf;       
        GastoEnergiaTotal = f.GastoEnergiaTotal;
        Integridade = f.Integridade;
        Bateria = f.Bateria;
        Resistencia = f.Resistencia;
        Velocidade = f.Velocidade;
        Ataque = f.Ataque;
        AtaqueElemental = f.AtaqueElemental;
        IntegridadeAtual = f.IntegridadeAtual;
        BateriaAtual = f.BateriaAtual;
        Spy = f.Spy;
        Keylogger = f.Keylogger;
        Trojan = f.Trojan;
        Ranson = f.Ranson;
        Virus = f.Virus;
        AumentoSpy = f.AumentoSpy;
        KeyloggerAtual = f.KeyloggerAtual;
        WormPercentual = f.WormPercentual;
        VirusPercentual = f.VirusPercentual;       
    }
}
