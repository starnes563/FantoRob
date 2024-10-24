using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriculoProcurando : MonoBehaviour
{
    public GameObject TelaProcurarRival;
    public GameObject Pai;
    public void Finalizar()
    {
        if(Random.Range(0,101)>50)
        {
            TelaProcurarRival.SetActive(true);
            Pai.gameObject.SetActive(false);
        }
    }
}
