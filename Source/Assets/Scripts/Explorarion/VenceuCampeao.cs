using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenceuCampeao : MonoBehaviour
{
    public AudioSource Source;
    public AudioClip FanfarraCampeao;
    bool tocou = false;
    public GameObject QuadroEstrela;
    [HideInInspector]
    public bool primeiravez = true;
    private void Update()
    {       
        if(!Source.isPlaying && tocou)
        {
            CaixaDeSom.Instancia.GetComponent<AudioSource>().UnPause();
            CaixaDeSom.Instancia.GetComponent<AudioSource>().volume = 0;
            StartCoroutine(CaixaDeSom.Instancia.AumentaVolume());
            GameObject.FindWithTag("Player").GetComponent<Walk>().CanIWalk = true;
            tocou = false;
        }
    }
    public void Tocar()
    {
        if(!primeiravez)
        {
            QuadroEstrela.SetActive(true);
            tocou = true;
            CaixaDeSom.Instancia.GetComponent<AudioSource>().Pause();
            Source.PlayOneShot(FanfarraCampeao);
            primeiravez = true;
            GameObject.FindWithTag("Player").GetComponent<Walk>().PararDeAndar();
        }
       
    }
}
