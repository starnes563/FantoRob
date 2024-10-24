using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreChangeButton : MonoBehaviour
{
    public GameObject ChangeMenu;
    public bool Fantorob;
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
        if (Fantorob)
        {
            ChangeMenu.GetComponent<ChangeRobot>().AbrirFantorob();
        }
        else
        {
            ChangeMenu.GetComponent<ChangeRobot>().AbrirParty();
        }
    }
}
