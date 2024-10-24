using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesligaTutorial : MonoBehaviour
{
    public static bool Tutorial;
    // Start is called before the first frame update
    void OnEnable()
    {
        if (!Tutorial) { this.gameObject.SetActive(false); }
    }    
}
