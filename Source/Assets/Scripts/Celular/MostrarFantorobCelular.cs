 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MostrarFantorobCelular : MonoBehaviour
{
    public Image NucleoFisico;
    public Image NucleoElemental;
    public SpriteRenderer MiniiconeFantorob;
   public void Mostrar(FantoRob rob)
    {
        MiniiconeFantorob.sprite = rob.MenuIconeFantorob;
        NucleoFisico.sprite = rob.Fisico.MySprite;
        NucleoElemental.sprite = rob.SpriteElemento;
    }
}
