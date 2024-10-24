using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSubMenu : MonoBehaviour
{
    public Button ActButton;
    public Button SeeButton;
    public bool party;
    public ChangeRobot Menu;
    public FantoRob MyRobot;
    private bool Ativo = false;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        if(Ativo)
        {
            if(Input.GetButtonDown("Fire2"))
            {
                Fechar();
            }
        }
    }
    void Fechar()
    {
        gameObject.SetActive(false);
        Ativo = false;
        Menu.SubAberto = false;
    }
    public void Iniciar()
    {
       
    }
    public void Ver()
    {
        Menu.SeeRobot(MyRobot);
    }
    public void TirarParty()
    {
        Menu.RetirarParty(MyRobot);
    }
    public void ColocarParty()
    {
        Menu.PorParty(MyRobot);
    }
}
