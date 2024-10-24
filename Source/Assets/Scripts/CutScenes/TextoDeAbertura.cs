using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextoDeAbertura : MonoBehaviour
{
    public GameObject CaixadeAudio;
    public GameObject RestoDaCena;

    public void AtivaMusica()
    {
        CaixadeAudio.SetActive(true);
    }
    public void AtivaCena()
    {
        RestoDaCena.SetActive(true);       
    }
    public void Destruir()
    {
        Destroy(this.gameObject);
    }
}
