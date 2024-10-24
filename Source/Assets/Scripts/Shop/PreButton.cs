using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreButton : MonoBehaviour
{
    public GameObject Manager;
    public bool Sell;
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
        if(Sell)
        {
           // Manager.GetComponent<ManagerShop>().MostrarVender();
        }
        else
        {
         //   Manager.GetComponent<ManagerShop>().MostrarComprar();
        }
    }
}
