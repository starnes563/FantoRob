using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardaRoupa : MonoBehaviour
{
    //o guarda roupa precisa ser acionado toda vez quer se quer trocar de roupa na proxima cena
    public int ProximaRoupa;
    public bool trocarderoupa;
    // Start is called before the first frame update
    void Start()
    {
        if(trocarderoupa)
        {
            PlayerStatus.PersonagemAtual = ProximaRoupa;
            trocarderoupa = false;
        }
       
    }

    
}
