using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtCancelarItem : MonoBehaviour
{
    public GameObject Gerenciador;
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
        SonsDoMenu.Desistir();
        manager.EsconderMenuItem();
        battleManager.BattleState = battleManager.Reiniciar;
        if(battleManager.Reiniciar == BattleManager.BattleStateMachine.PLAYERSTART)
        {
            manager.MostrarCaixaDeAcao();
        }
    }
}
