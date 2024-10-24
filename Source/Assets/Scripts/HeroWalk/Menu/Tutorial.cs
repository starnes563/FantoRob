using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public List<GameObject> Objetos = new List<GameObject>();
    public AudioSource AudioSource;
    public AudioClip SomConfirma;
    public void AcionarBotao()
    {
        AudioSource.PlayOneShot(SomConfirma);
        foreach(GameObject g in Objetos)
        {
            g.SetActive(false);
        }
    }
}
