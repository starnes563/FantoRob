using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreButton : MonoBehaviour
{
    public GameObject MyCore;
    private MenuManager menuManager;
    public int MyId;
    public RobotGeneralMenu Menu;
    public Status MyStatus;
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
        menuManager.AbrirMenuNucleo(MyCore, MyId, Menu, MyStatus);
    }
}
