using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultButton : MonoBehaviour
{
    public Centrifuga Centrifuga;
    public UnMerger Unmerger;

    public void Clicou()
    {
        if(Unmerger.pente != null)
        {
            Centrifuga.DispararCentrifuga();
        }
    }
    
}
