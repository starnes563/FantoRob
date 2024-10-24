using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotaoRetornar : MonoBehaviour
{
    public GameObject Gerenciador;
    private TransitionManager manager;
    private BattleManager battleManager;
    public bool atacou = false;
    void Start()
    {
        manager = Gerenciador.GetComponent<TransitionManager>();
        battleManager = Gerenciador.GetComponent<BattleManager>();
    }

    // Update is called once per frame
   public void Clicou()
    {
        SonsDoMenu.Desistir();
        manager.EsconderMenuAtaques();
        if (atacou)
        {            
            battleManager.PlayerEndTurn();            
        }
        else
        {                       
            battleManager.BattleState = battleManager.Reiniciar;
            manager.MostrarCaixaDeAcao();
        }       
        
    }

}
