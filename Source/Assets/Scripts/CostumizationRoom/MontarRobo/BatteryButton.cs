using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryButton : MonoBehaviour
{
    
    public CoreMenu Coremenu;
    MenuManager menuManager;
    // Start is called before the first frame update
    void Start()
    {
        menuManager = GameObject.FindWithTag("Gerenciador").GetComponent<MenuManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Clicar()
    {
        menuManager.AbrirMenuBateria(Coremenu);
    }
}
