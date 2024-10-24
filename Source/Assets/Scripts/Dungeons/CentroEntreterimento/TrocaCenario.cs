using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrocaCenario : MonoBehaviour
{
    [HideInInspector]
    public int Gatilho = 0;
    public GameObject Objeto0;
    public GameObject Objeto1;
    // Start is called before the first frame update
    void Awake()
    {
        switch(Gatilho)
        {
            case 0:
                Objeto0.SetActive(true);
                Objeto1.SetActive(false);
                break;
            case 1:
                Objeto0.SetActive(false);
                Objeto1.SetActive(true);
                break;
        }
    }
}
