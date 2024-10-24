using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SomBarra : MonoBehaviour
{
    public AudioClip MeuClip;
    private Slider slider;
    private bool possotocar;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(slider.value == slider.maxValue && possotocar)
        {
            SomBarraDeAcao.Instancia.PlayOneShot(MeuClip);
            possotocar = false;
        }
        if(slider.value < slider.maxValue && !possotocar)
        {
            possotocar = true;
        }
    }
}
