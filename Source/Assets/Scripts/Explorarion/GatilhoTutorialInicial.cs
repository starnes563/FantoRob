using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class GatilhoTutorialInicial : MonoBehaviour
{
    public GameObject CaixaAcao;
    bool mostrou = false;
    public SequenciaCena Director;
    public PlayableAsset Playable;
    // Update is called once per frame
    void Update()
    {
        if(CaixaAcao.activeSelf && !mostrou)
        {
            Iniciar();
        }
    }
    public void Iniciar()
    {        
            
            mostrou = true;
            Director.Começar(Playable);         
            
        
    }
}
