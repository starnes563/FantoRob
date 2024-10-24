using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActSubButton : MonoBehaviour
{
    //
    public void Clicar(int i)
    {
        switch(i)
        {
            case 0:
                IAPManager.instance.Buy500Credits();
                break;
            case 1:
                IAPManager.instance.Buy1000Credits();
                break;
            case 2:
                IAPManager.instance.Buy2000Credits();
                break;
            case 3:
                IAPManager.instance.Buy10000Credits();
                break;

        }
    }
}
