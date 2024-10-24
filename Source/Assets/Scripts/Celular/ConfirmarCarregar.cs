using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmarCarregar : MonoBehaviour
{
    public int MeuSave;
    public void Clicou()
    {
        ManagerGame.Instance.ActualSavePath = MeuSave;
        StartCoroutine(ManagerGame.Instance.Load());
    }
}
