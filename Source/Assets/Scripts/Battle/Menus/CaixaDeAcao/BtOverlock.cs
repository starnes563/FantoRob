using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtOverlock : MonoBehaviour
{
    public RobotManager robo;
    public BattleManager battleManager;
    // Start is called before the first frame update
public void Clicou()
    {
        battleManager.BattleState = BattleManager.BattleStateMachine.PLAYERANIMATION;
        SonsDoMenu.Confirmar();
        robo.OverlockOn();
    }

}
