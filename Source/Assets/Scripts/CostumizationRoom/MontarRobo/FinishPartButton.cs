using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPartButton : MonoBehaviour
{
    [HideInInspector]
    public PartMenu PartMenu;
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
        PartMenu.Concluir();
    }
}
