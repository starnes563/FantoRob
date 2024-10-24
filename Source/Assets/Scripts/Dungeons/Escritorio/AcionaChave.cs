using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcionaChave : MonoBehaviour
{   
    PossuiChave[] chave;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PorChave());
    }
    IEnumerator PorChave()
    {        
        chave = FindObjectsOfType<PossuiChave>();        
        chave[Random.Range(0, chave.Length)].possuichave = true;       
        yield return null;               
    }   
}
