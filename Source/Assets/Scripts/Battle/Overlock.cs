using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overlock : MonoBehaviour
{
    [HideInInspector]
    public BattleManager battleManager;
    // Update is called once per frame
   public void Finalizar()
    {
       // battleManager.BattleState = battleManager.Reiniciar;
        Destroy(gameObject);
    }
        

}
