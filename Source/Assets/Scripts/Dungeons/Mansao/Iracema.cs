using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iracema : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerStatus.ControleDeCena<=51||PlayerObjects.EsqueletoEspecial<=0)
        {
            this.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
