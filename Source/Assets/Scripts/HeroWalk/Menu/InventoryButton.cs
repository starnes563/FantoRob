﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButton : MonoBehaviour
{
    public PlayerMenu Menu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Clicou()
    {
        SonsMenu.Confimar();
        Menu.AbrirMenuInventario();
    }
}
