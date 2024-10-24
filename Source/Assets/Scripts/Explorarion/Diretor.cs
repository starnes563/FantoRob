using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diretor : MonoBehaviour
{
    public GameObject Iniciar;
    public GameObject Terminar;
    [HideInInspector]
    public bool PodeIniciar = false;
    public bool BattleCamera = false;
    public GameObject MenuPlayer;
    public static GameObject MenuPlayerStatic;
    public static Diretor Instance;
    public CaixaDialogo CaixaDialogo;
    public SetaGPS MinhaSetaGPS;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        if(MenuPlayer!=null){ MenuPlayerStatic = MenuPlayer; }        
        if (!BattleCamera)
        {
            IniciarCena();
        }        
    }

    // Update is called once per frame
    
     public void IniciarCena()
    {
        Instantiate(Iniciar, transform).GetComponent<IniciarCena>().Diretor = this;
    }
    public void TrocarACena()
    {       
        PodeIniciar = false;
        Instantiate(Terminar, transform);       
    }
    public static void DesativarMenuPlayer()
    {
        if(MenuPlayerStatic.activeSelf)
        {
            MenuPlayerStatic.SetActive(false);
        }
    }
}
