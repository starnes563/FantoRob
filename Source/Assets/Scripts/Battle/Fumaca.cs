using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fumaca : MonoBehaviour
{    public BattleManager bm;
    // Start is called before the first frame update
   public void Finalizar()
    {        
        bm.BattleState = BattleManager.BattleStateMachine.START;
        Destroy(gameObject);
    }
}
