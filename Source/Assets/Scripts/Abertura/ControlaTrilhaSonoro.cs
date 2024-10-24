using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaTrilhaSonoro : MonoBehaviour
{
    private AudioSource meuAudioSource;
    public AudioClip BackTrack;
    private bool playBack = true;
    // Start is called before the first frame update
    void Start()
    {
        meuAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {        
        if(!meuAudioSource.isPlaying && playBack)
        {
            meuAudioSource.clip=BackTrack;
            meuAudioSource.Play();
            meuAudioSource.loop = true;
            playBack = false;
        }
    }
}
