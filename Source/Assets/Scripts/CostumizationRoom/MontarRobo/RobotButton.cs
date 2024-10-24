using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotButton : MonoBehaviour
{
    public FantoRob MyRobot;
    private MenuManager menuManager;
    // Start is called before the first frame update
    void Start()
    {
        menuManager = GameObject.FindWithTag("Gerenciador").GetComponent<MenuManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Clicou()
    {
        menuManager.EscolherRobo(MyRobot);
    }
}
