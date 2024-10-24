using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtCancelarTroca : MonoBehaviour
{
    public GameObject Gerenciador;
    [HideInInspector]
    public bool fechar = true;
    private TransitionManager manager;
    private BattleManager battleManager;    
    void Start()
    {
        manager = Gerenciador.GetComponent<TransitionManager>();
        battleManager = Gerenciador.GetComponent<BattleManager>();
    }

    // Update is called once per frame
    public void Clicou()
    {
        if(fechar)
        {
            SonsDoMenu.Desistir();
            manager.EsconderMenuDeTroca();
            battleManager.BattleState = battleManager.Reiniciar;
            if (battleManager.Reiniciar == BattleManager.BattleStateMachine.PLAYERSTART)
            {
                manager.MostrarCaixaDeAcao();
            }
        }
        
    }
}
