using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotaoMenu : MonoBehaviour
{
    public List<GameObject> Fechar;
    public List<GameObject> Abrir;
public void Clicar()
    {
        foreach(GameObject g in Fechar)
        {
            g.SetActive(false);
        }
        foreach(GameObject g in Abrir)
        {
           g.SetActive(true);
        }
        SonsMenu.Confimar();
    }
}
