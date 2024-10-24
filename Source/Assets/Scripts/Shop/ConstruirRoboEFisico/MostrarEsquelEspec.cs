using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MostrarEsquelEspec : MonoBehaviour
{
    public Text Texto;
    int ultValor;
    // Start is called before the first frame update
    void Start()
    {
        ultValor = PlayerObjects.EsqueletoEspecial;
        Texto.text = PlayerObjects.EsqueletoEspecial.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(ultValor != PlayerObjects.EsqueletoEspecial)
        {
            Texto.text = PlayerObjects.EsqueletoEspecial.ToString();
        }
    }
}
