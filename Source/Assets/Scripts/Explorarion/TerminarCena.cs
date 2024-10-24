using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminarCena : MonoBehaviour
{    
    public void TerminarAnimacao()
    {          
    //Destroy(gameObject);
    }
    public void TrocarCena()
    {
        ManagerGame.Instance.LoadNextScene();
    }
}
