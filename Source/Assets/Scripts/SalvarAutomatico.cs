using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalvarAutomatico : MonoBehaviour
{
    public CaixaDeSalvamento CaixaDeSalvamento;
    // Start is called before the first frame update
    void Start()
    {
        CaixaDeSalvamento.AtivarInstancia();
        SaveSystem.Save();
    }

   
}
