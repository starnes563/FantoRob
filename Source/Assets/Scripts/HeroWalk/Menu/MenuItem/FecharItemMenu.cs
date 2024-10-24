using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FecharItemMenu : MonoBehaviour
{
    public ItensMenu ItensMenu;

    public void Clicou()
    {
        SonsMenu.Desistir();
        ItensMenu.Fechar();
    }
}
