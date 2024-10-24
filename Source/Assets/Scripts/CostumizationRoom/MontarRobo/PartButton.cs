using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartButton : MonoBehaviour
{
    [HideInInspector]
    public RobotPart MyPart;
    private MenuManager menuManager;
    [HideInInspector]
    public int MyId;
    [HideInInspector]
    public RobotGeneralMenu Menu;
    // Start is called before the first frame update
    void Start()
    {
        menuManager = GameObject.FindWithTag("Gerenciador").GetComponent<MenuManager>();
    }   
    public void Clicou()
    {
        menuManager.AbrirMenuPart(MyPart, MyId, Menu, transform.GetChild(0).GetComponent<Image>().sprite);
    }
}
