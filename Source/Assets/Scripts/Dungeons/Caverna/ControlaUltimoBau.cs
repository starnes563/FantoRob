using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaUltimoBau : MonoBehaviour
{
    public GameObject BauErrado;
    public GameObject BauCorreto;
    public int ID;
    // Start is called before the first frame update
    void Start()
    {
        if (StoryEvents.DesafiosCamp[4].Interagiveis[ID])
        {
            BauAparecer();
        }
        else
        {
            BauErrado.SetActive(true);
            BauCorreto.SetActive(false);
        }
    }
    public void BauAparecer()
    {
        BauErrado.SetActive(false);
        BauCorreto.SetActive(true);
    }


}
