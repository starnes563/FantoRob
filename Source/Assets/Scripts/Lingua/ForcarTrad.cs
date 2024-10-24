using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcarTrad : MonoBehaviour
{
    public int Language;
    // Start is called before the first frame update
    void Start()
    {
        ManagerGame.Instance.Idm = Language;
    }

    
}
