using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtTrocar : MonoBehaviour
{
    public GameObject Gerenciador;
    private TransitionManager manager;
    private BattleManager battleManager;
    // Start is called before the first frame update
    void Start()
    {
        manager = Gerenciador.GetComponent<TransitionManager>();
        battleManager = Gerenciador.GetComponent<BattleManager>();
    }

    // Update is called once per frame
    public void Clicar()
    {
        SonsDoMenu.Confirmar();
        manager.EsconderCaixaDeAcao();
        manager.MostrarMenuDeTroca(true);
        battleManager.BattleState = BattleManager.BattleStateMachine.PLAYERTRADE;
    }
}
