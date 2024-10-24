using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotEnoghtMoney : MonoBehaviour
{
    public bool Ativo = false;
    public ManagerShop Manager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Ativo)
        {
            if(Input.GetButton("Fire2"))
            {
                gameObject.SetActive(false);
                Ativo = false;
                //Manager.billBoardAtivo = false;
            }
        }
    }
}
