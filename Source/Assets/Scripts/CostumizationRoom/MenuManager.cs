using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private Walk player;
    public GameObject MenuForno;
    public GameObject MenuCentrifuga;
    public GameObject MenuMesa;
    public GameObject MenuRobo;
    public GameObject MenuNucleo;
    public GameObject MenuParte;
    public GameObject MenuPente;
    public GameObject MenuBateria;
    private bool menuAberto = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Walk>();
        //MenuForno.GetComponent<Merger>().Criar();
        //MenuCentrifuga.GetComponent<UnMerger>().Criar();
    }
    // Update is called once per frame
    void Update()
    {
        if(menuAberto)
        {
            if(Input.GetMouseButtonDown(0))
            {
                MenuForno.SetActive(false);
                MenuForno.GetComponent<Merger>().Fechar();
                MenuCentrifuga.SetActive(false);
                MenuCentrifuga.GetComponent<UnMerger>().Fechar();
                MenuMesa.SetActive(false);
                player.CanIWalk = true;
                menuAberto = false;
            }
            
        }
    }
    public void AbrirMenuForno()
    {
        player.CanIWalk = false;
        MenuForno.SetActive(true);
        menuAberto = true;
     
    }
    public void AbrirMenuCentrifuga()
    {
        player.CanIWalk = false;
        MenuCentrifuga.SetActive(true);
        menuAberto = true;
    }
    public void AbrirMenuMesa()
    {
        player.CanIWalk = false;
        MenuMesa.SetActive(true);
        menuAberto = true;
    }
    public void EscolherRobo(FantoRob robo)
    {
        MenuRobo.GetComponent<RobotGeneralMenu>().Montar(robo);
        MenuRobo.SetActive(true);
        
    }
    public void AbrirMenuPart(RobotPart part, int id, RobotGeneralMenu menu, Sprite sprit)
    {
        MenuParte.GetComponent<PartMenu>().Montar(part, id);
        MenuParte.SetActive(true);
    }
    public void AbrirMenuNucleo(GameObject core, int id, RobotGeneralMenu menu, Status status)
    {
        MenuNucleo.GetComponent<CoreMenu>().CriarMenu(core, id, menu, status);
        MenuNucleo.SetActive(true);
    }
    public void AbrirMenuPente(Plug plug)
    {
        MenuPente.GetComponent<CombSelectionMenu>().Criar(plug);
        MenuPente.SetActive(true);


    }
    public void AbrirMenuBateria(CoreMenu core)
    {
        //MenuBateria.GetComponent<BatterySelectionMenu>().Criar(core);
        MenuBateria.SetActive(true);
    }
}
