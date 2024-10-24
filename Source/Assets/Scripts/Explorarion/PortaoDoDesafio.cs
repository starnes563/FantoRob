using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaoDoDesafio : MonoBehaviour
{
    public int EstrelasParaAbrir;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerStatus.Estrelas >=EstrelasParaAbrir && PlayerStatus.CartaEndosso)
        {
            Destroy(this.gameObject);
        }
    }    
}
