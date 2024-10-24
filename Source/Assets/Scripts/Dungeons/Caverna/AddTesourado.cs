using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTesourado : MonoBehaviour
{
    public FantoRob Tesourado;
    public void DarTesourado()
    {
        PlayerObjects.RobotsNotInUse.Add(Tesourado);        
    }
}
