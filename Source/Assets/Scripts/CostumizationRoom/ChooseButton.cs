using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseButton : MonoBehaviour
{
    [HideInInspector]
    public int MyCr;
    [HideInInspector]
    public Pente MyComb;
    [HideInInspector]
    public Merger merger;
    [HideInInspector]
    public int meutipo;
    [HideInInspector]
    public int Index;
    public GameObject IconeSelec;
    // Start is called before the first frame update
    
    // Update is called once per frame
     public void Cliclou()
    {      
        merger.TocarSomConfirma();
        switch (meutipo)
        {
            case 0:
                merger.EscolherPente(MyComb, Index);
                break;
            case 1:
                merger.LimparCircuito();
                IconeSelec.SetActive(true);
                merger.SlotEscolhido(Index);
                break;
        }
    }
    public void Deselecionar()
    {
        IconeSelec.SetActive(false);
    }
}
