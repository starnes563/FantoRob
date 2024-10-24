using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Confirmacao : MonoBehaviour
{
    [HideInInspector]
    public ItemButon MyButton;
    // Start is called before the first frame update
    public void Confirmar()
    {
        SonsDoMenu.Confirmar();
        MyButton.PossoApertar = true;
        MyButton.GastarItem();
        Destroy(gameObject);
    }
    public void Desistir()
    {
        SonsDoMenu.Desistir();
        MyButton.PossoApertar = true;
        Destroy(gameObject);
    }
}
