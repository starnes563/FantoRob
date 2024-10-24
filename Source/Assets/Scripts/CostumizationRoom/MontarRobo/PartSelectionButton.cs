using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartSelectionButton : MonoBehaviour
{
    [HideInInspector]
    public RobotPart MyPiece;
    [HideInInspector]
    public PartMenu PartMenu; 
    [HideInInspector]
    public PartMenuSelecao SelecaoParte;
    public GameObject[] Icones = new GameObject[5];
    public Text Texto;
    public void Clicou()
    {
        PartMenu.SelecionarParte(MyPiece);
        SelecaoParte.Criar(PartMenu.ParteAtual, PartMenu.MenuRobo.MeuFantorob.Modelo);
    }
}
