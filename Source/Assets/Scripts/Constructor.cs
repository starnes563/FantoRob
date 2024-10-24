using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constructor : MonoBehaviour
{
    public static Constructor Instance;
    public List<GameObject> ItemPrefab;   
    public List<Pente> CombPrefab;     
    public List<FantoRob> Fantorobs;    
    //Listas de Sprites
    public Sprite SpriteBateria;
    public Sprite SpritePenteVazio;
    public Sprite SpritePenteCheio;
    public Sprite SpriteSilicio;
    public List<Sprite> SpritesCircuitos;
    public List<Sprite> SpritesItensConstruir;
    public List<Sprite> SpritesPartesRobo;
    public List<Sprite> SpriteMochila;
    //ListaNomes
    public List<string> NomesBateria;   
    public List<string> NomesCircuitos;
    public List<string> NomesItensConstruir;
    public List<string> NomesSilicio;
    public List<string> NomesPente;
    public List<string> NomesPart;
    public List<string> NomesMochila;
    //variaveis para os metodos
    public static Sprite GetSprite;
    public static string Nome;
    //para criar moves
    public List<GameObject> Animacoes;
    public static int controledeid = 155;
    public List<string> Nome1;
    public List<string> Nome2;
    public static Move MoveRetorno;
    public List<string> DescricaoElemental;
    public List<string> DescricaoFisico;
    public List<Move> MovesDeEfeito;
    public List<Weapon> NucleosFisicos;
    public List<Move> MovesEstaticos;
    //0 - Portugues
    public int Lingua = 0;
    //lista de spritesjogador
    public List<Sprite> SpritesPersoangem;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        // if exists but is not this one
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    public static Sprite RetornarSprite(int tipo,int item,int cicuito,int itemconst, int partrobo)
    {         
        switch (tipo)
        {
            case 0:
                GetSprite = Instance.SpriteSilicio;
                break;
            case 1:
                GetSprite = Instance.SpritePenteCheio;
                break;
            case 2:
                GetSprite = Instance.ItemPrefab[item].GetComponent<Item>().Sprite;
                break;
            case 3:
                GetSprite = Instance.SpriteBateria;
                break;
            case 4:
                GetSprite = Instance.SpritePenteVazio;
                break;
            case 5:
                GetSprite = Instance.SpritesCircuitos[cicuito];
                break;
            case 6:
                GetSprite = Instance.SpritesItensConstruir[itemconst];
                break;
            case 7:
                GetSprite = Instance.SpritesPartesRobo[partrobo];
                break;
            case 9:
                switch(item)
                {
                    case 20:
                        GetSprite = Instance.SpriteMochila[0];
                        break;
                    case 30:
                        GetSprite = Instance.SpriteMochila[1];
                        break;
                }

                break;
        }
        return GetSprite;
    }
    public static string RetornarNome(int tipo, int item, int bateria, int cicuito, int itemconst, int partrobo)
    {
        switch (tipo)
        {
            case 0:
                Nome = Instance.NomesSilicio[ManagerGame.Instance.Idm];
                break;
            case 1:
                Nome = Instance.NomesPente[ManagerGame.Instance.Idm];
                break;
            case 2:
                Nome = Instance.ItemPrefab[item].GetComponent<Item>().Nome[ManagerGame.Instance.Idm];
                break;
            case 3:
                Nome = Instance.NomesBateria[bateria];
                break;
            case 4:
                Nome = "";
                break;
            case 5:
                Nome = Instance.NomesCircuitos[cicuito];
                break;
            case 6:
                Nome = Instance.NomesItensConstruir[itemconst];
                break;
            case 7:
                Nome = Instance.NomesPart[partrobo];
                break;
            case 9:
                switch (ManagerGame.Instance.Idm)
                {
                    case 0:
                        switch (item)
                        {
                            case 20:
                                Nome = Instance.NomesMochila[0];
                                break;
                            case 30:
                                Nome = Instance.NomesMochila[1];
                                break;
                        }
                        break;
                    case 1:
                        switch (item)
                        {
                            case 20:
                                Nome = Instance.NomesMochila[2];
                                break;
                            case 30:
                                Nome = Instance.NomesMochila[3];
                                break;
                        }
                        break;
                }
               

                break;
        }
        return Nome;
    }
    public Type TypeCreator(int tipo)
    {
        Type newtype = ScriptableObject.CreateInstance<Type>();
        newtype.Tipo = tipo;
        newtype.Sprite = RetornarSprite(4, 0, 0, 0,0);
        switch(tipo)
        {
            case 2:
                newtype.Nome = NomesPente[ManagerGame.Instance.Idm];
                break;
        }
       
        newtype.Quantidade = 1;
        return newtype;
       
    }
    private void canSellCreator(GameObject newobject, GameObject prefab, int id)
    {
        

    }
    //Constructors
    public GameObject ItemConstructor(int id)
    {
        GameObject NewItem = new GameObject();
        Item item = NewItem.AddComponent<Item>();
        Item itempre = ItemPrefab[id].GetComponent<Item>();

        item.Itemid = id;
        item.Nome = itempre.Nome;
        item.Quantidade = itempre.Quantidade;
        item.Descricao = itempre.Descricao;
        item.Sprite = itempre.Sprite;
        item.Tipo = itempre.Tipo;
        item.GastoAcoes = itempre.GastoAcoes;
        item.Fator = itempre.Fator;
        return NewItem;
    }    
    public Circuit CircuitConstructor(int array)
    {
        Circuit newCircuit = ScriptableObject.CreateInstance<Circuit>();
        newCircuit.IniciarCircuito(array);
        
        return newCircuit;
    }
    public Pente CombConstructor(bool empty, int level)
    {
        Pente newComb = ScriptableObject.CreateInstance<Pente>();
        
        if(!empty)
        {
            newComb.ForgeInCreation(this, CircuitConstructor(Random.Range(0, 16)), CircuitConstructor(Random.Range(0, 16)), CircuitConstructor(Random.Range(0, 16)),
                CircuitConstructor(Random.Range(0, 16)), level, TypeCreator(2), MoveConstructor(level));                         
        }
        else
        {
            newComb.CriarPente(level, TypeCreator(2), MoveConstructor(level));
        }
        return newComb;
    }
    public static Move MoveConstructor(int nivel)
    {
        MoveRetorno = ScriptableObject.CreateInstance<Move>();
        if (Random.Range(0, 101) < 95)
        {
            MoveRetorno.Aleatório = true;
            int m = ManagerGame.Instance.Idm * 3;
            MoveRetorno.Nome = Instance.Nome1[Random.Range(m, m+3)] + " " + Instance.Nome2[Random.Range(m, m+3)];
           
            switch (nivel)
            {
                case 1:
                    MoveRetorno.Forca = Random.Range(30, 41);
                    MoveRetorno.Precisao = Random.Range(75, 91);
                    MoveRetorno.GastoEnergiaPercentual = Random.Range(15, 26);
                    MoveRetorno.UsoDeAcoes = 1;
                    int anim = Random.Range(0, 18);
                    MoveRetorno.Animacao = anim;
                    MoveRetorno.Nome = MoveRetorno.Nome + " " + anim.ToString();
                    MoveRetorno.AnimacaoDeAtaque = Instance.Animacoes[anim];
                    break;
                case 2:
                    MoveRetorno.Forca = Random.Range(50, 71);
                    MoveRetorno.Precisao = Random.Range(75, 91);
                    MoveRetorno.GastoEnergiaPercentual = Random.Range(25, 36);
                    MoveRetorno.UsoDeAcoes = 1;
                    anim = Random.Range(0, 43);
                    MoveRetorno.Animacao = anim;
                    MoveRetorno.AnimacaoDeAtaque = Instance.Animacoes[anim];
                    break;
                case 3:
                    MoveRetorno.Forca = Random.Range(65, 86);
                    MoveRetorno.Precisao = Random.Range(75, 91);
                    MoveRetorno.GastoEnergiaPercentual = Random.Range(30, 41);
                    MoveRetorno.UsoDeAcoes = 1;
                    anim = Random.Range(0,82);
                    MoveRetorno.Animacao = anim;
                    MoveRetorno.AnimacaoDeAtaque = Instance.Animacoes[anim];
                    break;
                case 4:
                   
                    MoveRetorno.Forca = Random.Range(75, 96);
                    MoveRetorno.Precisao = Random.Range(75, 96);
                    MoveRetorno.GastoEnergiaPercentual = Random.Range(35, 46);
                    
                    MoveRetorno.UsoDeAcoes = 1;
                    anim = Random.Range(0, Instance.Animacoes.Count);
                    MoveRetorno.Animacao = anim;
                    MoveRetorno.AnimacaoDeAtaque = Instance.Animacoes[anim];
                   
                    break;

            }
            MoveRetorno.Efeito = 0;
            MoveRetorno.Diferenciado = false;
           
            if (Random.Range(0, 101) > 50)
            {
                MoveRetorno.elemental = true;
                MoveRetorno.Elemento = Random.Range(0, 6);
                MoveRetorno.Descrição.Add(Instance.DescricaoElemental[ManagerGame.Instance.Idm]);
            }
            else
            {
                MoveRetorno.Descrição.Add(Instance.DescricaoFisico[ManagerGame.Instance.Idm]);
                MoveRetorno.Elemento = 9;
            }
            MoveRetorno.Id = controledeid;
            controledeid++;
            if (MoveRetorno.elemental) { MoveRetorno.AnimacaoDeAtaque.GetComponent<AttackAnimation>().MeuTipo = AttackAnimation.Tipo.ELEMENTAL; }
        }
        else
        {
           
            switch (nivel)
            {
                case 1:
                    MoveRetorno = Instance.MovesDeEfeito[Random.Range(0,4)];
                   
                    break;
                case 2:
                    MoveRetorno = Instance.MovesDeEfeito[Random.Range(4, 7)];

                    break;
                case 3:
                    MoveRetorno = Instance.MovesDeEfeito[Random.Range(7, 10)];

                    break;
                case 4:
                    MoveRetorno = Instance.MovesDeEfeito[Random.Range(10, 12)];

                    break;

            }
        }
     
        return MoveRetorno;
    }
    public RobotPart PartConstructor(int partid, int compilador, int boardlevel)
    {
        RobotPart part = ScriptableObject.CreateInstance<RobotPart>();
        switch(boardlevel)
        {
            case 1:
                part.CriarParte(partid, compilador, Random.Range(0, 5), boardlevel);
                break;
            case 2:
                part.CriarParte(partid, compilador, Random.Range(5, 12), boardlevel);
                break;
        }
     
        return part;
    }
   
   
  
 
}
