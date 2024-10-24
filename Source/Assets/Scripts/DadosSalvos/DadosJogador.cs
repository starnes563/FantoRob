using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[System.Serializable]
public class DadosJogador
{
    //Tempo
    public float Tempo;
    public float TempoMissao;
    //Level
    public int Level = 1;
    public int Exp = 0;
    public int nextLevel = 100;
    public int nextstar = 10;
    //Reputation
    public int Reputation = 1;
    public int Trending = 0;
    public int nextReputation = 70;
    //dinheiro
    public float Money;   
    //estrelas
    public int Estrelas;
    public int ControleDeCena;
    public bool CartaEndosso;
    //dias desafio
    public int DaysLeft = 7;
    //desafio
    public int Posicao;
    public int Pontos;
    //posicao
    public float[] transfom = new float[3];
    public int Cena;
    public int PersonagemAtual;
    //Marcus
    public bool MarcusAtivo = false;
    public float[] MarcusTransform = new float[3];
    //Luiza
    public bool LuizaAtiva = false;
    public float[] LuizaTransform = new float[3];
    //objetos
    public List<DadosFantorob> RobotsInUse = new List<DadosFantorob>();
    public List<DadosFantorob> RobotsNotInUse = new List<DadosFantorob>();
    public List<DadoPente> Combs = new List<DadoPente>();
    public int[] Circuits = new int[16];
    public List<DadoPente> PentesCheios = new List<DadoPente>();
    public int Silicon;
    public List<DadoParte> RobotParts = new List<DadoParte>();
    public List<int> Itens = new List<int>();
    public List<int> Batteries = new List<int>(7);       
    public List<int> ItensConstruir = new List<int>(44);
    public List<DadoNucleoFisico> NucleosFisicos = new List<DadoNucleoFisico>();
    public int Fantodin = 0;
    public int Creditos = 0;
    public int ActualSavePath;
    public List<DadosMissao> Missões = new List<DadosMissao>();
    public int EsqueletoEspecial = 0;
    public int InventarioMax = 10;
    //StoryEvents
    public DadosStory DadosStory;
    public string Nome;
    public int Sprite;
    //lingua
    public int Linguaatual;
    public DadosJogador()
    {
        //Tempo
        Tempo = ManagerGame.Instance.Tempo;      
        //posicaojogador
        Cena = SceneManager.GetActiveScene().buildIndex;
        transfom[0] = ManagerGame.Instance.HeroAtual.transform.position.x;
        transfom[1] = ManagerGame.Instance.HeroAtual.transform.position.y;
        transfom[2] = ManagerGame.Instance.HeroAtual.transform.position.z;
        PersonagemAtual = PlayerStatus.PersonagemAtual;
        //posicaomarcus        
        if (ManagerGame.Instance.Regiao.GetComponent<Posicionador>().Marcus != null)
        {
            MarcusAtivo = ManagerGame.Instance.Regiao.GetComponent<Posicionador>().Marcus.activeSelf;
            MarcusTransform[0] = ManagerGame.Instance.Regiao.GetComponent<Posicionador>().Marcus.transform.position.x;
            MarcusTransform[1] = ManagerGame.Instance.Regiao.GetComponent<Posicionador>().Marcus.transform.position.y;
            MarcusTransform[2] = ManagerGame.Instance.Regiao.GetComponent<Posicionador>().Marcus.transform.position.z;
        }
        else
        {
            MarcusAtivo = false;
        }
        //posicaoluiza        
        if (ManagerGame.Instance.Regiao.GetComponent<Posicionador>().Luiza != null)
        {
            LuizaAtiva = ManagerGame.Instance.Regiao.GetComponent<Posicionador>().Luiza.activeSelf;
            LuizaTransform[0] = ManagerGame.Instance.Regiao.GetComponent<Posicionador>().Luiza.transform.position.x;
            LuizaTransform[1] = ManagerGame.Instance.Regiao.GetComponent<Posicionador>().Luiza.transform.position.y;
            LuizaTransform[2] = ManagerGame.Instance.Regiao.GetComponent<Posicionador>().Luiza.transform.position.z;
        }
        else
        {
            LuizaAtiva = false;
        }
        //playerstatus
        Level = PlayerStatus.Level;
        Exp = PlayerStatus.Exp;
        nextLevel = PlayerStatus.nextLevel;
        Reputation = PlayerStatus.Reputation;
        Trending = PlayerStatus.Trending;
        nextReputation = PlayerStatus.nextReputation;
        Estrelas = PlayerStatus.Estrelas;
        nextstar = PlayerStatus.nextstar;
        ControleDeCena = PlayerStatus.ControleDeCena;
        CartaEndosso = PlayerStatus.CartaEndosso;
        DaysLeft = PlayerStatus.DaysLeft;
        Posicao = PlayerStatus.Posicao;
        Pontos = PlayerStatus.Pontos;
        //PlayerObjects
        foreach(FantoRob i in PlayerObjects.RobotsInUse)
        {
            if(i != null)
            {
                RobotsInUse.Add(new DadosFantorob(i));
            }           
        }
        foreach (FantoRob i in PlayerObjects.RobotsNotInUse)
        {
            if (i != null)
            {
                RobotsNotInUse.Add(new DadosFantorob(i));
            }               
        }
        foreach (Pente i in PlayerObjects.PentesVazios)
        {
            if (i != null)
            {
                Combs.Add(new DadoPente(i));
            }
                
        }
        for(int i = 0; i<16;i++)
        {
            Circuits[i] = PlayerObjects.Circuits[i];
                          
        }
        foreach (Pente i in PlayerObjects.PentesCheios)
        {
            if (i != null)
            {
                PentesCheios.Add(new DadoPente(i));
            }               
        }       
            Silicon = PlayerObjects.Silicon;                   
        foreach (RobotPart i in PlayerObjects.RobotParts)
        {

            if (i != null)
            {
                RobotParts.Add(new DadoParte(i));
            }               
        }
        for (int i = 0; i < PlayerObjects.PlayerObjectsStatic.Itens.Count; i++)
        {
            if (PlayerObjects.PlayerObjectsStatic.Itens[i] != null)
            {
                Itens.Add(PlayerObjects.PlayerObjectsStatic.Itens[i].GetComponent<Item>().Quantidade);
            }                
        }
        foreach (int i in PlayerObjects.PlayerObjectsStatic.Batteries)
        {          
                Batteries.Add(i);
        }
        foreach (int i in PlayerObjects.ItensConstruir)
        {           
                ItensConstruir.Add(i);
        }
        foreach (Weapon i in PlayerObjects.NucleosFisicos)
        {
            if (i != null)
            {
                NucleosFisicos.Add(new DadoNucleoFisico(i));
            }                
        }
        Fantodin = PlayerObjects.Fantodin;
        Creditos = PlayerObjects.Creditos;
        ActualSavePath = ManagerGame.Instance.ActualSavePath;
        Missões.Clear();
        foreach(Quest q in PlayerObjects.Missões)
        {
            if (q != null)
            {
                Missões.Add(new DadosMissao(q));
            }                
        }
        EsqueletoEspecial = PlayerObjects.EsqueletoEspecial;
        InventarioMax = PlayerObjects.InventarioMax;
        //Nome = PlayerStatus.Nome;
        //Sprite = Constructor.Instance.SpritesPersoangem.IndexOf(PlayerStatus.MeuSprite);
        DadosStory = new DadosStory();
        Linguaatual = ManagerGame.Instance.Idm;
    }
}
