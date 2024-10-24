using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatilhoGirino : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach(FantoRob rob in PlayerObjects.RobotsInUse)
        {
            if(rob.Modelo == 0) { PlayerObjects.RobotsInUse.Remove(rob); }
        }
        foreach (FantoRob rob in PlayerObjects.RobotsNotInUse)
        {
            if (rob.Modelo == 0) { PlayerObjects.RobotsNotInUse.Remove(rob); }
        }
    }

}
