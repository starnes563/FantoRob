using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GerenTutMarcus : MonoBehaviour
{
    public BattleManager BtManager;
    public SequenciaCena Director;
    public PlayableAsset Playable;
    bool ativado;
    // Start is called before the first frame update
    void Start()
    {
        if (StoryEvents.TutorialMarcus) { BtManager.EventoMarcus += ativar; }
        else { this.gameObject.SetActive(false); }
    }
    private void Update()
    {
        if(ativado && BtManager.BattleState== BattleManager.BattleStateMachine.IDLE)
        {
            Mostrar();  
        }
    }
    void ativar()
    {
        ativado = true;
    }
    void Mostrar()
    {
        ativado = false;
        Director.Começar(Playable);       
    }
}
