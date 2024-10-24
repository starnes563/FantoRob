using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtItem : MonoBehaviour
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

    public void Clicar()
    {
        SonsDoMenu.Confirmar();
        manager.EsconderCaixaDeAcao();
        manager.MostrarMenuItem();
        battleManager.BattleState = BattleManager.BattleStateMachine.PLAYERITEM;
    }
}
