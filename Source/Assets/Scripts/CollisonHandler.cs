using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisonHandler : MonoBehaviour
{
    public int SceneToLoad;
    public GameObject SpawnPoint;
    public AudioSource AudioSource;
    public AudioClip EfeitoSom;
    public string PosicaoDoAnimatorProximaCena;
    public string PosicaoAnimatorCenaAtual;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            AudioSource.PlayOneShot(EfeitoSom);
        }
    }
}
