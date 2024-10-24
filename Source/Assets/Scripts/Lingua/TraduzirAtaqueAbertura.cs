using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraduzirAtaqueAbertura : MonoBehaviour
{
    public List<Attack> Ataques;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Attack at in Ataques)
        {
            at.NomeAbertura();
        }        
    }

   
}
