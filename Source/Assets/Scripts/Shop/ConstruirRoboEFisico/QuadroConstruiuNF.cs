using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuadroConstruiuNF : MonoBehaviour
{
    public Image ImagemNF;
    public Text NomeNF;
    public AudioClip SomConstruiu;
    public AudioSource AudioSource;
    private AudioSource caixaDeSom;
    
    public void Mostrar(Weapon nf)
    {
        ImagemNF.sprite = nf.MySprite;
        NomeNF.text = nf.Nome[ManagerGame.Instance.Idm];
        this.gameObject.SetActive(true);
        caixaDeSom = GameObject.Find("CaixaDeSom").GetComponent<AudioSource>();
        caixaDeSom.Pause();       
        AudioSource.PlayOneShot(SomConstruiu);
    }
    void Update()
    {
        if (!AudioSource.isPlaying)
        {
            caixaDeSom.UnPause();
        }
    }
    public void Desativar()
    {
        if (!AudioSource.isPlaying)
        {
            Time.timeScale = 1f;
            this.gameObject.SetActive(false);
            Leticia.TocarSomDesiste();
        }
    }

}
