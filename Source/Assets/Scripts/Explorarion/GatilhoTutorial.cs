using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatilhoTutorial : MonoBehaviour
{
    public Fala Dialogo;
    bool podefalar = false;
    Walk Player;
    public GameObject Tutorial;
    public AudioClip SomAciona;
    public AudioSource AudioSource;
    public List<string> FalaTutorial;
    public string Nome;
    public void Acionar()
    {
        Tutorial.SetActive(true);
        if (Dialogo != null)
        {
            Dialogo.DigitarNaTela(FalaTutorial[ManagerGame.Instance.Idm], Nome);
        }
        Player.PararDeAndar();
    }
    void Update()
    {
        if(podefalar && !Tutorial.activeSelf && Input.GetButtonDown("Fire1"))
        {
            Acionar();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!podefalar && other.tag == "Player")
        {
            Player = other.GetComponent<Walk>();
            podefalar = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (podefalar) { podefalar = false; }
    }    
}

