using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionData : MonoBehaviour
{
    public List<string> RegionName = new List<string>();
    public List<FantoRob> PossibleEnemyRobots = new List<FantoRob>();   
    public bool InstanciaHero;
    public bool PodeAmigo;
    public bool LoadSaveOnLose;
    public GameObject CameraBatalha;
    public GameObject CenaBatalha;
    public GameObject AudioBatalha;
    public TransitionManager GerenteTransicao;
    public List<GameObject> AnimacoesDeTroca;
    public List<GatilhoCutscene> GatilhosCut;
    public GameObject GlobalLight;
    public AudioSource CaixaDeSom;
    public GameObject CameraCenario;
    public GameObject Posicao;
    public bool Loot = false;
    //do menos raro para o mais raro
    public List<Loot> PossibleLoot;
    public List<Loot> AllLoot;
    public List<int> IndexAllLootPorEstrela;
    public bool Abertura = false;
    public bool Desafio = false;
    public bool Cutscene = false;
    public int Saida;
    public Vector3 PosicaoSaida;
    private Diretor Diretor;
    public int Quarto;
    public Vector3 PosicaoQuarto;
     void Start()
    {
        //CalcularPossibleLoot();
        if (Loot)
        {
            StartCoroutine(CalcularPossibleLoot());
        }
        if(Desafio || Cutscene)
        {
           Diretor = GameObject.FindWithTag("MainCamera").GetComponent<Diretor>();
        }
    }
    public IEnumerator CalcularPossibleLoot()
    {
        PossibleLoot.Clear();
        //pega as estrelas do player
       // int atual;
       // int basico = 0;
       // atual = IndexAllLootPorEstrela[PlayerStatus.Estrelas];
        switch (PlayerStatus.Estrelas)
        {
            case 0:                
            //    basico = 0;
                break;
            case 1:               
            //    basico = 0;
                break;
            case 2:                
           //     basico = IndexAllLootPorEstrela[0];
                break;
            case 3:                
             //   basico = IndexAllLootPorEstrela[0];
                break;
            case 4:               
            //    basico = IndexAllLootPorEstrela[2];
                break;
            case 5:               
             //   basico = IndexAllLootPorEstrela[2];
                break;
            case 6:               
              //  basico = IndexAllLootPorEstrela[4];
                break;
            case 7:                
              //  basico = IndexAllLootPorEstrela[4];
                break;
            case 8:               
             //   basico = IndexAllLootPorEstrela[4];
                break;
        }                
        //pega o loot 
        for(int i = 0; i<AllLoot.Count; i++)
        {            
            PossibleLoot.Add(AllLoot[i]);
        }
        //pegar loot de estrelas anteriores
        //int vezes = Random.Range(3, 6);
        //for(int i = 0; i<vezes;i++)
       //{
            //if(basico >0)
            //{
           // comeco:
                //Loot proximoloot = AllLoot[Random.Range(0, basico+1)];
               // if (PossibleLoot.Contains(proximoloot))
              //  {
                //    goto comeco;
                //}
               // else
               // {
                //    PossibleLoot.Add(proximoloot);
               // }
          //  }
           // else
          //  {
                //break;
            //}
                 
       // }
        yield return null;
                
    }
    public void IrParaEntrada()
    {
        ManagerGame.Instance.SceneToLoad = Saida;
        PlayerStatus.NextHeroPosition = PosicaoSaida;
        GameObject.FindWithTag("MainCamera").GetComponent<Diretor>().TrocarACena();
    }
    public void IrParaQuarto()
    {
        ManagerGame.Instance.SceneToLoad = Quarto;
        PlayerStatus.NextHeroPosition = PosicaoQuarto;
        GameObject.FindWithTag("MainCamera").GetComponent<Diretor>().TrocarACena();
    }

}
