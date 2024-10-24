using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacaoTrocaEnemy : MonoBehaviour
{
    [HideInInspector]
    public ActiveRobotManager manager;
    [HideInInspector]
    public TransitionManager tmanager;
    [HideInInspector]
    public bool FimdeBatalha = false;
    [HideInInspector]
    public bool ComecoBatalha = false;
    // Start is called before the first frame update
    public void Trocar()
    {
        if (ComecoBatalha)
        {
            manager.RoboEnemyAparecer();
        }
        else
        {
            manager.TrocarDeRoboEnemy();
        }        
    }
    public void Fim()
    {
        if (FimdeBatalha)
        {
            manager.PlayerWins();
        }
        else if (!ComecoBatalha)
        {
            manager.battleManager.BattleState = BattleManager.BattleStateMachine.START;
        }
        Destroy(this.gameObject);
    }

}
