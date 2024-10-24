using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ovo : MonoBehaviour
{
    public AudioClip SomQuebrar;
   public void Destruir()
    {
        Destroy(this.gameObject);
    }
    public void TocarSomQuebrar()
    {
        GetComponent<AudioSource>().PlayOneShot(SomQuebrar);
    }
}
