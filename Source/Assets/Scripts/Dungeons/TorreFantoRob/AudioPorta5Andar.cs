using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPorta5Andar : MonoBehaviour
{
    AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

     public void Tocar()
    {
        source.Play();
    }
}
