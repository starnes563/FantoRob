using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotSpace : MonoBehaviour
{
    public bool Ativo = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Ativo)
        {
            if(Input.GetKeyDown("Fire2"))
            {
                Fechar();
            }
        }
    }
    void Fechar()
    {
        Ativo = false;
        gameObject.SetActive(true);
    }
}
