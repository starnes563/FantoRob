using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacaoTrocaPlayer : MonoBehaviour
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
        if(ComecoBatalha)
        {
            manager.RoboPlayerAparecer();
        }
        else
        {
            manager.TrocarDeRoboPlayer();
        }       
    }
    public void Fim()
    {
        if (FimdeBatalha)
        {
            manager.PlayerLoses();
        }
        if(ComecoBatalha)
        {
            tmanager.MostrarCaixaDeAcao();
        }
        if(!ComecoBatalha && !FimdeBatalha)
        {
            manager.battleManager.BattleState = BattleManager.BattleStateMachine.START;
        }
        Destroy(this.gameObject);
    }
}
