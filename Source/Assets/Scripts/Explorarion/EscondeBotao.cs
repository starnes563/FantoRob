using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscondeBotao : MonoBehaviour
{
    public int Estrela;
    void Start()
    {
        if(PlayerStatus.Estrelas<Estrela)
        {
            this.gameObject.SetActive(false);
        }
    }
   
}
