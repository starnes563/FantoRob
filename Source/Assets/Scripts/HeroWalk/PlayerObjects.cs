using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjects : MonoBehaviour
{
    public static PlayerObjects PlayerObjectsStatic;
    public static List<FantoRob> RobotsInUse = new List<FantoRob>();
    public static List<FantoRob> RobotsNotInUse = new List<FantoRob>();
    public static List<Pente> PentesVazios = new List<Pente>();
    public static int[] Circuits = new int[16];
    public static List<Pente> PentesCheios = new List<Pente>();
    public static int Silicon;
    public static List<RobotPart> RobotParts = new List<RobotPart>();
    public List<GameObject> Itens = new List<GameObject>();
    public List<int> Batteries = new List<int>(7);
    public List<GameObject> Weapons = new List<GameObject>();  
    public static List<int> ItensConstruir = new List<int>(44);
    public static List<Weapon> NucleosFisicos = new List<Weapon>();
    public static int Fantodin = 0;
    public static int Creditos = 0;
    public static List<Quest> Missões = new List<Quest>();
    private bool AdicionarInvent = false;
    public static int EsqueletoEspecial = 0;
    public FantoRob FantoRobParty;
    public bool adicioniarFantoRobParty;
    public static int InventarioMax = 5;
    public void Awake()
    {
        //check if the instance already exists
        if (PlayerObjectsStatic == null)
        {            
            PlayerObjectsStatic = this;
            if(ItensConstruir.Count==0)
            {
                ItensConstruir.Clear();
                for (int i = 0; i < 44; i++)
                {
                    ItensConstruir.Add(0);
                }
            }
        }       
    }   
    private void Start()
    {
        if (AdicionarInvent)
        {
            AdicionarInvent = false;
            if (PentesVazios.Count <= 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    PentesVazios.Add(Constructor.Instance.CombConstructor(true, 1));
                }
            }
            if (RobotParts.Count <= 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    RobotParts.Add(Constructor.Instance.PartConstructor(Random.Range(0, 5), Random.Range(0, 5), 1));
                }
            }
            for (int i = 0; i < Circuits.Length; i++)
            {
                Circuits[i] = 5;
            }
            foreach(GameObject g in Itens)
            {
                g.GetComponent<Item>().Quantidade = 1;
            }
            for (int i = 0; i < Batteries.Count; i++)
            {
                Batteries[i] = 5;
            }
            for (int i = 0; i < ItensConstruir.Count; i++)
            {
              ItensConstruir[i] = 15;
            }
            Fantodin = 1000000;
            Silicon = 99;
            PlayerStatus.Estrelas = 7;
           foreach(FantoRob rob in Constructor.Instance.Fantorobs)
           {
                FantoRob r = Instantiate(rob);
               r.Fisico = Instantiate(Constructor.Instance.NucleosFisicos[0]);
               r.IniciarRobo(false);
                RobotsNotInUse.Add(Instantiate(r));
            }          
            foreach (Weapon w in Constructor.Instance.NucleosFisicos)
            {
                Weapon we = Instantiate(w);
                we.IniciarArma(false);
                NucleosFisicos.Add(we);
            }
        }
        if(adicioniarFantoRobParty)
        {            
            FantoRobParty.IniciarRobo(false);
            RobotsInUse.Add(Instantiate(FantoRobParty));
        }
    }

    public static void PerderLoot()
    {
        int i = Random.Range(4, 6);
        for(int t = 0; t<i;t++)
        {
            int loot = Random.Range(0, 6);
            switch(loot)
            {
                case 0:
                    int r = Random.Range(0, 44);
                    if(ItensConstruir[r]>0)
                    {
                        ItensConstruir[r]--;
                    }
                    break;
                case 1:
                    if (PentesVazios != null && PentesVazios.Count > 0)
                    {
                        PentesVazios.RemoveAt(Random.Range(0, PentesVazios.Count));
                    }
                    break;                                       
                case 2:
                    if(PentesCheios != null && PentesCheios.Count>0)
                    {
                        PentesCheios.RemoveAt(Random.Range(0, PentesCheios.Count));
                    }                    
                    break;
                case 3:
                    int a = Random.Range(0, 16);
                    if(Circuits[a]>0)
                    {
                        Circuits[a]--;
                    }                    
                    break;
                case 4:
                    if(Silicon>0)
                    {
                        Silicon--;
                    }
                                        break;
                case 5:
                    if(RobotParts !=null && RobotParts.Count>0)
                    {
                        RobotParts.RemoveAt(Random.Range(0, RobotParts.Count));
                    }                    
                    break;
            }
        }
    }



























}
