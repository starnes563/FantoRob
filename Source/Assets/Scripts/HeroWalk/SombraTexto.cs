using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SombraTexto : MonoBehaviour
{
    public Text Principal;
    public Text Sombra;

    // Update is called once per frame
    void Update()
    {
        if(Principal.gameObject.activeSelf && Principal.text != Sombra.text)
        {
            Sombra.text = Principal.text;
        }
    }
}
